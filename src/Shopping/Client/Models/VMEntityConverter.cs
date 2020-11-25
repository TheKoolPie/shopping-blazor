using Shopping.Shared.Data;
using System.Security;

namespace Shopping.Client.Models
{
    public static class VMEntityConverter
    {
        public static Store ToEntity(this StoreCreateViewModel vm)
        {
            var store = new Store();
            if (!string.IsNullOrEmpty(vm.StoreId))
            {
                store.StoreId = vm.StoreId;
            }
            store.Name = vm.Name;
            store.Street = vm.Street;
            store.HouseNumber = vm.HouseNumber;
            store.PostalCode = int.Parse(vm.PostalCode);
            store.City = vm.City;
            store.PriceCategory = vm.Category;
            store.StoreChainId = vm.StoreChainId;
            return store;
        }
        public static StoreCreateViewModel ToCreateViewModel(this Store model)
        {
            var vm = new StoreCreateViewModel();
            vm.StoreId = model.StoreId;
            vm.Name = model.Name;
            vm.Street = model.Street;
            vm.HouseNumber = model.HouseNumber;
            vm.PostalCode = model.PostalCode.ToString();
            vm.City = model.City;
            vm.Category = model.PriceCategory;
            vm.StoreChainId = model.StoreChainId;

            return vm;
        }
        public static StoreChain ToEntity(this StoreChainCreateViewModel vm)
        {
            var chain = new StoreChain();
            chain.Name = vm.Name;
            chain.PriceCategory = vm.Category;
            chain.Url = vm.Url;
            return chain;
        }
    }
}
