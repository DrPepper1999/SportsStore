using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Extensions;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;

        public CartController(IProductRepository repository) =>
            _repository = repository;

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {
                cart.AddItem(product, 1);
                SetCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {
                cart.RemoveLine(product);
                SetCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        private void SetCart(Cart value)
        {
            HttpContext.Session.Set("Cart", value);
        }
    }
}
