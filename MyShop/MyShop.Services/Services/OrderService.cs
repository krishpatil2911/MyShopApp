using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services.Services
{
    public class OrderService : IOrderService
    {
        private IRepository<Order> OrderContext;

        public OrderService(IRepository<Order> orderContext)
        {
            OrderContext = orderContext;
        }

        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Price = item.Price,
                    Image = item.Image,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName
                });
            }

            OrderContext.Insert(baseOrder);
            OrderContext.Commit();
        }

        public List<Order> GetOrderList()
        {
            return OrderContext.Collection().ToList();
        }

        public Order GetOrder(string Id)
        {
            return OrderContext.Find(Id);
        }

        public void UpdateOrder(Order UpdatedOrder)
        {
            OrderContext.Update(UpdatedOrder);
            OrderContext.Commit();
        }
    }

}
