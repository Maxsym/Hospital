using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hospital.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital.DAL.Implementation
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private HospitalDbContext context;
        private DbSet<TEntity> dbSet;

        public BaseRepository(HospitalDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete != null)
            {
                dbSet.Remove(entityToDelete);
            }
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entityToUpdate)
        {
           await dbSet.AddAsync(entityToUpdate);
           context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
