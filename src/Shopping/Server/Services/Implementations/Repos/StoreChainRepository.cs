using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations.Repos
{
    public class StoreChainRepository : IStoreChainRepository
    {
        private readonly IShoppingDataRepository _context;
        public StoreChainRepository(IShoppingDataRepository context)
        {
            _context = context;
        }
        public async Task<List<StoreChain>> GetAllAsync()
        {
            var storeChains = await _context.StoreChains.ToListAsync();
            return storeChains;
        }
        public async Task<StoreChain> GetAsync(string id)
        {
            var storeChain = await _context.StoreChains.FirstOrDefaultAsync(s => s.StoreChainId == id);
            if (storeChain == null)
            {
                throw new ItemNotFoundException(typeof(StoreChain), id);
            }
            return storeChain;
        }
        public async Task<bool> DeleteByIdAsync(string id)
        {
            var existing = await GetAsync(id);
            if (existing == null)
            {
                throw new ItemNotFoundException(typeof(StoreChain), id);
            }

            //Delete Stores also?

            _context.StoreChains.Remove(existing);
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
        public async Task<StoreChain> CreateAsync(StoreChain item)
        {
            if (ItemAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(StoreChain), item.StoreChainId);
            }
            _context.StoreChains.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<StoreChain> UpdateAsync(string id, StoreChain item)
        {
            if (!ItemCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(StoreChain), item.StoreChainId);
            }
            var existing = await GetAsync(id);
            existing.Name = item.Name;
            existing.PriceCategory = item.PriceCategory;
            existing.Logo = item.Logo;
            existing.Url = item.Url;

            await _context.SaveChangesAsync();

            return existing;
        }

        public bool ItemAlreadyExists(StoreChain item)
        {
            var storeChains = _context.StoreChains.ToList();
            return storeChains.Any(s =>
                s.StoreChainId == item.StoreChainId ||
                s.Name == item.Name
                );
        }

        public bool ItemCanBeUpdated(StoreChain item)
        {
            var storeChains = _context.StoreChains.ToList();
            var withOutItem = storeChains.Where(s => s.StoreChainId != item.StoreChainId).ToList();
            return !(withOutItem.Any(s =>
                s.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)
                ));
        }



        public async Task<List<Store>> GetStoresOfChain(string chainId)
        {
            var storesOfChain = await _context.Stores.ToListAsync();
            return storesOfChain
                .Where(s => !string.IsNullOrEmpty(s.StoreChainId) && s.StoreChainId == chainId)
                .ToList();
        }
    }
}
