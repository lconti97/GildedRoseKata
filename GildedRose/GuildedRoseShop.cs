using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose
{
    public class GildedRoseShop
    {
        private IList<Item> items;

        public GildedRoseShop()
        {
            items = new List<Item>();
        }

        public void UpdateQuality()
        {
            foreach (var item in items)
                item.UpdateQuality();
        }

        public void Add(Item item)
        {
            items.Add(item);
        }

        public Item GetItem(String name)
        {
            return items.First(i => i.Name == name);
        }
    }
}
