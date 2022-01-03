using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.Contracts;
using MyShop.WebUI;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            IRepository<Product> ProductContext = new MockContext<Product>();
            IRepository<ProductCategory> ProductCategoryContext = new MockContext<ProductCategory>();

            ProductContext.Insert(new Product());

            HomeController controller = new HomeController(ProductContext,ProductCategoryContext);

            //// Act
            ViewResult result = controller.Index() as ViewResult;
            var ViewModel = (ProductListViewModel)result.ViewData.Model;

            //// Assert
            Assert.AreEqual(1,ViewModel.Products.Count());
        }

    }
}
