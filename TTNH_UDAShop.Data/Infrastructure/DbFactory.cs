using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTNH_UDAShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private TTNH_UDAShopDbContext dbContext;

        public TTNH_UDAShopDbContext Init()
        {
            return dbContext ?? (dbContext = new TTNH_UDAShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
