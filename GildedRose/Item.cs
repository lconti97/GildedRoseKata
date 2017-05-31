using System;

namespace GildedRose
{
    public class Item
    {
        public String Name { get; protected set; }
        public Int32 SellIn { get; protected set; }
        public Int32 Quality { get; protected set; }

        public Item(String name, Int32 quality, Int32 sellIn)
        {
            Name = name;
            Quality = quality;
            SellIn = sellIn;
        }

        public virtual void UpdateQuality()
        {
            if (SellIn <= 0)
                DecreaseQualityBy(2);
            else
                DecreaseQualityBy(1);

            SellIn--;
        }

        protected void IncreaseItemQualityBy(Int32 value)
        {
            if (Quality + value > 50)
                Quality = 50;
            else
                Quality += value;
        }

        protected void DecreaseQualityBy(Int32 value)
        {
            if (Quality - value < 0)
                Quality = 0;
            else
                Quality -= value;
        }
    }
}
