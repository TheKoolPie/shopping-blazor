using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Shopping.Shared.Data
{
    public class ShoppingList : BaseItem
    {
        public DateTime ListDate { get; set; }
        public string OwnerId { get; set; }
        public List<string> UserGroupIds { get; set; }
        [NotMapped]
        public List<UserGroup> UserGroups { get; set; }

        public List<ShoppingListItem> Items { get; set; }



        public ShoppingList()
        {
            UserGroupIds = new List<string>();
            UserGroups = new List<UserGroup>();
            Items = new List<ShoppingListItem>();
        }
        public ShoppingList(ShoppingList list) : base(list)
        {
            this.ListDate = list.ListDate;
            this.OwnerId = list.OwnerId;
            this.UserGroupIds = new List<string>(list.UserGroupIds);
            this.UserGroups = new List<UserGroup>(list.UserGroups);
            this.Items = new List<ShoppingListItem>(list.Items);
        }
        public void AddOrUpdateItem(ShoppingListItem item)
        {
            var existing = Items.FirstOrDefault(i => i.ProductItemId == item.ProductItemId);
            if (existing == null)
            {
                Items.Add(new ShoppingListItem(item));
            }
            else
            {
                existing.Amount = item.Amount;
                existing.Done = item.Done;
            }
        }
        public void AddUserGroup(UserGroup group)
        {
            if (!UserGroupIds.Any(i => i == group.Id))
            {
                UserGroupIds.Add(group.Id);
                UserGroups.Add(group);
            }
        }

        public void RemoveItem(string itemId)
        {
            var remItem = Items.FirstOrDefault(i => i.Id == itemId);
            if (remItem != null)
            {
                Items.Remove(remItem);
            }
        }

        public void RemoveUserGroup(string groupId)
        {
            if (UserGroupIds.Any(i => i == groupId))
            {
                UserGroupIds.Remove(groupId);
            }
        }

        public void Update(ShoppingList list)
        {
            this.ListDate = list.ListDate;
            this.OwnerId = list.OwnerId;
            this.UserGroupIds = new List<string>(list.UserGroupIds);
            this.Items = new List<ShoppingListItem>(list.Items);
        }

        public bool HasDifferentItems(List<ShoppingListItem> items)
        {
            bool hasDifferentItems = false;

            var current = this.Items.OrderBy(x => x.Id).ToList();
            var upd = items.OrderBy(x => x.Id).ToList();
            if (current.Count != upd.Count)
            {
                hasDifferentItems = true;
            }
            else
            {
                for (int i = 0; i < current.Count; i++)
                {
                    if (current[i] != upd[i])
                    {
                        hasDifferentItems = true;
                        break;
                    }
                }
            }
            return hasDifferentItems;
        }

        public bool HasDifferentUserGroupIds(List<string> compareIds)
        {
            var hasDifferentGroupIds = false;

            var current = this.UserGroupIds.OrderBy(x => x).ToList();
            var upd = compareIds.OrderBy(x => x).ToList();
            if (current.Count != upd.Count)
            {
                hasDifferentGroupIds = true;
            }
            else
            {
                for (int i = 0; i < current.Count; i++)
                {
                    if (current[i] != upd[i])
                    {
                        hasDifferentGroupIds = true;
                        break;
                    }
                }
            }
            return hasDifferentGroupIds;
        }

        public override bool Equals(object obj)
        {
            return obj is ShoppingList list &&
                   Id == list.Id &&
                   ListDate == list.ListDate &&
                   OwnerId == list.OwnerId &&
                   !HasDifferentUserGroupIds(list.UserGroupIds) &&
                   !HasDifferentItems(list.Items);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ListDate, OwnerId, UserGroupIds, Items);
        }
    }
}
