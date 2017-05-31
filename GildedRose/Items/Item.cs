using GildedRose.Strategies;
using System;

namespace GildedRose.Items
{
    public abstract class Item
    {
        public String Name { get; protected set; }
        public Int32 SellIn { get; protected set; }
        public Int32 Quality { get; protected set; }
        public ItemStrategy Strategy { get; protected set; }

        public Item(String name, Int32 quality, Int32 sellIn)
        {
            Name = name;
            Quality = quality;
            SellIn = sellIn;

            Strategy = new DefaultItemStrategy();
        }

        public abstract void UpdateQuality();

        protected void IncreaseItemQualityBy(Int32 value)
        {
            var valueToIncrease = value * Strategy.QualityModifier;
            if (Quality + valueToIncrease > 50)
                Quality = 50;
            else
                Quality += valueToIncrease;
        }

        protected void DecreaseQualityBy(Int32 value)
        {
            var valueToDecrease = value * Strategy.QualityModifier;
            if (Quality - valueToDecrease < 0)
                Quality = 0;
            else
                Quality -= valueToDecrease;
        }
    }
}
