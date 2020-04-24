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
    public partial class Form6 : Form
    {
        private int userId;
        private bool radioButtonState;

        public Form6(int userId)
        {
            this.userId = userId;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6.ActiveForm.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {   progressBar1.PerformStep();
            string querry = null;
            querry = "INSERT INTO usertests (id, numberOfQuestions, tableName, studentClass, instantMark)" +
                "VALUES ('"+ userId + "','"+ numericUpDown1.Value + "','"+ textBox1.Text + "','"+ textBox2.Text + "','"+radioButtonState+"')";
            DBConnect db = new DBConnect();
            db.Insert(querry);
            progressBar1.PerformStep();
            progressBar1.PerformStep();
            progressBar1.PerformStep();

        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton1.Checked)
            {
                radioButtonState = true;
            }
            else if (radioButton2.Checked)
            {
                radioButtonState = false;
            }
        }
    }
}
