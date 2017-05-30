using System;
using System.Collections.Generic;

namespace GuildedRose
{
    public class GuildedRoseShop
    {
        private const String Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string AgedBrie = "Aged Brie";
        public IList<Item> Items;

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                var name = item.Name;
                var isNormalItem = IsNormalItem(name);

                if (isNormalItem)
                {
                    DecreaseQualityByItemQualityBy(item, 1);
                    item.SellIn--;
                    if (item.SellIn < 0)
                        DecreaseQualityByItemQualityBy(item, 1);
                }
                else if (name == BackstagePasses)
                {
                    if (item.SellIn < 6)
                        IncreaseItemQualityBy(item, 3);
                    else if (item.SellIn < 11)
                        IncreaseItemQualityBy(item, 2);
                    else
                        IncreaseItemQualityBy(item, 1);

                    item.SellIn--;

                    if (item.SellIn < 0)
                        item.Quality = 0;
                }
                else if (name == AgedBrie)
                {
                    IncreaseItemQualityBy(item, 1);

                    item.SellIn--;

                    if (item.SellIn < 0)
                        IncreaseItemQualityBy(item, 1);
                }
            }
        }

        private void IncreaseItemQualityBy(Item item, Int32 value)
        {
            if (item.Quality + value > 50)
                item.Quality = 50;
            else
                item.Quality += value;
        }

        private void DecreaseQualityByItemQualityBy(Item item, Int32 value)
        {
            if (item.Quality - value < 0)
                item.Quality = 0;
            else
                item.Quality -= value;
        }

        private Boolean IsNormalItem(String name)
        {
            return name != Sulfuras && name != AgedBrie && name != BackstagePasses;
        }
    }



    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}
