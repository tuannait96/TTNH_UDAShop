using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTNH_UDAShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        TTNH_UDAShopDbContext Init();
    }
}
