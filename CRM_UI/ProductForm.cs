using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM_BL.Model;

namespace CRM_UI
{
    public partial class ProductForm : Form
    {
        public Product Product { get; set; }

        public ProductForm()
        {
            InitializeComponent();
        }
        public ProductForm(Product product):this()
        {
            Product = product;
            textBox1.Text = Product.Name;
            numericUpDown1.Value = Product.Price;
            numericUpDown2.Value = Product.Count;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pr = Product ?? new Product();
            pr.Name = textBox1.Text;
            pr.Price = numericUpDown1.Value;
            pr.Count = Convert.ToInt32(numericUpDown2.Value);
           
            Close();
        }
    }
}
