namespace AE_Chat_Comp
{
    using System;
    using System.Windows.Forms;
    using System.Xml;

    public partial class LoginForm : Form
    {
        private ChatForm chatForm = new ChatForm();

        public LoginForm()
        {
            InitializeComponent();
            try
            {
                Configurator.Initialize();
            }
            catch(XmlException e)
            {
                //config filen är korrupt
                DialogResult result = MessageBox.Show("\"configuration.config\" har ett ogiltigt format, vill du ha mer information?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                    MessageBox.Show(e.Message, "Ytterligare information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(UriFormatException e)
            {
                //Ogiltig domän
                DialogResult result = MessageBox.Show("Ogiltig domän, kontrollera domänen i \"configuration.config\". Vill du ha mer information?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                    MessageBox.Show(e.Message, "Ytterligare information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            chatForm.FormClosed += (s, e) => { Close(); };
            chatForm.Show();
        }
        
        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (!ValidInput() || ServerCommunicator.Communicating)
                return;

            ServerCommunicator.Communicating = true;
            ServerCommunicator.Register(textBoxUserName.Text, textBoxPassWord.Text);
        }

        private void ButtonLogIn_Click(object sender, EventArgs e)
        {
            if(!ValidInput() || ServerCommunicator.Communicating)
                return;

            ServerCommunicator.Communicating = true;
            ServerCommunicator.Login(textBoxUserName.Text, textBoxPassWord.Text);
        }
        
        private bool ValidInput()
        {
            if (string.IsNullOrEmpty(textBoxUserName.Text.Trim()))
            {
                MessageBox.Show("Ogiltigt användarnamn och/eller lösenord. Dessa fält får inte vara tomma.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }
    }
}
