using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services.Interfaces;
using Shopping.Shared.Services.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations.Repos
{
    public class StoreRepository : IStoreRepository
    {
        private readonly IShoppingDataRepository _context;
        public StoreRepository(IShoppingDataRepository context)
        {
            _context = context;
        }

        public async Task<List<Store>> GetAllAsync()
        {
            var stores = await _context.Stores.ToListAsync();
            return stores;
        }
        public async Task<Store> GetAsync(string id)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.StoreId == id);
            if (store == null)
            {
                throw new ItemNotFoundException(typeof(Store), id);
            }
            return store;
        }
        public async Task<bool> DeleteByIdAsync(string id)
        {
            var existing = await GetAsync(id);
            if (existing == null)
            {
                throw new ItemNotFoundException(typeof(Store), id);
            }
            //Delete Assignemnts also?
            _context.Stores.Remove(existing);
            bool result = false;
            try
            {
                await _context.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public async Task<Store> CreateAsync(Store item)
        {
            if (ItemAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(Store), item.StoreId);
            }
            _context.Stores.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Store> UpdateAsync(string id, Store item)
        {
            if (!ItemCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(Store), item.StoreId);
            }
            var existing = await GetAsync(id);
            existing.Name = item.Name;
            existing.Street = item.Street;
            existing.HouseNumber = item.HouseNumber;
            existing.PostalCode = item.PostalCode;
            existing.City = item.City;
            existing.PriceCategory = item.PriceCategory;
            existing.StoreChainId = item.StoreChainId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public bool ItemAlreadyExists(Store item)
        {
            var stores = _context.Stores.ToList();
            return stores.Any(s =>
                s.StoreId == item.StoreId ||
                s.Name == item.Name ||
                IsSameAddress(s, item)
                );
        }

        public bool ItemCanBeUpdated(Store item)
        {
            var stores = _context.Stores.ToList();
            var withoutItem = stores.Where(s => s.StoreId != item.StoreId).ToList();
            return !(withoutItem.Any(s =>
                IsSameAddress(s, item)
            ));
        }

        private bool IsSameAddress(Store a, Store b)
        {
            return (a.Street == b.Street &&
                    a.HouseNumber == b.HouseNumber &&
                    a.PostalCode == b.PostalCode &&
                    a.City == b.City);
        }
    }
}
