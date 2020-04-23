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
    {
        private int userId;

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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void createTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var myForm6 = new Form6(this.userId);
            myForm6.Show();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
