using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class DbInitializer
    {
        public static void Initialize(EFDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
