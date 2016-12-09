using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoCompleteMVVMWPFToolKit.Lib
{
    class Util
    {
        public static char ConvertKeyToChar(Key key)
        {
            return Convert.ToChar(KeyInterop.VirtualKeyFromKey(key));
        }

        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static iojson GetWebContent(ref HTTPData httpdata, string url)
        {
            iojson i = new iojson();

            try
            {
                Debug.WriteLine("GetWebContent.url: " + url);

                // NOTE: this is a temporary remove SSL ceritficate check solution. Please comment out this line in production.
                if (ConfigurationManager.AppSettings["IsFakeSSLCert"] == "yes")
                {
                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                }

                var client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    CookieContainer = httpdata.Cookie,
                });

                client.BaseAddress = new Uri(GetWebSrvURL());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(url).Result;

                // will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                string resultContent = response.Content.ReadAsStringAsync().Result;

                // set CSRF token
                IEnumerable<string> values;

                if (response.Headers.TryGetValues("X-CSRF-Token", out values))
                {
                    httpdata.CSRFtoken = values.First();
                }

                i.Decode(resultContent);
            }
            catch (Exception e)
            {
                // TODO: need to improve.
                Debug.WriteLine("GetWebContent: " + e.Message.ToString());
                i.AddError("GetWebContent: " + e.Message.ToString());
            }

            Debug.WriteLine("GetWebContent.Encode: " + i.Encode());

            return i;
        }

        public static iojson PostWebContent(ref HTTPData httpdata, string url, string jsonStr)
        {
            iojson i = new iojson();

            try
            {
                Debug.WriteLine("PostWebContent.url: " + url);
                Debug.WriteLine("PostWebContent.jsonStr: " + jsonStr);

                // NOTE: this is a temporary remove SSL ceritficate check solution. Please comment out this line in production.
                if (ConfigurationManager.AppSettings["IsFakeSSLCert"] == "yes")
                {
                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                }

                var client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    CookieContainer = httpdata.Cookie,
                });
                client.BaseAddress = new Uri(GetWebSrvURL());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-CSRF-Token", httpdata.CSRFtoken);

                //var content = new FormUrlEncodedContent(new[] {
                //    new KeyValuePair<string, string>("", "login")
                //});
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                // will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                string resultContent = response.Content.ReadAsStringAsync().Result;

                // set CSRF token
                IEnumerable<string> values;

                if (response.Headers.TryGetValues("X-CSRF-Token", out values))
                {
                    httpdata.CSRFtoken = values.First();
                }

                i.Decode(resultContent);
            }
            catch (Exception e)
            {
                // TODO: need to improve.
                i.AddError("PostWebContent: " + e.Message.ToString());
            }

            return i;
        }

        public static iojson UploadFile(ref HTTPData httpdata, string url, string[] fileNameArr)
        {
            iojson i = new iojson();

            try
            {
                Debug.WriteLine("PostWebContent.url: " + url);

                // NOTE: this is a temporary remove SSL ceritficate check solution. Please comment out this line in production.
                if (ConfigurationManager.AppSettings["IsFakeSSLCert"] == "yes")
                {
                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                }

                var client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    CookieContainer = httpdata.Cookie,
                });
                client.BaseAddress = new Uri(GetWebSrvURL());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-CSRF-Token", httpdata.CSRFtoken);

                //var content = new FormUrlEncodedContent(new[] {
                //    new KeyValuePair<string, string>("", "login")
                //});

                MultipartFormDataContent form = new MultipartFormDataContent();

                //form.Add(new StringContent("Jun"), "username");

                for (int c = 0; c < fileNameArr.Length; c++)
                {
                    string fileNameFull = fileNameArr[c];
                    byte[] MyFile = StreamFile(fileNameFull);
                    string fileName = Path.GetFileName(fileNameFull);
                    form.Add(new ByteArrayContent(MyFile, 0, MyFile.Count()), "files", fileName);
                }

                HttpResponseMessage response = client.PostAsync(url, form).Result;

                // will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                client.Dispose();

                string resultContent = response.Content.ReadAsStringAsync().Result;

                // set CSRF token
                IEnumerable<string> values;

                if (response.Headers.TryGetValues("X-CSRF-Token", out values))
                {
                    httpdata.CSRFtoken = values.First();
                }

                i.Decode(resultContent);
            }
            catch (Exception e)
            {
                // TODO: need to improve.
                i.AddError("UploadFile: " + e.Message.ToString());
            }

            return i;
        }

        public static byte[] StreamFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            // Create a byte array of file stream length
            byte[] fileData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();
            return fileData; //return the byte data
        }

        public static async Task<string> AjaxV1(string uri)
        {
            try
            {
                // NOTE: this is a temporary remove SSL ceritficate check solution. Please comment out this line in production.
                if (ConfigurationManager.AppSettings["IsFakeSSLCert"] == "yes")
                {
                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                }

                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(uri);

                // will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                //return await Task.Run(() => JsonObject.Parse(content));
                //return await Task.Run(() => content);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                // TODO: need to improve.
                Debug.WriteLine(e.Message);
                return JSONFailed();
            }
        }

        public static string GetWebSrvURL()
        {
            return ConfigurationManager.AppSettings["WebSrvProtocol"] +
                "://" + ConfigurationManager.AppSettings["WebSrvDomain"] +
                ":" + ConfigurationManager.AppSettings["WebSrvPort"];
        }

        public static T SelectedRadioValue<T>(T defaultValue, params RadioButton[] buttons)
        {
            foreach (RadioButton button in buttons)
            {
                if (button.IsChecked == true)
                {
                    if (button.Tag is string && typeof(T) != typeof(string))
                    {
                        string value = (string)button.Tag;
                        return (T)Convert.ChangeType(value, typeof(T));
                    }

                    return (T)button.Tag;
                }
            }

            return defaultValue;
        }

        public static string JSONFailed()
        {
            return "{Status:false}";
        }

        public static Int64 ConvStrInt64(string num)
        {
            Int64 n = 0;

            try
            {
                n = Int64.Parse(num);
            }
            catch (Exception)
            {
                n = 0;
            }

            return n;
        }

        public static void PerformRequest(string method, CookieContainer cookie, string csrftoken, string url, string postData)
        {
            // NOTE: this is a temporary remove SSL ceritficate check solution. Please comment out this line in production.
            if (ConfigurationManager.AppSettings["IsFakeSSLCert"] == "yes")
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetWebSrvURL() + url);
            request.CookieContainer = cookie; // use the global cookie variable

            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.Method = method;
            request.Headers.Add("X-CSRF-Token", csrftoken);

            if (method == "POST")
            {
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            WebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //CSRFtoken = response.Headers.Get("X-CSRF-Token");

            //Debug.WriteLine("CSRFtoken: " + CSRFtoken);
            Debug.WriteLine(responseString);
        }

        public static byte[] StreamFileV1(string filename)
        {
            // NOTE: This will fail if the file is opened by another process.
            return File.ReadAllBytes(filename);
        }

        public static byte[] StreamFileV2(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();
            return ImageData; //return the byte data
        }
    }
}
