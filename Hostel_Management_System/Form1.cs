using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hostel_Management_System
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ROOMS Myroom = new ROOMS();
            Myroom.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            STUDENT Mystudent = new STUDENT();
            Mystudent.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FEES Myfee = new FEES();
            Myfee.Show();
            this.Hide();
        }
    }
}
