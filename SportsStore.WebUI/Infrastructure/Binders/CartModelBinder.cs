using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Cart cart = null;
            if (bindingContext.HttpContext.Session != null)
                cart = bindingContext.HttpContext.Session.Get<Cart>(sessionKey);

            if (cart == null)
            {
                cart = new Cart();
                if (bindingContext.HttpContext.Session != null)
                    bindingContext.HttpContext.Session.Set(sessionKey, cart);
            }

            bindingContext.Result = ModelBindingResult.Success(cart);

            return Task.CompletedTask;
        }
    }
}
