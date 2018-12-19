namespace AE_Chat_Comp
{
    using System;
    using System.IO;
    using System.Xml;

    static class Configurator
    {
        public static Uri Address { get; set; }
        public static string ConfigPath { get; set; } = "configuration.config";
        public static string PendingMessagesPath { get; set; } = "pending_messages.xml";

        public static void Initialize()
        {
            if (!File.Exists(ConfigPath))
            {
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, IndentChars = "\t" };
                using (XmlWriter writer = XmlWriter.Create(ConfigPath, settings))
                {
                    writer.WriteStartElement("configuration");
                    writer.WriteElementString("address", "http://10.110.226.181/AEChatt/AE.php");
                    writer.WriteEndElement();
                }
            }
            if (!File.Exists(PendingMessagesPath))
            {
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, IndentChars = "\t" };
                using (XmlWriter writer = XmlWriter.Create(PendingMessagesPath, settings))
                {
                    writer.WriteStartElement("pending_messages");
                    writer.WriteEndElement();
                }
            }

            using (XmlReader reader = XmlReader.Create(ConfigPath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "address":
                                reader.Read();
                                Address = new Uri(reader.Value);
                                break;
                        }
                    }
                }
            }
        }
    }
}
