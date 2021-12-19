using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory.Repositories
{
   public  class ProductRepository
    {
        #region constants
        private readonly string Product_Not_Found = "product not found";
        #endregion

        #region properties
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();
        #endregion

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product product)
        {
            products.Add(product);
        }

        public void Update(Product product)
        {
            Product productTobeUpdate = products.Find(p => p.Id == product.Id);
            if (productTobeUpdate != null)
            {
                productTobeUpdate = product;
            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public Product Find(string id)
        {
            Product product = products.Find(p => p.Id == id);
            if (product != null)
            {
               return  product;
            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("product not found");
            }
        }

    }
}
