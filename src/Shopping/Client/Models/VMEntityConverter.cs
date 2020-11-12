using Shopping.Shared.Data;

namespace Shopping.Client.Models
{
    public static class VMEntityConverter
    {
        public static Store ToEntity(this StoreCreateViewModel vm)
        {
            var store = new Store();
            store.Name = vm.Name;
            store.Street = vm.Street;
            store.HouseNumber = vm.HouseNumber;
            store.PostalCode = int.Parse(vm.PostalCode);
            store.City = vm.City;
            store.PriceCategory = vm.Category;
            store.StoreChainId = vm.StoreChainId;
            return store;
        }
    }
}
