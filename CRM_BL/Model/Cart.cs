using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
    public class Cart:IEnumerable
    {
       public Customer Customer { get; set; }
        public Dictionary<Product, int> Product { get; set; }
        public decimal Price => GetAll().Sum(p=>p.Price);
       
        public Cart(Customer customer)
        {
            Customer = customer;
            Product = new Dictionary<Product, int>(); // here storage all product
        }
        public void AddProduct(Product product)
        {
            if (Product.TryGetValue(product, out int count))
            {
                Product[product] = ++count;
            }
            else
            {
                Product.Add(product, 1);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var product_ in Product.Keys)
            {
                for (int i = 0; i < Product[product_]; i++)
                {
                    yield return product_;
                }
            }
        }

        public List<Product> GetAll()
        {
            var result = new List<Product>();
            foreach (Product i in this)
            {
                result.Add(i);
            }
            return result;
        }
    }
}
