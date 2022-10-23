using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext _dbContext;
        public EFProductRepository(EFDbContext dbContext) =>
            _dbContext = dbContext;
        public IEnumerable<Product> Products { get { return _dbContext.Products; } }
    }
}
