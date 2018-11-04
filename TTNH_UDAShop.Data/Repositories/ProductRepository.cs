using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTNH_UDAShop.Data.Infrastructure;
using TTNH_UDAShop.Model.Models;

namespace TTNH_UDAShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        // viết bổ sung
    }
    class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        // viết bổ sung
    }
}
