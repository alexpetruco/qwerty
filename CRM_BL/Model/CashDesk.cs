using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
    public class CashDesk
    {
        CRMcontext db ;
        public int NumCash { get; set; }
        public Seller Seller { get; set; }// = new Seller();
        public Queue<Cart> Queue { get; set; } //= new Queue<Cart>();
        public int MaxQueueLength { get; set; }
        public int ExitCustomer { get; set; }
        public bool Ismodel { get; set; }
        public int Count
        {
            get { return Queue.Count; } //можно так public int Count =>Queue.Count;
            set { value = 1; }
        }
        //public int Count => Queue.Count;
        public event EventHandler<Check> Checkclosed;

        public CashDesk(int _numcash,Seller _seller,CRMcontext db)
        {
            NumCash = _numcash;
            Seller = _seller;
            Queue = new Queue<Cart>();
            Ismodel = true;
            MaxQueueLength = 10;
            this.db = db ?? new CRMcontext() ;

        }

        public void AddInQueue(Cart _cart)
        {
            if (Queue.Count < MaxQueueLength)
            {
                Queue.Enqueue(_cart);
            }
            else
            {
                ExitCustomer++;
            }
        }
        public decimal ExctractQueue()
        {
            decimal sum = 0;
            if(Queue.Count==0)
            {
                return 0;
            }
            var deqcart = Queue.Dequeue();
            if (deqcart != null)
            {
                var check = new Check()
                {
                    Sellerid = Seller.Sellerid,
                    Seller = Seller,
                    Customerid = deqcart.Customer.Customerid,
                    Customer = deqcart.Customer,
                    Created = DateTime.Now
                };
                if (!Ismodel)
                {
                    db.Checks.Add(check);
                    db.SaveChanges();
                }
                else
                {
                    check.Checkid = 0;
                }
                var sells = new List<Sell>();
                foreach (Product product in deqcart)
                {
                    if (product.Count > 0)
                    {
                        var sell = new Sell()
                        {
                            Checkid = check.Checkid,
                            Check = check,
                            Productid = product.Productid,
                            Product = product
                        };

                        sells.Add(sell);
                        if (!Ismodel)
                        {
                            db.Sells.Add(sell);
                        }

                        product.Count--;

                        sum += product.Price;
                        
                    }
                    
                }
                check.Price = sum;

                if (!Ismodel)
                {
                    db.SaveChanges();

                }
                Checkclosed?.Invoke(this, check);
            }

            return sum;
        }
        public override string ToString()
        {
            return $"CashWindow{NumCash }";
        }
    }
}
