namespace ChadProgram
{
    partial class FriendsForm
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
            this.lstRequests = new System.Windows.Forms.ListBox();
            this.lstFriends = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstRequests
            // 
            this.lstRequests.FormattingEnabled = true;
            this.lstRequests.ItemHeight = 15;
            this.lstRequests.Location = new System.Drawing.Point(299, 12);
            this.lstRequests.Name = "lstRequests";
            this.lstRequests.Size = new System.Drawing.Size(243, 319);
            this.lstRequests.TabIndex = 0;
            // 
            // lstFriends
            // 
            this.lstFriends.FormattingEnabled = true;
            this.lstFriends.ItemHeight = 15;
            this.lstFriends.Location = new System.Drawing.Point(31, 12);
            this.lstFriends.Name = "lstFriends";
            this.lstFriends.Size = new System.Drawing.Size(247, 319);
            this.lstFriends.TabIndex = 1;
            // 
            // FriendsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 342);
            this.Controls.Add(this.lstFriends);
            this.Controls.Add(this.lstRequests);
            this.Name = "FriendsForm";
            this.Text = "FriendsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox lstRequests;
        private ListBox lstFriends;
    }
}