using Shopping.Shared.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopping.Shared.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty(this IList list)
        {
            return !(list != null && list.Count > 0);
        }
        public static void AddOrUpdate(this List<ProductCategory> list, ProductCategory entity)
        {
            var item = list.FirstOrDefault(c => c.Id == entity.Id);
            if (item == null)
            {
                list.Add(entity);
            }
            else
            {
                item.Name = entity.Name;
                item.ColorCode = entity.ColorCode;
            }
        }
        public static void AddOrUpdate(this List<ProductItem> list, ProductItem entity)
        {
            var item = list.FirstOrDefault(p => p.Id == entity.Id);
            if (item == null)
            {
                list.Add(item);
            }
            else
            {
                item.Name = entity.Name;
                item.Unit = entity.Unit;
                item.CategoryId = entity.CategoryId;
                item.Category = entity.Category;
            }
        }
        public static void RemoveIfExists<T>(this List<T> list, string id) where T : BaseItem
        {
            var item = list.FirstOrDefault(c => c.Id == id);
            if(item != null)
            {
                list.Remove(item);
            }
        }
    }
}
