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
    public partial class Form3 : Form
    {
        public string className;
        public Form3(string login,int id)
        {
            InitializeComponent();
            DBConnect db = new DBConnect();
            className = db.GetStudentClassName(id);
            this.label1.Text = label1.Text + " " + login;
            this.label2.Text = label2.Text + " " + id; 
            this.label3.Text = label3.Text + " " + className;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void openTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var myForm7 = new Form7(className);
            myForm7.Show();
        }
    }
}
