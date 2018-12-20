namespace AE_Chat_Comp
{
    using System.Drawing;
    using System.Windows.Forms;
    using System;
    using System.Xml;

    public partial class ChatForm : Form
    {
        private TextBox currentSendTextBox;
        private TextBox currentReadTextBox;
        public string Username { get; set; } = "Tölpen"; //tillfällig

        public ChatForm()
        {
            InitializeComponent();
        }
        
        private void ListViewOthers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                TabPage tabPage = new TabPage(e.Item.Text);

                TextBox textBoxRead = new TextBox() { Location = new Point(3, 3), Multiline = true, Size = new Size(635, 314), ReadOnly = true, TabStop = false, Name = "read", ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill };
                tabPage.Controls.Add(textBoxRead);

                TextBox textBoxSend = new TextBox() { Location = new Point(3, 320), Multiline = true, Size = new Size(635, 80), TabStop = false, MaxLength = 800, Name = "send", Dock = DockStyle.Bottom };
                textBoxSend.KeyDown += TextBoxSend_KeyDown;
                tabPage.Controls.Add(textBoxSend);

                tabControlConversations.TabPages.Add(tabPage);
                tabControlConversations.SelectedTab = tabPage;

                currentReadTextBox = textBoxRead;
                currentSendTextBox = textBoxSend;
            }
            else
            {
                //Ta bort taben
                foreach (TabPage tabPage in tabControlConversations.TabPages)
                {
                    if (tabPage.Text == e.Item.Text)
                    {
                        tabControlConversations.TabPages.Remove(tabPage);
                        break;
                    }
                }
            }
        }

        private void TabControlConversations_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage != null)
            {
                e.TabPage.BringToFront();
                /*
                foreach (TextBox textBox in e.TabPage.Controls.OfType<TextBox>())
                {
                    if (textBox.Name == "send")
                        currentSendTextBox = textBox;
                    else if (textBox.Name == "read")
                        currentReadTextBox = textBox;
                }
                */
            }
        }

        private void TextBoxSend_KeyDown(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrEmpty(currentSendTextBox.Text.Trim()))
                {
                    TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                    string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss" + ((utcOffset < TimeSpan.Zero) ? "-" : "+") + utcOffset.Hours.ToString("00") + ":" + utcOffset.Minutes.ToString("00"));
                    int msgId = PendingMessages.AppendPendingMessage(Username, tabControlConversations.SelectedTab.Text, timestamp, currentSendTextBox.Text);
                    currentReadTextBox.AppendText(currentSendTextBox.Text + "\n");
                    currentSendTextBox.Clear();
                    currentSendTextBox.Select(0, 0);
                    ServerCommunicator.SendMessage(Username, tabControlConversations.SelectedTab.Text, timestamp, currentSendTextBox.Text, msgId.ToString());
                }
            }
        }
    }
}
