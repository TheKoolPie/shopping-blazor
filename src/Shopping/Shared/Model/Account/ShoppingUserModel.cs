using System;

namespace Shopping.Shared.Model.Account
{
    public class ShoppingUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ShoppingUserSettingsModel Settings { get; set; }


        public ShoppingUserModel() 
        {
            Settings = new ShoppingUserSettingsModel();
        }
        public ShoppingUserModel(ShoppingUserModel user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Settings = user.Settings;
        }

        public override bool Equals(object obj)
        {
            return obj is ShoppingUserModel model &&
                   Id == model.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
