using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Exam
{
    public partial class Form1 : Form
    {
        private OleDbCommand run;
        private OleDbConnection connect;
        private OleDbDataReader read;
        private string connection_string = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;
                                                        User ID=01\325;Initial Catalog=MyDb;Data Source=01\SQLEXPRESS";

        public Form1()
        {
            InitializeComponent();
        }


        //Add new user

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connect = new OleDbConnection(connection_string))
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string email = textBox3.Text;
                string user_ID = textBox4.Text;

                connect.Open();
                
                run = new OleDbCommand("INSERT INTO [MyDb].[db_owner].[Users] VALUES ('"+ user_ID +"'," +
                    " '" + username + "'," +
                    " '" + password + "'," +
                    " '" + email + "')", connect);

                run.Parameters.Add(new OleDbParameter("user_id", textBox4.Text));
                run.Parameters.Add(new OleDbParameter("username", textBox1.Text));
                run.Parameters.Add(new OleDbParameter("password", textBox2.Text));
                run.Parameters.Add(new OleDbParameter("email", textBox3.Text));

                run.ExecuteNonQuery();
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            Select_all_users();
        }

        //Select all users

        private void button4_Click(object sender, EventArgs e)
        {
            Select_all_users();
        }

        private void Select_all_users()
        {
            using (connect = new OleDbConnection(connection_string))
            {
                connect.Open();

                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;

                run = new OleDbCommand("SELECT [user_id], [username], [password], [email] FROM [MyDb].[db_owner].[Users]", connect);

                read = run.ExecuteReader();

                while (read.Read())
                {
                    listBox1.Items.Add(read.GetValue(0) + " " + read.GetString(1));
                }
            }
        }

        //Delete user by user id

        private void button3_Click(object sender, EventArgs e)
        {
            using (connect = new OleDbConnection(connection_string))
            {
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                string user_ID = textBox4.Text;

                connect.Open();

                run = new OleDbCommand("DELETE FROM [MyDb].[db_owner].[Users] WHERE [user_id] = " + user_ID, connect)
                run.Parameters.Add(new OleDbParameter("user_ID", textBox4.Text));

                run.ExecuteNonQuery();
            }
            textBox4.Text = string.Empty;

            Select_all_users();
        }

        //Select user by user id
            
        private void button2_Click(object sender, EventArgs e)
        {
            using (connect = new OleDbConnection(connection_string))
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string email = textBox3.Text;
                string user_ID = textBox4.Text;

                connect.Open();

                run = new OleDbCommand("SELECT * FROM [MyDb].[db_owner].[Users] WHERE [user_id] = " + user_ID, connect)
                run.Parameters.Add(new OleDbParameter("user_ID", textBox4.Text));

                read = run.ExecuteReader();

                read.Read();

                textBox1.Text = read["username"].ToString();
                textBox2.Text = read["password"].ToString();
                textBox3.Text = read["email"].ToString();
            }
        }

        //Update user info by id

        private void button5_Click(object sender, EventArgs e)
        {
            using (connect = new OleDbConnection(connection_string))
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string email = textBox3.Text;
                string user_ID = textBox4.Text;

                connect.Open();
                
                run = new OleDbCommand("UPDATE [MyDb].[db_owner].[Users] SET [username] = '" + username + "'," +
                    " [password] = '" + password + "'," +
                    " [email] = '" + email + "'" +
                    " WHERE [user_id] = " + user_ID, connect)


                run.Parameters.Add(new OleDbParameter("username", textBox1.Text));
                run.Parameters.Add(new OleDbParameter("password", textBox2.Text));
                run.Parameters.Add(new OleDbParameter("email", textBox3.Text));
                run.Parameters.Add(new OleDbParameter("user_ID", textBox4.Text));

                run.ExecuteNonQuery();   
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            Select_all_users();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = " input username";
            textBox2.Text = " input password";
            textBox3.Text = " input e-mail";
            textBox4.Text = " input ID";
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connect.Close();
            this.Close();
        }
    }
}
