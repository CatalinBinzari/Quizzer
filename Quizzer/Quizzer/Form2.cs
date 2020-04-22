using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quizzer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ActiveControl = button1;
            comboBox1.SelectedItem = "Student";

            //textbox1 gray background
            this.textBox1.Enter += new EventHandler(textBox1_Enter);
            this.textBox1.Leave += new EventHandler(textBox1_Leave);
            textBox1_SetText();

            //textbo2 gray background
            this.textBox2.Enter += new EventHandler(textBox2_Enter);
            this.textBox2.Leave += new EventHandler(textBox2_Leave);
            textBox2_SetText();

            //textbox3 gray background
            this.textBox3.Enter += new EventHandler(textBox3_Enter);
            this.textBox3.Leave += new EventHandler(textBox3_Leave);
            textBox3_SetText();
        }

        protected void textBox1_SetText()
        {
            this.textBox1.Text = "Login";
            textBox1.ForeColor = Color.Gray;
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {   
            if (textBox1.ForeColor == Color.Black)
                return;
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
                textBox1_SetText();
        }

        protected void textBox2_SetText()
        {
            this.textBox2.Text = "Password";
            textBox2.ForeColor = Color.Gray;
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.ForeColor == Color.Black)
                return;
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
                textBox2_SetText();
        }

        protected void textBox3_SetText()
        {
            this.textBox3.Text = "password";
            textBox3.ForeColor = Color.Gray;
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.ForeColor == Color.Black)
                return;
            textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == "")
                textBox3_SetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            if (this.textBox1.Text == "Login" || LoginAlreadyExists())
            {
                IncorectLogin();
            }
            else if(textBox2.Text != textBox3.Text)
            {
                IncorectPassword();
                this.textBox1.BackColor = Color.White;
            }
            else
            {
                this.textBox1.BackColor = Color.White;
                this.textBox2.BackColor = Color.White;
                this.textBox3.BackColor = Color.White;

                SignUp(textBox1.Text,textBox2.Text, ++comboBox1.SelectedIndex);
            }

        }

        public void IncorectLogin()
        {
                this.textBox1.BackColor = Color.Red;
                this.label3.Text = "\nSet login or choose another one.";
        }
        public void IncorectPassword()
        {
            this.textBox2.BackColor = Color.Red;
            this.textBox3.BackColor = Color.Red;
            this.label3.Text =label3.Text + "\nPasswords do not match or \nhave not been set.";
        }
        public bool LoginAlreadyExists()
        {
            DBConnect db = new DBConnect();
            return db.CheckUser(textBox1.Text.ToString());
        }
        public void SignUp(string uname, string password, int rightsLevel)
        {
            DBConnect db = new DBConnect();
            db.InsertUser(uname, password, rightsLevel);
            this.label3.Text = label3.Text + "\nSign up was successful.";

        }

    }
}
