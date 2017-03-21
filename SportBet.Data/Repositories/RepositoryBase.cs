using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SportBetDbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public RepositoryBase(SportBetDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public virtual TEntity Get(int id)
        {
            return dbSet.Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return dbSet;
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }
    }
}
