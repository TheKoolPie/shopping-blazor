using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Shopping.Shared.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopping.Server.Data
{
    public class EfDataSet<T> : IDataSet<T> where T : class
    {
        DbSet<T> _dbSet;

        public int Count => _dbSet.Count();

        public EfDataSet(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public List<T> ToList()
        {
            return _dbSet.ToList();
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
