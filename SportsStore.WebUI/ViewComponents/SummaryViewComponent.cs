using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebUI.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session?.Get<Cart>("Cart");

            if (cart == null)
                cart = new Cart();

            return View(cart);
        }
    }
}
