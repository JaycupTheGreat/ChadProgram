using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ChadProgram
{
   
    public partial class DirectMessage : Form
    { 
        List<string> messages = new List<string>();
        private string otherUser = "";
        public DirectMessage(string otherUser)
        {
            InitializeComponent();
            this.otherUser = otherUser;
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SQLDataLayer dl = new SQLDataLayer();
            dl.SendDirectMessage(this.otherUser, txtMessage.Text);
            txtMessage.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SQLDataLayer dl = new SQLDataLayer();
            List<string> tmp = dl.GetDirectChatMessages(this.otherUser);
            if (tmp.Count > messages.Count)
            {
                messages = tmp;
                lstMessages.DataSource = messages;
                lstMessages.SelectedIndex = lstMessages.Items.Count-1;
            }
        }
    }
}
