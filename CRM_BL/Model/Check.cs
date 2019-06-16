using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
    public class Check
    {
        public int Checkid { get; set; }
        public int Customerid { get; set; }
        public virtual Customer Customer { get; set; }
        public int Sellerid { get; set; }
        public virtual Seller Seller { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Sell> Sells { get; set; }
        public decimal Price { get; set; }
        public override string ToString()
        {
            return $"N{Checkid}from{Created.ToString("dd.MM.yy hh:mm")}";
        }
    }
}
