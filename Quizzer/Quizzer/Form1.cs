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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = button1;
            //textbox1 gray background
            this.textBox1.Enter += new EventHandler(textBox1_Enter);
            this.textBox1.Leave += new EventHandler(textBox1_Leave);
            textBox1_SetText();

            //textbo2 gray background
            this.textBox2.Enter += new EventHandler(textBox2_Enter);
            this.textBox2.Leave += new EventHandler(textBox2_Leave);
            textBox2_SetText();
        }
            private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnect db = new DBConnect();
                if (db.LoginUser(this.textBox1.Text, this.textBox2.Text)) 
                {
                    Console.WriteLine("Login succesful");
                    base.SetVisibleCore(false);
                    var myForm = new Form3();
                    myForm.Show();
      
                }
                else
                {
                    Console.WriteLine("Login insuccesful");
                    //nu exista userul,trebuie sign up
                }
            }
            catch 
            {
                Console.WriteLine(System.Environment.StackTrace);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            var myForm = new Form2();
            myForm.Show();
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


    }
}
