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
              CRMcontext db = new CRMcontext();
             public int NumCash { get; set; }
        public Seller Seller { get; set; } = new Seller();
        public Queue<Cart> Queue_ { get; set; } = new Queue<Cart>();
             public int MaxQueueLength { get; set; }
             public int ExitCustomer { get; set; }
             public bool Ismodel { get; set; }
        public int Count
        {
            get{return Queue_.Count;}
            
        }

        public CashDesk(int _numcash,Seller _seller)
        {
            NumCash = _numcash;
            Seller = _seller;
            Queue_ = new Queue<Cart>();
            Ismodel = true;
            

        }

        public void AddInQueue(Cart _cart)
        {
            if (Queue_.Count <= MaxQueueLength)
            {
                Queue_.Enqueue(_cart);
            }
            else
            {
                ExitCustomer++;
            }
        }
        public decimal ExctractQueue()
        {
            decimal sum = 0;
            if(Queue_.Count==0)
            {
                return 0;
            }
            Cart deqcart = Queue_.Dequeue();
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

                if (!Ismodel)
                {
                    db.SaveChanges();

                }
            }

            return sum;
        }
    }
}
