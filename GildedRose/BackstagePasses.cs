using System;

namespace GildedRose
{
    public class BackstagePasses : Item
    {
        public BackstagePasses(Int32 quality, Int32 sellIn) : base("Backstage passes to a TAFKAL80ETC concert", quality, sellIn)
        { }

        public override void UpdateQuality()
        {
            if (SellIn <= 0)
                Quality = 0;
            else if (SellIn < 6)
                IncreaseItemQualityBy(3);
            else if (SellIn < 11)
                IncreaseItemQualityBy(2);
            else
                IncreaseItemQualityBy(1);

            SellIn--;
        }
    }
}
