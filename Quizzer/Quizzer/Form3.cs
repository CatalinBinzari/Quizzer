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
        public int numberOfQuestions;
        qlist[] ql;
        public struct qlist
        {
            public int testQuestionNumber;
            public string question, rs1, rs2, rs3, rs4;
            public qlist(int testQN, string qn, string r1, string r2, string r3, string r4)
            {
                testQuestionNumber = testQN;
                question = qn;
                rs1 = r1;
                rs2 = r2;
                rs3 = r3;
                rs4 = r4;
            }
        }
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
            var myForm7 = new Form7(className, this);
            myForm7.Show();
        }
        public void InitializeForm3(int test_id, string test_name)
        {
            DBConnect db = new DBConnect();
            numberOfQuestions = db.GetNumberOfQuestions(test_id);
            ql = new qlist[numberOfQuestions];

            //in ql
            //select question,rs1,rs2,rs3,rs4 from questions where `test_id` = '82';
            // in care vom stoca intrebarile

        }
    }
}
