using Hospital.DAL.Domains;
using Hospital.DAL.Interfaces;

namespace Hospital.DAL.Implementation
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(HospitalDbContext context) : base(context){}
    }
}
