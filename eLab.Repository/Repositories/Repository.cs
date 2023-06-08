using eLab.Data;
using eLab.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eLab.Repository.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.AddAsync(entity);
        }

        public void AddRange(IList<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Delete(object id)
        {
            var entity = GetById(id);
            dbSet.Remove(entity);
        }

        public void DeleteRange(IList<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public IQueryable<TEntity> GetAll(
                    Expression<Func<TEntity, bool>> filter = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    List<Expression<Func<TEntity, object>>> includeProperties = null,
                    int? page = null,
                    int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (includeProperties != null)
                includeProperties.ForEach(i => query = query.Include(i));

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query.AsQueryable();
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void UpdateRange(IList<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }
    }
}
