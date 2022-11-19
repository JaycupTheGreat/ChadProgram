using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ChadProgram
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SQLDataLayer dl = new SQLDataLayer();
            bool loginWorked = dl.Login(txtUsername.Text, txtPassword.Text);
            if (loginWorked)
            {
                ChatWindow chat = new ChatWindow(txtUsername.Text);
                chat.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login didn't work nerd");
            }




            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["localconnection"].ConnectionString);
            //try
            //{
            //    conn.Open();
            //    //we need query and to find out if the username and password exist
            //    string qry = "select count(*) from users where username = '" + txtUsername.Text + "' and password = '" + txtUsername.Text + "'";
            //    //return a single value in executescalar
            //    SqlCommand cmd = new SqlCommand(qry, conn);
            //    //this will return a single value; if 0 user doesnt exist or password is incorrect
            //    int count = (int)cmd.ExecuteScalar();
            //    //if we get a 1 username password worked otherwise display incorrect
            //    if (count == 1)
            //    {
            //        ChatWindow chat = new ChatWindow(txtUsername.Text, conn);
            //        chat.Show();
            //    }
            //    else { MessageBox.Show("Get it right nerd");}
            //}
            //catch
            //{
            //    throw;
            //}
            //finally
            //{
            //    conn.Close();
            //}
        }
    }
}
