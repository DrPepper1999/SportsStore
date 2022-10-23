using Microsoft.AspNetCore.Http;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class CartRepository : ICartRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string sessionKey = "Cart";

        public CartRepository(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public Cart Cart { get => _httpContextAccessor.HttpContext.Session.Get<Cart>(sessionKey);
            set => _httpContextAccessor.HttpContext.Session.Set<Cart>(sessionKey , value); }
    }
}
