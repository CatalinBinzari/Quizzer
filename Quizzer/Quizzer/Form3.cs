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
        public int qindex = 0;
        public int id;
        public int test_id;
        public Form1 _form1;
        public struct qlist
        {
            public int testQuestionNumber, chosenAnswer;
            public string question, rs1, rs2, rs3, rs4;
            
        }
        public Form3(string login,int id, Form1 form1)
        {
            this._form1 = form1;
            this.id = id;
            InitializeComponent();
            DBConnect db = new DBConnect();
            className = db.GetStudentClassName(id);
            this.label1.Text = label1.Text + " " + login;
            this.label2.Text = label2.Text + " " + id; 
            this.label3.Text = label3.Text + " " + className;

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //previous
            try
            {
                AddAnswer(GetChosenAnswer());
                SwitchQuestion(--qindex);
                progressBar1.Value = qindex+1;
            }
            catch (Exception)
            {
                SwitchQuestion(++qindex);
                MessageBox.Show("Can not decrement more.");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //next button
            try
            {
                AddAnswer(GetChosenAnswer());
                SwitchQuestion(++qindex);
                progressBar1.Value = qindex+1;
            }
            catch (Exception)
            {
                SwitchQuestion(--qindex);
                MessageBox.Show("Can not increment more.");
               
            }



        }

        private void openTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var myForm7 = new Form7(className, this);
            myForm7.Show();
        }
        public void InitializeForm3(int test_id, string test_name)
        {
            this.test_id = test_id;
            DBConnect db = new DBConnect();
            numberOfQuestions = db.GetNumberOfQuestions(test_id);
            ql = new qlist[numberOfQuestions];
            db.GetQuestions(ref ql, test_id);
            label6.Text = label6.Text + " " + test_name;
            label7.Text = label7.Text + " " + numberOfQuestions.ToString();
            label5.Text = label5.Text + " " + test_id;
            SwitchQuestion(qindex);
            progressBar1.Maximum = numberOfQuestions;

            //
            //in ql
            //select question,rs1,rs2,rs3,rs4 from questions where `test_id` = '82';
            // in care vom stoca intrebarile

        }
        public void SwitchQuestion(int qindex)
        {
            label9.Text = "Question number " + (qindex + 1);
            richTextBox1.Text = ql[qindex].question;
            textBox1.Text = ql[qindex].rs1;
            textBox2.Text = ql[qindex].rs2;
            textBox3.Text = ql[qindex].rs3;
            textBox4.Text = ql[qindex].rs4;
            if (ql[qindex].chosenAnswer != 0) { SetChosenAnswer(ql[qindex].chosenAnswer); }
            else EmptyChosenAnswer();
        }
        public void AddAnswer(int answerindex)
        {
            ql[qindex].chosenAnswer = answerindex;
        }
        public int GetChosenAnswer()
        {
            if (radioButton1.Checked) { return 1; }
            else if(radioButton2.Checked) { return 2; }
            else if (radioButton3.Checked) { return 3; }
            else if (radioButton4.Checked) { return 4; }
            else return 0;
        }
        public void SetChosenAnswer(int answer)
        {
            if      (answer == 1) { radioButton1.Checked = true; }
            else if (answer == 2) { radioButton2.Checked = true; }
            else if (answer == 3) { radioButton3.Checked = true; }
            else if (answer == 4) { radioButton4.Checked = true; }
        }
        public void EmptyChosenAnswer()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var myForm8 = new Form8(ql, this, id, test_id);
            myForm8.Show();
            //sumbit list
            //new database answers;
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _form1.Visible = true;
            this.Close();
        }
    }
}
