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
    public partial class Form1 : Form
    {
        CRMcontext db;
        public Form1()
        {
            InitializeComponent();
            db = new CRMcontext();
        }

         
        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(db.Products,db);
            catalogProduct.Show();
        }

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(db.Sellers,db);
            catalogSeller.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCustomer = new Catalog<Customer>(db.Customers,db);
            catalogCustomer.Show();
        }

        private void checkOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var checkOrder = new Catalog<Check>(db.Checks,db);
            checkOrder.Show();
        }

        private void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                db.SaveChanges();
            } 
        }

        private void addSellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SellerForm();
            if(form.ShowDialog() == DialogResult.OK)
            {

                db.Sellers.Add(form.Seller);
                db.SaveChanges();
            }

        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                db.SaveChanges();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void modelingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ModelForm();
            form.Show();
        }
    }
}
