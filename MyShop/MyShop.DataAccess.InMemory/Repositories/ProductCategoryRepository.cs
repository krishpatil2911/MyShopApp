using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory.Repositories
{
    public class ProductCategoryRepository
    {
        #region constants
        private readonly string Product_Not_Found = "product not found";
        #endregion

        #region properties
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;
        #endregion

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory product)
        {
            productCategories.Add(product);
        }

        public void Update(ProductCategory product)
        {
            ProductCategory productTobeUpdate = productCategories.Find(p => p.Id == product.Id);
            if (productTobeUpdate != null)
            {
                productTobeUpdate = product;
            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory product = productCategories.Find(p => p.Id == id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productToDelete = productCategories.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                productCategories.Remove(productToDelete);
            }
            else
            {
                throw new Exception("product not found");
            }
        }
    }
}
