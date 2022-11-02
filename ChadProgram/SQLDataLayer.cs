using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace ChadProgram
{

    public class SQLDataLayer
    {
        string connectionString;
        //this constructor takes a string
        //but also has a default value
        //which means we should be able to call the constructor without parameters
        //if no param it will read from the app.config
        public SQLDataLayer(string connString = "")
        {
            //if connstring is blank
            if (connString == "") //connectionstring from config
                connectionString = ConfigurationManager.ConnectionStrings["localconnection"].ConnectionString;
            else //otherwise whatever was passed in
                connectionString = connString;
        }

        //execute nonquery
        private bool ExecuteNonQuery(string qry)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            bool ret = true;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //query didn't work
                ret = false;
            }
            finally
            {
                //close connection
                conn.Close();
            }
            return ret;
        }

        //execute scalar
        private object ExecuteScalar(string qry)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            object ret = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(qry, conn);
                ret = cmd.ExecuteScalar();
            }
            catch
            {
                //query didn't work
                ret = null;
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        private object ExecuteDataReader(string qry)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(qry, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object tmp = reader[0];
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            //REMOVE THIS
            return null;
        }

        public List<string> GetChatMessages()
        {
            List<string> messages = new List<string>();

            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from chat order by message_date", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //gets the current message from the db because we are reading line by line
                    string currentMessage = reader[0] + ":" + reader[2];
                    messages.Add(currentMessage);
                    //object tmp = reader[0];
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            //ExecuteDataReader("select * from chat");

            return messages;
        }

        public List<string> GetChatUsers()
        {
            List<string> messages = new List<string>();

            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select username from users", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //gets the current message from the db because we are reading line by line
                    string currentUser = reader[0].ToString();
                    messages.Add(currentUser);
                    //object tmp = reader[0];
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            //ExecuteDataReader("select * from chat");

            return messages;
        }

        public bool RegisterUser(string username, string password)
        {
            return this.ExecuteNonQuery("insert into users(username, password, register_date) " +
                "values ('" + username + "','" + password + "',getdate())");
        }

        public bool Login(string username, string password)
        {
            int count = (int)ExecuteScalar($"select count(*) from users where username = '{username}' and password = '{password}' ");

            //if count = 1 we know the user exists
            return count == 1;
        }

        public bool SendMessage(string message)
        {
            bool ret = true;

            string qry = $"insert into chat values('{ChatWindow.Username}',getdate(),'{message}')";
            ret = ExecuteNonQuery(qry);
            return ret;
        }

        public bool SendDirectMessage(string recipient, string message)
        {
            bool ret = true;

            string qry = $"insert into DirectMessage values ('{ChatWindow.Username}','{recipient}',getdate(),'{message}')";
            ret = ExecuteNonQuery(qry);


            return ret;
        }

        public bool GetDirectMessages(string otherUser)
        {
            bool ret = true;


            return ret;
        }

        public void SetUpDatabase()
        {
            string createUser = "create table Users(username varchar(30) primary key, [password] varchar(30), register_date datetime)";
            string createChat = "create table Chat(username varchar(30), message_date datetime primary key(username,message_date), [message] varchar(150), foreign key (username) references users(username))";

            string createDirectMessages = @"create table DirectMessage (
Sender varchar(30),
Receiver varchar(30),
Message_Date datetime,
Message_Content varchar(255)
primary key(Sender, receiver,message_date),
foreign key (sender) references Users(Username),
foreign key (receiver) references Users(Username)
)";

            ExecuteNonQuery(createUser);
            ExecuteNonQuery(createChat);
            ExecuteNonQuery(createDirectMessages);
            ExecuteNonQuery("create table Groups(GroupID int not null identity(1,1), GroupName varchar(20) not null, primary key(GroupID));");
            ExecuteNonQuery("create table GroupUsers(\r\nGroupID int not null, \r\nusername varchar(30) not null, \r\nprimary key(GroupID, username), \r\nforeign key (username) references users(username), \r\nforeign key (groupid) references groups(groupid))");
            ExecuteNonQuery("create table GroupChat(\r\ngroupID int not null,\r\nusername varchar(30) not null, \r\nmessage_date datetime not null,\r\nmessage varchar(150),\r\nprimary key(username, message_date))");
        }

        public List<string> GetDirectChatMessages(string them)
        {
            List<string> messages = new List<string>();

            string qry = $@"select * from DirectMessage 
where (sender = '{ChatWindow.Username}' 
and receiver = '{them}') 
or (Sender = '{them}' and receiver = '{ChatWindow.Username}') 
order by Message_Date asc";


            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(qry, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //gets the current message from the db because we are reading line by line
                    string currentMessage = reader[0] + ":" + reader[3];
                    messages.Add(currentMessage);
                    //object tmp = reader[0];
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            //ExecuteDataReader("select * from chat");

            return messages;
        }


        // TO DO
        public List<string> GetGroups()
        {
            List<string> messages = new List<string>();

            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select groupname from groups", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //gets the current message from the db because we are reading line by line
                    string currentUser = reader[0].ToString();
                    messages.Add(currentUser);
                    //object tmp = reader[0];
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            //ExecuteDataReader("select * from chat");

            return messages;
        }

        public List<string> GetGroupChatMessages(string group)
        {
            List<string> messages = new List<string>();

//            string qry = $@"select * from DirectMessage 
//where (sender = '{ChatWindow.Username}' 
//and receiver = '{them}') 
//or (Sender = '{them}' and receiver = '{ChatWindow.Username}') 
//order by Message_Date asc";


            //SqlConnection conn = new SqlConnection(connectionString);
            //try
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand(qry, conn);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        //gets the current message from the db because we are reading line by line
            //        string currentMessage = reader[0] + ":" + reader[3];
            //        messages.Add(currentMessage);
            //        //object tmp = reader[0];
            //    }
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    conn.Close();
            //}
            //ExecuteDataReader("select * from chat");

            return messages;
        }

        public bool SendGroupMessage(string group, string recipient, string message)
        { //think this should work? no error checking here tho
            bool ret = true;

            string qry = $"insert into GroupChat values (group, '{ChatWindow.Username}','{recipient}',getdate(),'{message}')";
            ret = ExecuteNonQuery(qry);


            return ret;
        }
    }
}
