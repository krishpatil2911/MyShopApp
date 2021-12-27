using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        private IRepository<Product> context;
        private IRepository<ProductCategory> categoryRepository;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            categoryRepository = productCategoryContext;//8486824972//0033
        }
        public ActionResult Index(string category=null)
        {
            List<Product> products;
            List<ProductCategory> ProductCategories = categoryRepository.Collection().ToList();
            if (category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == category).ToList();
            }
            ProductListViewModel productListViewModel = new ProductListViewModel();
            productListViewModel.Products = products;
            productListViewModel.ProductCategories = ProductCategories;

            return View(productListViewModel);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            return View(product);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}