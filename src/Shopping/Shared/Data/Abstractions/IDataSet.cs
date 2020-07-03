using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopping.Shared.Data.Abstractions
{
    public interface IDataSet<T> where T : class
    {
        Task<List<T>> ToListAsync();
        List<T> ToList();
        void Add(T entity);
        void Remove(T entity);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        int Count { get; }
    }
}
