using CRM_BL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_UI
{
    public partial class SIgnIN : Form
    {
        public Customer Customer { get; set; }
        public SIgnIN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer = new Customer()
            {Name = textBox1.Text };
            DialogResult = DialogResult.OK;
        }

        private void SIgnIN_Load(object sender, EventArgs e)
        {

        }
    }
}
