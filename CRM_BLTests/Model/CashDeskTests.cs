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
    public class CashDeskTests
    {
        [TestMethod()]
        public void CashDeskTest()
        {
            //arrange
            var customer1 = new Customer()
            {
                Name = "test",
                Customerid = 1
            };
            var customer2 = new Customer()
            {
                Name = "test2",
                Customerid = 2
            };
            var seller = new Seller()
            {
                Name = "test seller",
                Sellerid = 1,
            
            };
            var product_ = new Product()
            {
                Productid = 1,
                Name = "pr1",
                Price = 100,
                Count = 10

            };
            var product_2 = new Product()
            {
                Productid = 2,
                Name = "pr2",
                Price = 105,
                Count = 11

            };
            var cart = new Cart(customer1);

            cart.AddProduct(product_);
            cart.AddProduct(product_);
            cart.AddProduct(product_2);
            var cartResult = cart.GetAll();
           

            var cart2 = new Cart(customer2);

            cart2.AddProduct(product_);
            cart2.AddProduct(product_2);
            cart2.AddProduct(product_2);
            var cartResult2 = cart.GetAll();

            var cashdesk = new CashDesk(1, seller);
            cashdesk.MaxQueueLength = 10;
            cashdesk.AddInQueue(cart);
            cashdesk.AddInQueue(cart2);
            var cart1ExpectedResult = 305;
            var cart2ExpectedResult = 310;

            //act
            var cart1ActualResult = cashdesk.ExctractQueue();
            var cart2ActualResult = cashdesk.ExctractQueue();
            //assert
            Assert.AreEqual(cart1ExpectedResult,cart1ActualResult);
            Assert.AreEqual(cart2ExpectedResult, cart2ExpectedResult);
            Assert.AreEqual(7,product_.Count);
            Assert.AreEqual(8,product_2.Count);

        }

        
    }
}