using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Customer> CustomerContext;
        IBasketService basketService;
        IOrderService orderService;
        // GET: Basket

        public BasketController()
        {

        }
        public BasketController(IBasketService basketService,IOrderService orderService,IRepository<Customer> CustomerContext)
        {
            this.basketService = basketService;
            this.orderService = orderService;
            this.CustomerContext = CustomerContext;
        }

        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        } 

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }


        [Authorize]
        public ActionResult CheckOut()
        {
            Customer customer = CustomerContext.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer != null)
            {
                Order order = new Order() { 
                FirstName=customer.FirstName,
                Surname=customer.LastName,
                Email=customer.Email,
                Street=customer.Street,
                City=customer.City,
                State=customer.State,
                ZipCode=customer.Zipcode
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }

        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
            //process payment
            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("ThankYou", new { OrderId = order.Id });
        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}