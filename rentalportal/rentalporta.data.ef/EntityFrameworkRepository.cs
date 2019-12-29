using Microsoft.EntityFrameworkCore;
using rentalportal.model.Core;
using rentalportal.model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace rentalportal.data.ef
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
    {
        public EntityFrameworkRepository(RentalPortalContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }

        protected DbSet<T> DbSet { get; }

        protected DbContext DbContext { get; }

        public IQueryable<T> Items => DbSet;
        public IQueryable<T> ItemsNoTracking => DbSet.AsNoTracking();

        public IQueryable<T> ItemsIncluding(params Expression<Func<T, object>>[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            return paths.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(DbSet,
                (current, includeProperty) => current.Include(includeProperty));
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Add(item);
        }

        public virtual void Delete(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Remove(item);
        }

        public virtual void Delete(int id)
        {
            var item = FindById(id);
            if (item != null)
            {
                Delete(item);
            }
        }

        public T FindById(int id)
        {
            return DbSet.Find(id);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void AddRange(params T[] items)
        {
            if (items != null && items.Length > 0)
            {
                foreach (var item in items)
                {
                    Add(item);
                }
            }
        }
    }
}
