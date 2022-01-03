using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.Contracts;
using MyShop.Services.Services;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            IRepository<Product> ProductContext = new MockContext<Product>();
            IRepository<Basket> BasketContext = new MockContext<Basket>();
            var httpContext = new MockHttpContext();

            IBasketService basketService = new BasketService(ProductContext,BasketContext);
            var controller = new BasketController(basketService);
          
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);
            controller.AddToBasket("1");

            Basket basket = BasketContext.Collection().FirstOrDefault();

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.FirstOrDefault().ProductId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Product> ProductContext = new MockContext<Product>();
            IRepository<Basket> BasketContext = new MockContext<Basket>();

            ProductContext.Insert(new Product() { Id = "1", Price = 10.00m });
            ProductContext.Insert(new Product() { Id = "2", Price = 15.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });

            BasketContext.Insert(basket);
            
            var httpContext = new MockHttpContext();
            IBasketService basketService = new BasketService(ProductContext, BasketContext);
            var controller = new BasketController(basketService);
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(35.00m, basketSummary.BasketTotal);


        }
    }
}
