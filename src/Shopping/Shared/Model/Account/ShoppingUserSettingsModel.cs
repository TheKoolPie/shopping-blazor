using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Model.Account
{
    public class ShoppingUserSettingsModel
    {
        public string FirstName;
        public string LastName;
        public string StandardUserGroupId;
        public ShoppingUserSettingsModel()
        {

        }
        public ShoppingUserSettingsModel(ShoppingUserSettingsModel model)
        {
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.StandardUserGroupId = model.StandardUserGroupId;
        }
    }
}
