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
    public partial class Form4 : Form
    {   private int qinxex=0;
        private static int qnumber;
        private int userId;
        qlist[] ql;
        public struct qlist
        {
            public int testQuestionNumber;
            public string question, rs1, rs2, rs3, rs4;
            public int correctRsNumber;
            public qlist(int testQN, string qn, string r1, string r2, string r3, string r4, int corRN)
            {
                testQuestionNumber = testQN;
                question = qn;
                rs1 = r1;
                rs2 = r2;
                rs3 = r3;
                rs4 = r4;
                correctRsNumber = corRN;
            }
        }

        public Form4(string user, int id)
        {
            this.userId = id;
            InitializeComponent();
            InitializeLowPanel(user, id);
            
        }
        private void InitializeLowPanel(string user, int id)
        {
            label1.Text = label1.Text + " " + id.ToString();
            label5.Text = label5.Text + " " + user;
            label2.Text = label2.Text + " Professor";
            
        }

        

        private void createTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var myForm6 = new Form6(this.userId, this);
            myForm6.Show();
            
        }
        public void InitializePanel1(string testName, int qnr, string sclass)
        {
            qnumber = qnr;
            ql = new qlist[qnumber];
            progressBar1.Maximum = qnumber;
            

            this.label6.Text = this.label6.Text + " " + testName;
            this.label7.Text = this.label7.Text + " " + qnumber;
            this.label8.Text = this.label8.Text + " " + sclass;
            this.label9.Text = this.label9.Text + " " + (qinxex+1).ToString();
            
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {   
           ql[qinxex] = new qlist(qinxex+1, richTextBox1.Text, textBox1.Text, textBox2.Text
               , textBox3.Text, textBox4.Text, 1);
           
            
            //ql++ va incrementa si va afisa ce are in urmatorul index daca are 
            
            ++qinxex;
           
            try {
                progressBar1.Value = qinxex+1;
                if (ql[qinxex].testQuestionNumber == 0)
                {
                    richTextBox1.Text = string.Empty;
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    textBox4.Text = string.Empty;
                    this.label9.Text = "Question number " + (qinxex + 1).ToString();
                }
                else
                {
                    richTextBox1.Text = ql[qinxex].question.ToString();
                    textBox1.Text = ql[qinxex].rs1;
                    textBox2.Text = ql[qinxex].rs2;
                    textBox3.Text = ql[qinxex].rs3;
                    textBox4.Text = ql[qinxex].rs4;
                    this.label9.Text = "Question number " + (qinxex + 1).ToString();
                
                
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not increment more.");
                richTextBox1.Text = ql[--qinxex].question.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ql[qinxex] = new qlist(qinxex + 1, richTextBox1.Text, textBox1.Text, textBox2.Text
               , textBox3.Text, textBox4.Text, 1);
            --qinxex;
            
            try {
                progressBar1.Value = qinxex+1;
                richTextBox1.Text = ql[qinxex].question.ToString();
                textBox1.Text = ql[qinxex].rs1;
                textBox2.Text = ql[qinxex].rs2;
                textBox3.Text = ql[qinxex].rs3;
                textBox4.Text = ql[qinxex].rs4;
                this.label9.Text = "Question number " + (qinxex + 1).ToString();
            }
            catch (Exception) 
            {
                
                richTextBox1.Text = ql[++qinxex].question.ToString();
                progressBar1.Value = qinxex + 1;
                MessageBox.Show("Can not decrement more.");
                
                
            }
           
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ql[qinxex] = new qlist(qinxex + 1, richTextBox1.Text, textBox1.Text, textBox2.Text
               , textBox3.Text, textBox4.Text, 1);
            string querry = null;
            DBConnect db = new DBConnect();
            foreach (qlist element in ql)
            {
                querry = "INSERT INTO questions (id, test_id, question, rs1, rs2, rs3, rs4, correct)" +
                "VALUES ('" + userId + "','" + db.GetTestId() + "','" + element.question + "','" + element.rs1 + "','"
                + element.rs2 + "','"+ element.rs3 + "','" + element.rs4 + "','"+ element.correctRsNumber + "')";
                try
                {
                    db.Insert(querry);
                }
                catch
                {
                    MessageBox.Show("Question not added.");
                }
            }
            MessageBox.Show("Test uploaded.");
            panel1.Visible = false;

        }
    }
}
