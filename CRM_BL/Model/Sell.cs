using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
   public class Sell
    {
        public int  Sellid{ get; set; }
        public int Checkid { get; set; }
        public int Productid { get; set; }
        public virtual Check Check { get; set; } 
        public virtual Product Product { get; set; }
    }
}
