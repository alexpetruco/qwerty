using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
    public class ShopCompModel
    {
        Generator generator = new Generator();
        Random rand1 = new Random();
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sells { get; set; } = new List<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();
        public ShopCompModel()
        {
           var sellers= generator.GetNewSeller(20);
            generator.GetNewProduct(1000);
            generator.GetNEwCustomers(100);
            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }
            for (int i=0; i<3;i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count,Sellers.Dequeue()));
            }
        }
        public void Start()
        {
            var _customers = generator.GetNEwCustomers(10);
            var carts = new Queue<Cart>();
            foreach(var custom in _customers)
            {
                var cart_ = new Cart(custom);
                foreach (var prod in generator.GetRandomProducts(10,30))
                {
                    cart_.AddProduct(prod);
                }
                    carts.Enqueue(cart_);
                
            }
            //var cash1 = CashDesks.FirstOrDefault();/////////TODO
            
            while (carts.Count>0)
            {
                var cash1 = CashDesks[rand1.Next(CashDesks.Count - 1)];
                cash1.AddInQueue(carts.Dequeue());
                

            }
            while (true)
            {
                var cash1 = CashDesks[rand1.Next(CashDesks.Count - 1)];
                var money = cash1.ExctractQueue();
            }
            

        }
    }
}
