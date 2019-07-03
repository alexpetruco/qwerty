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
        Cart cart;
        Customer customer;
        CashDesk cashDesk;
        
        public Form1()
        {
            InitializeComponent();
            db = new CRMcontext();
            cart = new Cart(customer);
            cashDesk = new CashDesk(1, db.Sellers.FirstOrDefault(),db)
            {
               Ismodel = false
            };
            
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
            Task.Run(() =>
            {
                listBox1.Invoke((Action)delegate
                {
                    listBox1.Items.AddRange(db.Products.ToArray());
                    UpDateList();
                });
            });
        }

        private void modelingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var form = new ModelForm();
            form.Show();
        }

        private void LIstBox1BoubleClick(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem is Product product)
            {
                cart.AddProduct(product);
                listBox2.Items.Add(product);
                UpDateList();
            }
        }
        private void UpDateList()
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(cart.GetAll().ToArray());
            label1.Text = "Sum"+cart.Price;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (customer != null)
            {
                cashDesk.AddInQueue(cart);
                var price =cashDesk.ExctractQueue();
                listBox2.Items.Clear();
                cart = new Cart(customer);
                MessageBox.Show("purchase successful Sum"+price,"purcahase perfom OK",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
            else
            {

                MessageBox.Show("signIn", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new SIgnIN();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                var tempcustomer = db.Customers.FirstOrDefault(c => c.Name.Equals(form.Customer.Name));
                if (tempcustomer != null)
                {
                    customer = tempcustomer;
                    

                }
                else
                {
                    db.Customers.Add(form.Customer);
                    db.SaveChanges();
                    customer = form.Customer;
                    
                }
                cart.Customer = customer;
            }
            linkLabel1.Text = $"hi,{customer.Name}";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
