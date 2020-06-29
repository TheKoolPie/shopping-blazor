using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Data.Abstractions
{
    public class StandardDataSet<T> : IDataSet<T> where T : class
    {
        List<T> _data;
        public StandardDataSet()
        {
            _data = new List<T>();
        }

        public void Add(T entity)
        {
            _data.Add(entity);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_data.FirstOrDefault(predicate.Compile()));
        }

        public void Remove(T entity)
        {
            _data.Remove(entity);
        }

        public List<T> ToList()
        {
            return _data;
        }

        public async Task<List<T>> ToListAsync()
        {
            return await Task.FromResult(_data);
        }
    }
}
