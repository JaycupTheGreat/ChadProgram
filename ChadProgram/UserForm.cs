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
    public partial class UserForm : Form
    {
        List<string> lstUserInfo = new List<string>(); 
            SQLDataLayer dl = new SQLDataLayer();
        public UserForm(string username)
        {
            InitializeComponent();
            //run sql command here
            
            foreach(var item in dl.GetUserInfo(username))
            {
                lstUserInfo.Add(item);
            }
            lblUsername.Text = username;
            lblFirst.Text = lstUserInfo[0];
            lblLast.Text = lstUserInfo[1];
            lblLastMessage.Text = lstUserInfo[2];
        }
    }
}
