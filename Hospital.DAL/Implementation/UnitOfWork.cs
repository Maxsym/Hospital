using System.Threading.Tasks;
using Hospital.DAL.Domains;
using Hospital.DAL.Interfaces;

namespace Hospital.DAL.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private HospitalDbContext _dbContext;
        
        public UnitOfWork(HospitalDbContext  context)
        {
            this._dbContext = context;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
