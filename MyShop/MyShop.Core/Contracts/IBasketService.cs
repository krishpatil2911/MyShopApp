using MyShop.Core.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace MyShop.DataAccess.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string productId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
        void RemoveFromBasket(HttpContextBase httpContext, string productId);
    }
}