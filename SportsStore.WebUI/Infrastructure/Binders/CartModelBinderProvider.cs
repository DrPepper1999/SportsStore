using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            IModelBinder binder = new CartModelBinder();
            return context.Metadata.ModelType == typeof(Cart) ? binder : null;
        }
    }
}
