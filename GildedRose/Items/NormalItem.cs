using System;

namespace GildedRose.Items
{
    public class NormalItem : Item
    {
        public NormalItem(String name, Int32 quality, Int32 sellIn) : base(name, quality, sellIn)
        { }

        public override void UpdateQuality()
        {
            if (SellIn <= 0)
                DecreaseQualityBy(2);
            else
                DecreaseQualityBy(1);

            SellIn--;
        }
    }
}
