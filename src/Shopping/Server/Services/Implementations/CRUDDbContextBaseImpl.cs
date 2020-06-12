using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public abstract class CRUDDbContextBaseImpl<T> : ICRUDAccess<T> where T : BaseItem
    {
        protected readonly ShoppingDbContext _context;
        protected readonly ILogger _logger;
        public CRUDDbContextBaseImpl(ShoppingDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<T> CreateAsync(T item)
        {
            if (ItemAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(T), item.Id);
            }

            _context.Add<T>(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new PersistencyException("Could not create item", e);
            }
            return item;
        }

        public async Task<T> UpdateAsync(string id, T item)
        {
            var existingItem = await _context.FindAsync<T>(id);
            if (existingItem == null)
            {
                throw new ItemNotFoundException(typeof(T), id);
            }
            if (!(existingItem.Equals(item)))
            {
                UpdateExistingItem(existingItem, item);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new PersistencyException("Could not update item", e);
                }
            }
            return item;
        }

        public async Task<bool> DeleteAsync(T item)
        {
            return await DeleteByIdAsync(item.Id);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var item = await _context.FindAsync<T>(id);
            if (item == null)
            {
                throw new ItemNotFoundException(typeof(T), id);
            }

            _context.Remove<T>(item);
            await _context.SaveChangesAsync();

            return true;
        }

        public abstract Task<List<T>> GetAllAsync();
        public abstract Task<T> GetAsync(string id);
        public abstract bool ItemAlreadyExists(T item);
        public abstract void UpdateExistingItem(T existing, T update);
    }
}
