using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace ChadProgram
{
    public partial class ChatWindow : Form
    {
        List<string> chatMessages = new List<string>();
        List<string> chatUsers = new List<string>();


        public static string? Username;
        //SqlConnection conn;
        public ChatWindow(string username)
        {
            InitializeComponent();
            //this.conn = conn;
            ChatWindow.Username = username;
            lblUsername.Text = username;

        }
       
        private void btnSend_Click(object sender, EventArgs e)
        {
            SQLDataLayer dl = new SQLDataLayer();
            dl.SendMessage(txtMessage.Text);
            //SendMessage(txtMessage.Text);
            txtMessage.Clear();
        }

        private void btnGetMessages_Click(object sender, EventArgs e)
        {
            SQLDataLayer dl = new SQLDataLayer();
            lstMessages.DataSource = dl.GetChatMessages();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SQLDataLayer dl = new SQLDataLayer();
            List<string> messages = dl.GetChatMessages();
            if (messages.Count > chatMessages.Count)
            {
            lstMessages.DataSource = messages;
                chatMessages = messages;
                lstMessages.SelectedIndex = lstMessages.Items.Count - 1;
            }
            List<string> users = dl.GetChatUsers();
            if (users.Count > chatUsers.Count)
            {
                chatUsers = users;
                lstUsers.DataSource = chatUsers;
                lstUsers.SelectedIndex = lstUsers.Items.Count - 1;
            }
        }

        private void lstUsers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstUsers.SelectedIndex != -1)
            {
                //DirectMessage dm = new DirectMessage(lstUsers.SelectedValue?.ToString());
                //dm.Show();
                //put this back
                UserForm uf = new UserForm(lstUsers.SelectedValue?.ToString());
                uf.Show();
            }
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {

        }

        private void btnGroups_Click(object sender, EventArgs e)
        {
            //open groupchat form here
            GroupChat gp = new GroupChat();
            gp.Show();
        }

        private void lstUsers_MouseDown(object sender, MouseEventArgs e)
        { //right click
            if (e.Button == MouseButtons.Right)
            {
                FriendsForm f = new FriendsForm();
                f.Show();
            }
        }
    }
}
