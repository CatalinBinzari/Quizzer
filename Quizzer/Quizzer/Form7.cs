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
    public partial class Form7 : Form
    {   public static List<string> test_id_list;
        public static List<string> tests_list;
        public Form3 _form3;

        public Form7(string className, Form3 form3)
        {
            _form3 = form3;
            InitializeComponent();
            DBConnect db = new DBConnect();
            test_id_list = new List<string>();
            tests_list = db.GetUserTests(className, ref test_id_list);
            for (int i =0; i < test_id_list.Count; ++i)
            {
                this.listBox1.Items.Add(String.Format("Test name: {0}; Test id: {1}", tests_list[i], test_id_list[i]));
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //trebuie selectat un test, apoi apasat ok,
            //daca testul nu e selectat, messagebox " selectati testul"
            int index = listBox1.SelectedIndex;
            try {
                Console.WriteLine(test_id_list[index]);
                _form3.InitializeForm3(Int32.Parse(test_id_list[index]), tests_list[index]);
                _form3.panel2.Visible = true;
                //trebuie sa intoarca test index la form3 si sa il facaa visibil
                ActiveForm.Dispose();
            }
            catch(Exception) {
                MessageBox.Show("Select the quizz.");
            }
            //facem un initialize si apelam cu _form3.init();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveForm.Dispose();
        }
    }
}
