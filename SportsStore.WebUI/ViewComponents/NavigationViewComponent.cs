using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebUI.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly IProductRepository _repository;

        public NavigationViewComponent(IProductRepository repository) =>
            _repository = repository;

        public IViewComponentResult Invoke()
        {
            var category = Request.Query["category"];

            if (category.Count != 0)
                ViewBag.SelectedCategory = category;
            else ViewBag.SelectedCategory = "";

            var categories = _repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }
    }
}
