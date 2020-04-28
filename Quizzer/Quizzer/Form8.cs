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
    public partial class Form8 : Form
    {
        public Form3.qlist[] ql;
        public int student_id;
        public int test_id;
        public Form3 _form3;
        public Form8(Form3.qlist[] ql, Form3 form3, int student_id, int test_id)
        {
            this._form3 = form3;
            this.ql = ql; ;
            this.student_id = student_id;
            this.test_id = test_id;
            InitializeComponent();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Questions:", 160);
            listView1.Columns.Add("Answers:", 160);
            listView1.View = View.Details;
            string[] arr = new string[2];
            ListViewItem itm;
            foreach (Form3.qlist element in ql)
            {   
                arr[0] = element.testQuestionNumber.ToString();
                arr[1] = element.chosenAnswer.ToString();
                itm = new ListViewItem(arr);
                listView1.Items.Add(itm);    
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            foreach (Form3.qlist element in ql)
            {
                string querry = "";
                querry = "INSERT INTO responses (student_id, test_id, question_index, response_index)" +
                "VALUES ('" + student_id + "','" + test_id + "','" + element.testQuestionNumber + "','" + element.chosenAnswer + "')";
                try
                {
                    db.Insert(querry);
                }
                catch
                {
                    MessageBox.Show("Response not added.");
                }

            }
            MessageBox.Show("Submitted.");
            _form3.panel2.Visible = false;
            Form8.ActiveForm.Dispose();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8.ActiveForm.Dispose();
        }
    }
}
