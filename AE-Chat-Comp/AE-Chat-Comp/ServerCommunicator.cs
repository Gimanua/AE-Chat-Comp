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
            Communicating = false;
            if (e.Error != null || e.Cancelled)
            {
                MessageBox.Show(e.Error.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string responseString = Encoding.Default.GetString(e.Result);
            if (responseString.StartsWith("RTL:"))
            {
                if(responseString.Substring(4,2) == "S:")
                    MessageBox.Show(responseString.Substring(6), "Respons", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(responseString.Substring(6), "Fel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (responseString.StartsWith("RTR:"))
            {
                if (responseString.Substring(4, 2) == "S:")
                    MessageBox.Show(responseString.Substring(6), "Respons", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(responseString.Substring(6), "Fel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (responseString.StartsWith("RTSM:"))
            {
                if (responseString.Substring(5, 2) == "S:")
                {
                    //Ta bort från pending_messages
                    if(int.TryParse(responseString.Substring(7), out int msgId))
                        PendingMessages.RemovePendingMessage(msgId);
                }
                else
                    MessageBox.Show(responseString.Substring(7), "Fel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
