using System;

namespace GildedRose.Strategies
{
    public class ItemStrategy
    {
        public Int32 QualityModifier { get; protected set; }
        public Int32 SellInModifier { get; protected set; }

        public ItemStrategy(Int32 qualityModifier = 1, Int32 sellInModifier = 1)
        {
            QualityModifier = qualityModifier;
            SellInModifier = sellInModifier;
        }
    }
}
