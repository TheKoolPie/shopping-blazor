using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
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

        public ShoppingDataRepoEf(ShoppingDbContext context)
        {
            _context = context;

            _categories = new EfDataSet<ProductCategory>(_context.Categories);
            _products = new EfDataSet<ProductItem>(_context.Products);
            _userGroups = new EfDataSet<UserGroup>(_context.UserGroups);
            _shoppingLists = new EfDataSet<ShoppingList>(_context.ShoppingLists);
            _userGroupShoppingLists = new EfDataSet<UserGroupShoppingList>(_context.UserGroupShoppingLists);
        }


        public IDataSet<ProductCategory> Categories => _categories;

        public IDataSet<ProductItem> Products => _products;

        public IDataSet<UserGroup> UserGroups => _userGroups;

        public IDataSet<ShoppingList> ShoppingLists => _shoppingLists;

        public IDataSet<UserGroupShoppingList> UserGroupShoppingLists => _userGroupShoppingLists;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
