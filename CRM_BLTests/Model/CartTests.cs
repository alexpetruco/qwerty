using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRM_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model.Tests
{
    [TestClass()]
    public class CartTests
    {
        [TestMethod()]
        public void CartTest()
        {
            //arannge
            var customer = new Customer()
            {
                Customerid = 1,
                Name = "testser"

            };
            var product_ = new Product()
            {
                Productid =1,
                Name = "pr1",
                Price=100,
                Count = 10
               
            };
            var product_2 = new Product()
            {
                Productid =2,
                Name = "pr2",
                Price = 102,
                Count = 11

            };
            var cart = new Cart(customer);
           
            var expectedResult = new List<Product>()
            {
                product_,product_,product_2
            };

            //act
            cart.AddProduct(product_);
            cart.AddProduct(product_);
            cart.AddProduct(product_2);
            var cartResult = cart.GetAll();
            //assert

            Assert.AreEqual(expectedResult.Count, cart.GetAll().Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], cartResult[i]);
            }
        }

       
    }
}