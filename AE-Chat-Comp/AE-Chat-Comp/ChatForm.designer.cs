namespace AE_Chat_Comp
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Göran");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Pelle");
            this.listViewOthers = new System.Windows.Forms.ListView();
            this.textBoxServerStatus = new System.Windows.Forms.TextBox();
            this.tabControlConversations = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // listViewOthers
            // 
            this.listViewOthers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewOthers.CheckBoxes = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.listViewOthers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewOthers.Location = new System.Drawing.Point(12, 58);
            this.listViewOthers.MultiSelect = false;
            this.listViewOthers.Name = "listViewOthers";
            this.listViewOthers.Size = new System.Drawing.Size(121, 380);
            this.listViewOthers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewOthers.TabIndex = 1;
            this.listViewOthers.UseCompatibleStateImageBehavior = false;
            this.listViewOthers.View = System.Windows.Forms.View.List;
            this.listViewOthers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewOthers_ItemChecked);
            // 
            // textBoxServerStatus
            // 
            this.textBoxServerStatus.BackColor = System.Drawing.Color.DarkRed;
            this.textBoxServerStatus.Location = new System.Drawing.Point(12, 12);
            this.textBoxServerStatus.Multiline = true;
            this.textBoxServerStatus.Name = "textBoxServerStatus";
            this.textBoxServerStatus.ReadOnly = true;
            this.textBoxServerStatus.Size = new System.Drawing.Size(121, 40);
            this.textBoxServerStatus.TabIndex = 2;
            this.textBoxServerStatus.Text = "Disconnected";
            this.textBoxServerStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabControlConversations
            // 
            this.tabControlConversations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlConversations.Location = new System.Drawing.Point(139, 12);
            this.tabControlConversations.Name = "tabControlConversations";
            this.tabControlConversations.SelectedIndex = 0;
            this.tabControlConversations.Size = new System.Drawing.Size(649, 426);
            this.tabControlConversations.TabIndex = 4;
            this.tabControlConversations.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControlConversations_Selected);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlConversations);
            this.Controls.Add(this.textBoxServerStatus);
            this.Controls.Add(this.listViewOthers);
            this.Name = "ChatForm";
            this.Text = "AE Chatt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listViewOthers;
        private System.Windows.Forms.TextBox textBoxServerStatus;
        private System.Windows.Forms.TabControl tabControlConversations;
    }
}