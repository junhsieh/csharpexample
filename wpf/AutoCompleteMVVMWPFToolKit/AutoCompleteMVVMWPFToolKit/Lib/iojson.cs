using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace AutoCompleteMVVMWPFToolKit.Lib
{
    class iojson
    {
        public bool Status { get; set; }
        public List<string> ErrArr { get; set; }
        public List<object> ObjArr { get; set; }
        public Dictionary<string, object> ObjMap { get; set; }

        public iojson()
        {
            this.ErrArr = new List<string>();
            this.ObjArr = new List<object>();
            this.ObjMap = new Dictionary<string, object>();
        }

        public void AddError(string str)
        {
            this.ErrArr.Add(str);
        }

        public void AddObjToArr(object obj)
        {
            this.ObjArr.Add(obj);
        }

        public object GetObjFromArr(int k, object obj)
        {
            if (k < 0 || k >= this.ObjArr.Count)
            {
                return null;
            }

            var jsonRaw = this.ObjArr[k];

            if (obj == null)
            {
                return jsonRaw;
            }

            if (jsonRaw is string)
            {
                obj = jsonRaw;
                return jsonRaw;
            }

            populateObj(obj, jsonRaw.ToString());
            return obj;
        }

        public void AddObjToMap(string key, object obj)
        {
            this.ObjMap.Add(key, obj);
        }

        public object GetObjFromMap(string k, object obj)
        {
            var jsonRaw = this.ObjMap[k];

            if (obj == null)
            {
                return jsonRaw;
            }

            populateObj(obj, jsonRaw.ToString());
            return obj;
        }

        public string Encode()
        {
            if (this.ErrArr.Count > 0)
            {
                this.Status = false;
                this.ObjArr = new List<object>();
                this.ObjMap = new Dictionary<string, object>();
            }
            else
            {
                this.Status = true;
            }

            return JsonConvert.SerializeObject(this);
        }

        public void Decode(string jsonStr)
        {
            populateObj(this, jsonStr);
        }

        public error populateObj<T>(T obj, string jsonStr)
        {
            var serializer = new JsonSerializer();

            using (var reader = new StringReader(jsonStr))
            {
                serializer.Populate(reader, obj);
            }

            return null;
        }
    }

    class error
    {
        public string S { get; set; }

        public void New(string s)
        {
            this.S = s;
        }

        public string Error()
        {
            return this.S;
        }
    }
}
