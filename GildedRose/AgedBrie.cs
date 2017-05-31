using System;

namespace GildedRose
{
    public class AgedBrie : Item
    {
        public AgedBrie(Int32 quality, Int32 sellIn) : base("Aged Brie", quality, sellIn)
        { }

        public override void UpdateQuality()
        {
            if(SellIn <= 0)
                IncreaseItemQualityBy(2);
            else
                IncreaseItemQualityBy(1);

            SellIn--;
        }

    }
}
