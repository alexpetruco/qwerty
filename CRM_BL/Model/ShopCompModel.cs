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

        List<Task> tasks = new List<Task>();
        CancellationTokenSource cancellationTokenSource;
        CancellationToken token;
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sells { get; set; } = new List<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();

        public int CustomerSpeed { get; set; } = 100;
        public int CashDeskSpeed { get; set; } = 100;

        public ShopCompModel()
        {
           var sellers= generator.GetNewSellers(20);
            generator.GetNewProducts(1000);
            generator.GetNewCustomers(100);
            cancellationTokenSource = new CancellationTokenSource();
            token = cancellationTokenSource.Token;

            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }
            for (int i=0; i<3;i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count,Sellers.Dequeue(),null));
            }
        }
        public void Start()
        {
            tasks.Add(new Task(() => CreateCart(10, token)));
            tasks.AddRange(CashDesks.Select(c => new Task(() => CashDeskWork(c, token))));
            foreach (var task in tasks)
            {
                task.Start();
            }
        }
        public void Stop()
        {
                cancellationTokenSource.Cancel();
        }
        private void CashDeskWork(CashDesk _cashDesk,CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            if(_cashDesk.Count>0)
            {
                _cashDesk.ExctractQueue();
                Thread.Sleep(1000);
            }
        }
   

        private void CreateCart(int _customercount,CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var customers_ = generator.GetNewCustomers(_customercount);
                
                foreach (var custom in customers_)
                {
                    var cart_ = new Cart(custom);
                    foreach(var product_ in generator.GetRandomProducts(10,30) )
                    {
                        cart_.AddProduct(product_);
                    }
                    var cash1 = CashDesks[rand1.Next(CashDesks.Count)];
                    cash1.AddInQueue(cart_);
                    
                }
                Thread.Sleep(CustomerSpeed);
            }
        }
    }
}
