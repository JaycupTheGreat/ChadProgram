using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChadProgram
{
    public partial class GroupChat : Form
    {
        List<string> chatMessages = new List<string>();
        List<string> chatGroups = new List<string>();

        public GroupChat()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SQLDataLayer dl = new();
            //here use index selected in listbox to get the group to send it to 
            //dl.SendGroupMessage(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //update the listbox with the messages from the group that is the selected index in the groups listbox
            SQLDataLayer dl = new SQLDataLayer();
            //List<string> messages = dl.GetChatMessages();
            //if (messages.Count > chatMessages.Count)
            //{
            //    lstMessages.DataSource = messages;
            //    chatMessages = messages;
            //    lstMessages.SelectedIndex = lstMessages.Items.Count - 1;
            //}

            //should constantly update the groups listbox with all current existing groups
            List<string> groups = dl.GetGroups();
            if (groups.Count > chatGroups.Count)
            {
                chatGroups = groups;
                lstGroups.DataSource = chatGroups;
                lstGroups.SelectedIndex = lstGroups.Items.Count - 1; 
            }
        }
    }
}
