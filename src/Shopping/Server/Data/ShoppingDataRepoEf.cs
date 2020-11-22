using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Data
{
    public class ShoppingDataRepoEf : IShoppingDataRepository
    {
        private ShoppingDbContext _context;

        private EfDataSet<ProductCategory> _categories { get; set; }
        private EfDataSet<ProductItem> _products { get; set; }
        private EfDataSet<UserGroup> _userGroups { get; set; }
        private EfDataSet<ShoppingList> _shoppingLists { get; set; }
        private EfDataSet<UserGroupShoppingList> _userGroupShoppingLists { get; set; }
        private EfDataSet<ShoppingListItem> _shoppinglistItems { get; set; }
        private EfDataSet<UserGroupMembers> _userGroupMembers { get; set; }
        private EfDataSet<StoreChain> _storeChains { get; set; }
        private EfDataSet<Store> _stores { get; set; }
        private EfDataSet<StoreProductCategory> _storeProductCategories { get; set; }

        public ShoppingDataRepoEf(ShoppingDbContext context)
        {
            _context = context;

            _categories = new EfDataSet<ProductCategory>(_context.Categories);
            _products = new EfDataSet<ProductItem>(_context.Products);
            _userGroups = new EfDataSet<UserGroup>(_context.UserGroups);
            _shoppingLists = new EfDataSet<ShoppingList>(_context.ShoppingLists);
            _userGroupShoppingLists = new EfDataSet<UserGroupShoppingList>(_context.UserGroupShoppingLists);
            _shoppinglistItems = new EfDataSet<ShoppingListItem>(_context.ShoppingListItems);
            _userGroupMembers = new EfDataSet<UserGroupMembers>(_context.UserGroupMembers);
            _storeChains = new EfDataSet<StoreChain>(_context.StoreChains);
            _stores = new EfDataSet<Store>(_context.Stores);
            _storeProductCategories = new EfDataSet<StoreProductCategory>(_context.StoreProductCategories);
        }


        public IDataSet<ProductCategory> Categories => _categories;

        public IDataSet<ProductItem> Products => _products;

        public IDataSet<UserGroup> UserGroups => _userGroups;

        public IDataSet<ShoppingList> ShoppingLists => _shoppingLists;

        public IDataSet<UserGroupShoppingList> UserGroupShoppingLists => _userGroupShoppingLists;

        public IDataSet<ShoppingListItem> ShoppingListItems => _shoppinglistItems;

        public IDataSet<UserGroupMembers> UserGroupMembers => _userGroupMembers;

        public IDataSet<StoreChain> StoreChains => _storeChains;

        public IDataSet<Store> Stores => _stores;

        public IDataSet<StoreProductCategory> StoreProductCategories => _storeProductCategories;
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
