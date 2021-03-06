﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Quizzer
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "Quizzer";
            uid = "root";
            password = "Casiojapanmov1";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public List<string>[] Select()
        {
            string query = "SELECT * FROM users";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["login"] + "");
                    list[2].Add(dataReader["password"] + "");
                    list[3].Add(dataReader["rightsLevel"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        public List<string> GetUserTests(string userClass, ref List<string> test_id_list)
        {
            string query = "select test_id, tableName from usertests where `studentClass` = '"+ userClass + "'";

            //Create a list to store the result
            List<string> list = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    test_id_list.Add(dataReader["test_id"] + "");
                    list.Add(dataReader["tableName"] + "");
                    
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return null;
            }
        }
        public bool CheckUser(String username)
        {
            String tmp="";
            string query = "SELECT exists(select id from users where `login` = '"+username+"')";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    tmp = dataReader[0].ToString();
                }
                
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                if (tmp=="1")
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public bool LoginUser(String username, String password, ref int rightsLEvel,ref int id)
        {   
            String tmp = null;
            string query = "select rightsLEvel, id from users where `login` = '" + username + "' and `password` = '" + password + "'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    tmp = dataReader[0].ToString();
                    id = Int32.Parse(dataReader[1].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                rightsLEvel = Int32.Parse(tmp);

                if (!String.IsNullOrEmpty(tmp))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public void Insert(string query)
        {
            //string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }
        //Insert statement
        public void InsertUser(string uname, string password, int rightsLevel, string studentClass)
        {
            string query = String.Format("insert into users (login, password, rightsLevel, studentClass) values ('{0}', '{1}', '{2}', '{3}')"
                , uname, password, rightsLevel, studentClass);

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }
        
        public string GetTestId()
        {
            String tmp = "";
            string query = "select max(test_id) from usertests";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    tmp = dataReader[0].ToString();
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
                return tmp;
            }
            else
            {
                return null;
            }
        }
        public int GetNumberOfQuestions(int test_id)
        {
            String tmp = "";
            string query = "select numberOfQuestions from usertests where `test_id` = '"+test_id+"'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    tmp = dataReader[0].ToString();
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
                return Int32.Parse(tmp);
            }
            else
            {
                return 0;
            }
        }
        public string GetStudentClassName(int id)
        {
            String tmp = "";
            string query = "select studentClass from users where `id` = '"+id+"'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    tmp = dataReader[0].ToString();
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
                return tmp;
            }
            else
            {
                return null;
            }
        }
        public void GetQuestions(ref Form3.qlist []ql, int test_id)
        {
            
            string query = "select question, rs1, rs2, rs3, rs4 from questions where `test_id` = '"+ test_id +"'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                int index = 0;
                while (dataReader.Read())
                {
                    ql[index].question = dataReader[0].ToString();
                    ql[index].rs1 = dataReader[1].ToString();
                    ql[index].rs2 = dataReader[2].ToString();
                    ql[index].rs3 = dataReader[3].ToString();
                    ql[index].rs4 = dataReader[4].ToString();
                    ql[index].testQuestionNumber = index + 1;
                    ++index;

                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
                //return Int32.Parse(tmp);
            }
            else
            {
                //return 0;
            }
        }


        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
    }
}
