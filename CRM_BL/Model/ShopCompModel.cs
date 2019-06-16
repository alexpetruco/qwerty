using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
    public class ShopCompModel
    {  
        Generator generator = new Generator();
        Random rand1 = new Random();
        bool flag = false;
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
            flag = true;
            Task.Run(()=> CreateCart(10,1000));    //=>лямбда выражение в качестве лямбд выражения передаем метод       

            var cashdesktask = CashDesks.Select(c => new Task(() => CashDeskWork(c, 1000)));
            foreach (var _task in cashdesktask)
            {
                _task.Start();
            }

        }
        public void Stop()
        {
            flag = false;
        }
        private void CashDeskWork(CashDesk _cashDesk,int _sleep)
        {
            if(_cashDesk.Count>0)
            {
                _cashDesk.ExctractQueue();
                Thread.Sleep(_sleep);
            }
        }
   

        private void CreateCart(int _customercount,int _sleep)
        {
            while (true)
            {
                var customers_ = generator.GetNEwCustomers(_customercount);
                
                foreach (var custom in customers_)
                {
                    var cart_ = new Cart(custom);
                    foreach(var product_ in generator.GetRandomProducts(10,30) )
                    {
                        cart_.AddProduct(product_);
                    }
                    var cash1 = CashDesks[rand1.Next(CashDesks.Count - 1)];
                    cash1.AddInQueue(cart_);

                }
                Thread.Sleep(_sleep);
            }
        }
    }
}
