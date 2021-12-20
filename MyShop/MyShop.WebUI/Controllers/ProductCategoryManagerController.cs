using MyShop.Core.Models;
using MyShop.DataAccess.InMemory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        private InMemoryRepository<ProductCategory> context;

        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> ProductCategorys = context.Collection().ToList();
            return View(ProductCategorys);
        }

        public ActionResult Create()
        {
            ProductCategory ProductCategory = new ProductCategory();
            return View(ProductCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory ProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(ProductCategory);
            }
            else
            {
                context.Insert(ProductCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory ProductCategory = context.Find(Id);
            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory ProductCategory, string Id)
        {
            ProductCategory ProductCategoryToBeEdit = context.Find(Id);
            if (ProductCategoryToBeEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(ProductCategory);
                }

                ProductCategoryToBeEdit.Category = ProductCategory.Category;

                context.Commit();

                return RedirectToAction("Index");


            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory ProductCategoryToDelete = context.Find(Id);
            if (ProductCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductCategoryToDelete);
            }
        }



        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory ProductCategoryToDelete = context.Find(Id);
            if (ProductCategoryToDelete == null)
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