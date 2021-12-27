using System;
using System.Collections.Generic;
using System.IO;
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
            categoryRepository = productCategoryContext;//8486824972//0033
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
        public ActionResult Create(Product product,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//content//ProductImages//") + product.Image);
                }
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
        public ActionResult Edit(Product product,string Id,HttpPostedFileBase file)
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

                if (file != null)
                {
                    ProductToBeEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//content//ProductImages//") + ProductToBeEdit.Image);
                }

                ProductToBeEdit.Description = product.Description;
                ProductToBeEdit.Category = product.Category;
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