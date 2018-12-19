namespace AE_Chat_Comp
{
    using System.Net;
    using System.Collections.Specialized;
    using System.Text;
    using System.Windows.Forms;

    static class ServerCommunicator
    {
        public static bool Communicating { get; set; } = false;

        public static void Login(string username, string password)
        {
            NameValueCollection postData = new NameValueCollection()
            {
                { "intent", "login"},
                { "username", username },
                { "password", password }
            };
            ServerRequest(postData);
        }

        public static void Register(string username, string password)
        {
            NameValueCollection postData = new NameValueCollection()
            {
                { "intent", "register"},
                { "username", username },
                { "password", password }
            };
            ServerRequest(postData);
        }

        public static void SendMessage(string sender, string receiver, string timestamp, string message, string id)
        {
            NameValueCollection postData = new NameValueCollection()
            {
                { "intent", "sendMessage"},
                { "sender", sender },
                { "receiver", receiver },
                { "timestamp", timestamp},
                { "message", message },
                { "id", id }
            };
            ServerRequest(postData);
        }
        
        private static void ServerRequest(NameValueCollection postData)
        {
            using (WebClient client = new WebClient())
            {
                client.UploadValuesCompleted += HandleServerResponse;
                client.UploadValuesAsync(Configurator.Address, postData);
            }
        }

        private static void HandleServerResponse(object sender, UploadValuesCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                MessageBox.Show(e.Error.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string responseString = Encoding.Default.GetString(e.Result);
            if (responseString.StartsWith("ResponseToLogin:"))
            {
                if(responseString.Substring(16,2) == "S:")
                {
                    //Det lyckades
                    MessageBox.Show(responseString.Substring(18), "Respons", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(responseString.Substring(18), "Fel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (responseString.StartsWith("ResponseToRegister:"))
            {
                if (responseString.Substring(19, 2) == "S:")
                {
                    //Det lyckades
                    MessageBox.Show(responseString.Substring(21), "Respons", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(responseString.Substring(21), "Fel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (responseString.StartsWith("ResponseToSendMessage:"))
            {
                if (responseString.Substring(22, 2) == "S:")
                {
                    //Det lyckades
                    MessageBox.Show(responseString.Substring(24), "Respons", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(responseString.Substring(24), "Fel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
