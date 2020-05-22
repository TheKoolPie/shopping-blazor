﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Data
{
    public class ShoppingList
    {
        public Guid Id { get; set; }
        public List<ShoppingListItem> Items { get; set; }
        public DateTime Date { get; set; }
    }
}
