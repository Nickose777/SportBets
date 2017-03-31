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
        protected readonly Func<SportBetDbContext> GetContext;

        public RepositoryBase(Func<SportBetDbContext> GetContext)
        {
            this.GetContext = GetContext;
        }

        public void Add(TEntity entity)
        {
            SportBetDbContext context = GetContext();
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            dbSet.Add(entity);
        }
        public void Remove(TEntity entity)
        {
            SportBetDbContext context = GetContext();
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            dbSet.Remove(entity);
        }

        public virtual TEntity Get(int id)
        {
            SportBetDbContext context = GetContext();
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            return dbSet.Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            SportBetDbContext context = GetContext();
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            return dbSet.ToList();
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            SportBetDbContext context = GetContext();
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            return dbSet.Where(predicate).ToList();
        }
    }
}
