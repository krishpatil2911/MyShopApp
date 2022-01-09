using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class OrderController : Controller
    {

        IOrderService OrderService;

        public OrderController(IOrderService orderService)
        {
            OrderService = orderService;
        }

        // GET: Order
        public ActionResult Index()
        {
            List<Order> orders = OrderService.GetOrderList();
            return View(orders);
        }

        public ActionResult UpdateOrder(string Id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            Order order = OrderService.GetOrder(Id);
            return View(order);
        }

        public ActionResult OrderItems(List<OrderItem> items)
        {
           
            return View(items);
        }
        [HttpPost]
        public ActionResult UpdateOrder(Order UpdatedOrder,string Id)
        {
            
            Order order = OrderService.GetOrder(Id);
            order.OrderStatus = UpdatedOrder.OrderStatus;
            OrderService.UpdateOrder(order);
            return RedirectToAction("Index");
        }
    }
}