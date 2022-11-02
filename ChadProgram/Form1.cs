using Microsoft.Data.SqlClient;
using System.Configuration;

namespace ChadProgram
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["remoteconnection"].ConnectionString);
        public Form1()
        {
            InitializeComponent();
            try
            {
               SQLDataLayer layer = new SQLDataLayer();
                layer.SetUpDatabase();
            }
            catch
            {

            }
        }

        

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MessageBox.Show("Success");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Connection failed " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
    }
}