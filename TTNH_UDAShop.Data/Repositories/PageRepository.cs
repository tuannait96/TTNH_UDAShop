using TTNH_UDAShop.Data.Infrastructure;
using TTNH_UDAShop.Model.Models;

namespace TTNH_UDAShop.Data.Repositories
{
    public interface IPageRepository : IRepository<Page>
    {
    }

    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}