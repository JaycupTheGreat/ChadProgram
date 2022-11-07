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
    public partial class NewGroupForm : Form
    {SQLDataLayer dl = new();
        public NewGroupForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
            dl.RegisterGroup(txtGroupName.Text); //error checking?
            txtGroupName.Enabled = false;
        }

        private void btnAdduser_Click(object sender, EventArgs e)
        {
            dl.RegisterGroupUser(txtGroupName.Text, txtAddUser.Text);
        }
    }
}
