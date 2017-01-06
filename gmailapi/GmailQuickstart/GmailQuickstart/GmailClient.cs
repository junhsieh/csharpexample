using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GmailQuickstart
{
    class GmailClient
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        UserCredential Credential;
        GmailService Service;
        public bool IsValidCredential;

        string[] Scopes = { GmailService.Scope.MailGoogleCom };
        string ApplicationName = "Gmail API .NET Quickstart";
        string UserId = "me";

        public GmailClient()
        {
            if (this.SetCredential() == true)
            {
                // Create Gmail API service.
                this.Service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = this.Credential,
                    ApplicationName = this.ApplicationName,
                });
            }
        }

        public bool SetCredential()
        {
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart.json");

                this.Credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            if (this.Credential != null)
            {
                this.IsValidCredential = true;
            }

            return this.IsValidCredential;
        }

        public void ProcessMessageMain()
        {
            string query = "in:(INBOX)"; // in:(INBOX OR SENT)
            List<Message> msgArr = this.ListMessages(query);

            foreach (var msg in msgArr)
            {
                Console.WriteLine("{0}", msg.Id);

                Message msgObj = this.GetMessage(msg.Id);

                IList<MessagePartHeader> headerArr = msgObj.Payload.Headers;

                //foreach (var item in headerArr)
                //{
                //    Console.WriteLine("{0} => {1}", item.Name, item.Value);
                //}
            }
        }

        /// <summary>
        /// List the labels in the user's mailbox.
        /// </summary>
        /// <param name="service">Gmail API service instance.</param>
        /// <param name="userId">User's email address. The special value "me"
        /// can be used to indicate the authenticated user.</param>
        public void ListLabels()
        {
            try
            {
                ListLabelsResponse response = this.Service.Users.Labels.List(this.UserId).Execute();

                foreach (Label label in response.Labels)
                {
                    Console.WriteLine(label.Id + " - " + label.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        /// <summary>
        /// List all Messages of the user's mailbox matching the query.
        /// </summary>
        /// <param name="service">Gmail API service instance.</param>
        /// <param name="userId">User's email address. The special value "me"
        /// can be used to indicate the authenticated user.</param>
        /// <param name="query">String used to filter Messages returned.</param>
        public List<Message> ListMessages(String query)
        {
            List<Message> result = new List<Message>();
            UsersResource.MessagesResource.ListRequest request = this.Service.Users.Messages.List(this.UserId);
            request.Q = query;

            do
            {
                try
                {
                    ListMessagesResponse response = request.Execute();
                    result.AddRange(response.Messages);
                    request.PageToken = response.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
            } while (!String.IsNullOrEmpty(request.PageToken));

            return result;
        }

        /// <summary>
        /// Retrieve a Message by ID.
        /// </summary>
        /// <param name="service">Gmail API service instance.</param>
        /// <param name="userId">User's email address. The special value "me"
        /// can be used to indicate the authenticated user.</param>
        /// <param name="messageId">ID of Message to retrieve.</param>
        public Message GetMessage(String messageId)
        {
            try
            {
                return this.Service.Users.Messages.Get(this.UserId, messageId).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return null;
        }
    }
}
