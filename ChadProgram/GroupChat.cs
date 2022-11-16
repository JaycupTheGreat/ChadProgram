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
        SQLDataLayer dl = new SQLDataLayer();
        bool selected = false;

        List<string> chatMessages = new List<string>();
        List<string> chatGroups = new List<string>();

        public GroupChat()
        {
            InitializeComponent();
            lstGroups.DataSource = chatGroups;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (selected)
            {
                dl.SendGroupMessage(lstGroups.SelectedValue.ToString(), txtMessage.Text);
                txtMessage.Text = "";

            }
            lstGroups.DataSource = chatGroups;
            //here use index selected in listbox to get the group to send it to 
            //dl.SendGroupMessage(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //update the listbox with the messages from the group that is the selected index in the groups listbox

            //should constantly update the groups listbox with all current existing groups
            List<string> groups = dl.GetGroups(ChatWindow.Username);
            if (groups.Count > chatGroups.Count)
            {
                chatGroups = groups;
                lstGroups.DataSource = chatGroups;
                lstGroups.SelectedIndex = lstGroups.Items.Count - 1;
            }
            if (lstGroups.SelectedIndex > -1) //if something is selected
            {
                //get the group from the selected index in the listbox
               // List<string> tmp = dl.GetDirectChatMessages(lstGroups.SelectedValue.ToString());
                List<string> tmp = dl.GetGroupChatMessages(lstGroups.SelectedValue.ToString());
                if (tmp.Count > chatMessages.Count || tmp != null)
                {
                    selected = true;
                    chatMessages = tmp;
                    lstMessages.DataSource = chatMessages;
                    lstMessages.SelectedIndex = lstMessages.Items.Count - 1; //reselect the last item
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewGroupForm frm = new NewGroupForm();
            frm.Show();
            lstGroups.DataSource = chatGroups;

        }

        private void lstGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selected = true;
        }
    }
}
