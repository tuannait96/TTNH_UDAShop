using TTNH_UDAShop.Data.Infrastructure;
using TTNH_UDAShop.Model.Models;

namespace TTNH_UDAShop.Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide>
    {
    }

    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}