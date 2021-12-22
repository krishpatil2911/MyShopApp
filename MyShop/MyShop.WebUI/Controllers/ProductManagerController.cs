using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.Contracts;
using MyShop.DataAccess.InMemory.Repositories;


namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        private IRepository<Product> context;
        private IRepository<ProductCategory> categoryRepository;

        public ProductManagerController(IRepository<Product> productContext,IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            categoryRepository = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products =context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = categoryRepository.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product =product;
                viewModel.ProductCategories = categoryRepository.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product,string Id)
        {
            Product ProductToBeEdit = context.Find(Id);
            if (ProductToBeEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                ProductToBeEdit.Description = product.Description;
                ProductToBeEdit.Category = product.Category;
                ProductToBeEdit.Image = product.Image;
                ProductToBeEdit.Name = product.Name;
                ProductToBeEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");


            }
        }

        public ActionResult Delete(string Id)
        {
            Product ProductToDelete = context.Find(Id);
            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductToDelete);
            }
        }



        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product ProductToDelete = context.Find(Id);
            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }


    }
}