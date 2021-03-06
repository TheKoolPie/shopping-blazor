﻿using Shopping.Shared.Model.Account;
using System.Threading.Tasks;

namespace Shopping.Shared.Data.Abstractions
{
    public class StandardShoppingDataRepo : IShoppingDataRepository
    {
        private StandardDataSet<ProductCategory> _categories;
        private StandardDataSet<ProductItem> _products;
        private StandardDataSet<ShoppingList> _shoppingLists;
        private StandardDataSet<UserGroup> _userGroups;
        private StandardDataSet<UserGroupShoppingList> _userGroupShoppingList;
        private StandardDataSet<ShoppingListItem> _shoppinglistItems;
        private StandardDataSet<UserGroupMembers> _userGroupMembers;
        private StandardDataSet<StoreChain> _storeChains;
        private StandardDataSet<Store> _stores;
        private StandardDataSet<StoreProductCategory> _storeProductCategories;

        public StandardShoppingDataRepo()
        {
            _categories = new StandardDataSet<ProductCategory>();
            _products = new StandardDataSet<ProductItem>();
            _shoppingLists = new StandardDataSet<ShoppingList>();
            _userGroups = new StandardDataSet<UserGroup>();
            _userGroupShoppingList = new StandardDataSet<UserGroupShoppingList>();
            _shoppinglistItems = new StandardDataSet<ShoppingListItem>();
            _userGroupMembers = new StandardDataSet<UserGroupMembers>();
            _storeChains = new StandardDataSet<StoreChain>();
            _stores = new StandardDataSet<Store>();
            _storeProductCategories = new StandardDataSet<StoreProductCategory>();
        }

        public IDataSet<ProductCategory> Categories => _categories;

        public IDataSet<ProductItem> Products => _products;

        public IDataSet<UserGroup> UserGroups => _userGroups;

        public IDataSet<ShoppingList> ShoppingLists => _shoppingLists;

        public IDataSet<UserGroupShoppingList> UserGroupShoppingLists => _userGroupShoppingList;

        public IDataSet<ShoppingListItem> ShoppingListItems => _shoppinglistItems;

        public IDataSet<UserGroupMembers> UserGroupMembers => _userGroupMembers;

        public IDataSet<StoreChain> StoreChains => _storeChains;

        public IDataSet<Store> Stores => _stores;

        public IDataSet<StoreProductCategory> StoreProductCategories => _storeProductCategories;

        public async Task SaveChangesAsync()
        {
            await Task.FromResult(true);
        }
    }
}
