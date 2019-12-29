using rentalportal.model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace rentalportal.model.Core
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Items { get; }
        IQueryable<T> ItemsNoTracking { get; }
        IQueryable<T> ItemsIncluding(params Expression<Func<T, object>>[] paths);
        void Add(T item);
        void Delete(T item);
        void Delete(int id);
        T FindById(int id);
        void DeleteRange(IEnumerable<T> entities);
        void AddRange(params T[] items);
    }
}
