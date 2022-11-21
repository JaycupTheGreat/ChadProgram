using Microsoft.Data.SqlClient;
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

namespace ChadProgram
{
    public partial class Register : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["remoteconnection"].ConnectionString);
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //no parameter because we want config
            SQLDataLayer dl = new SQLDataLayer();
           
            bool registerWorked = dl.RegisterUser(txtName.Text, txtPass.Text);
            if (registerWorked)
            {
                MessageBox.Show("Success");
                if (txtFirstName.Text != "")
                {
                    dl.FirstName(txtName.Text, txtFirstName.Text);
                }
                if (txtLastName.Text != "")
                {
                    dl.LastName(txtName.Text, txtLastName.Text);
                }
            }
            else
            {
                MessageBox.Show("You failed to make a new user");
            }
        }
    }
}
