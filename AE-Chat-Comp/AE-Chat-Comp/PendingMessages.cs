namespace AE_Chat_Comp
{
    using System.Collections.Generic;
    using System.Xml;

    static class PendingMessages
    {
        public static readonly string Path = Configurator.PendingMessagesPath;
        
        public static int AppendPendingMessage(string sender, string receiver, string timestamp, string message)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path);

            int msgId = -1;
            
            XmlNodeList nodes = doc.GetElementsByTagName("pending_message");
            List<int> Ids = new List<int>();

            foreach(XmlNode node in nodes)
            {
                if (int.TryParse(node.Attributes["id"].Value, out int id))
                    Ids.Add(id);
            }

            for (int i = 0; i <= nodes.Count; i++)
            {
                if (!Ids.Contains(i))
                {
                    msgId = i;
                    break;
                }
            }

            XmlElement msg = doc.CreateElement("pending_message");

            XmlAttribute atr = doc.CreateAttribute("sender");
            atr.Value = sender;
            msg.Attributes.Append(atr);

            atr = doc.CreateAttribute("receiver");
            atr.Value = receiver;
            msg.Attributes.Append(atr);

            atr = doc.CreateAttribute("timestamp");
            atr.Value = timestamp;
            msg.Attributes.Append(atr);

            atr = doc.CreateAttribute("id");
            atr.Value = msgId.ToString();
            msg.Attributes.Append(atr);

            msg.InnerText = message;

            doc.SelectSingleNode("/pending_messages").AppendChild(msg);
            doc.Save(Path);

            return msgId;
        }

        public static void RemovePendingMessage(int msgId)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path);

            XmlNode msg = doc.SelectSingleNode("/pending_messages/pending_message[@id='" + msgId + "']");
            msg.ParentNode.RemoveChild(msg);

            doc.Save(Path);
        }
    }
}
