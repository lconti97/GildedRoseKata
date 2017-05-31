using GildedRose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseTests
{
    [TestClass]
    public class GuildedRoseShopTests
    {
        private const String Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const String AgedBrie = "Aged Brie";
        private const String BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";

        private GildedRoseShop guildedRoseShop;
        
        public GuildedRoseShopTests()
        {
            guildedRoseShop = new GildedRoseShop();
        }

        [TestMethod]
        public void NormalItemQualityDecreasesByOnePerDay()
        {
            var updatedItem = CreateAndUpdateNormalItem("Plain Joe's Sword", 10, 15);
         
   Assert.AreEqual(9, updatedItem.Quality);
        }

        [TestMethod]
        public void NormalItemSellByDateDecreasesByOnePerDay()
        {
            var updatedItem = CreateAndUpdateNormalItem("Plain Joe's Sword", 10, 15);

            Assert.AreEqual(14, updatedItem.SellIn);
        }

        [TestMethod]
        public void NormalItemPastSellInDateQualityDecreasesByTwoPerDay()
        {
            var updatedItem = CreateAndUpdateNormalItem("Plain Joe's Dusty Sword", 10, 0);

            Assert.AreEqual(8, updatedItem.Quality);
        }

        [TestMethod]
        public void NormalItemPastSellInDateWithQualityOneDecreasesByOneAfterOneDay()
        {
            var updatedItem = CreateAndUpdateNormalItem("Plain Joe's Dust Sword", 1, -1);

            Assert.AreEqual(0, updatedItem.Quality);
        }

        [TestMethod]
        public void NormalItemWithZeroQualityRemainsAtZero()
        {
            var updatedItem = CreateAndUpdateNormalItem("Plain Joe's Cardboard Sword", 0, 15);

            Assert.AreEqual(0, updatedItem.Quality);
        }

        [TestMethod]
        public void AgedBrieBeforeSellInDateQualityIncreasesByOnePerDay()
        {
            var agedBrie = new AgedBrie(10, 15);
            guildedRoseShop.Add(agedBrie);

            guildedRoseShop.UpdateQuality();
            var changedBrie = guildedRoseShop.GetItem(AgedBrie);

            Assert.AreEqual(11, changedBrie.Quality);
        }
         
        [TestMethod]
        public void AgedBrieAfterSellInDateQualityIncreasesByTwoPerDay()
        {
            var agedBrie = new AgedBrie(10, 0);
            guildedRoseShop.Add(agedBrie);

            guildedRoseShop.UpdateQuality();
            var updatedAgedBrie = guildedRoseShop.GetItem(AgedBrie);

            Assert.AreEqual(12, updatedAgedBrie.Quality);
        }

        [TestMethod]
        public void AgedBrieBeforeSellInDateWithQualityFiftyDoesNotIncreaseInQuality()
        {
            var agedBrie = new AgedBrie(50, 10);
            guildedRoseShop.Add(agedBrie);

            guildedRoseShop.UpdateQuality();
            var updatedAgedBrie = guildedRoseShop.GetItem(AgedBrie);

            Assert.AreEqual(50, updatedAgedBrie.Quality);
        }

        [TestMethod]
        public void AgedBrieAfterSellInDateQualityDoesNotIncreaseAboveFifty()
        {
            var agedBrie = new AgedBrie(50, -2);
            guildedRoseShop.Add(agedBrie);

            guildedRoseShop.UpdateQuality();
            var updatedAgedBrie = guildedRoseShop.GetItem(AgedBrie);

            Assert.AreEqual(50, updatedAgedBrie.Quality);
        }

        [TestMethod]
        public void AgedBrieAfterSellInDateWithQualityFortyNineIncreasesToFiftyAfterOneDay()
        {
            var agedBrie = new AgedBrie(49, -2);
            guildedRoseShop.Add(agedBrie);

            guildedRoseShop.UpdateQuality();
            var updatedAgedBrie = guildedRoseShop.GetItem(AgedBrie);

            Assert.AreEqual(50, updatedAgedBrie.Quality);
        }

        [TestMethod]
        public void SulfurasBeforeSellInDateQualityDoesNotChange()
        {
            var sulfuras = new Sulfuras();
            guildedRoseShop.Add(sulfuras);

            guildedRoseShop.UpdateQuality();

            var updatedSulfuras = guildedRoseShop.GetItem(Sulfuras);
            Assert.AreEqual(80, updatedSulfuras.Quality);
        }

        [TestMethod]
        public void SulfurasAfterSellInDateQualityDoesNotChange()
        {
            var sulfuras = new Sulfuras();
            guildedRoseShop.Add(sulfuras);

            guildedRoseShop.UpdateQuality();

            var updatedSulfuras = guildedRoseShop.GetItem(Sulfuras);
            Assert.AreEqual(80, updatedSulfuras.Quality);
        }

        [TestMethod]
        public void SulfurasSellInDateIsZero()
        {
            var sulfuras = new Sulfuras();
            guildedRoseShop.Add(sulfuras);

            guildedRoseShop.UpdateQuality();

            var updatedSulfuras = guildedRoseShop.GetItem(Sulfuras);
            Assert.AreEqual(0, updatedSulfuras.SellIn);
        }

        [TestMethod]
        public void BackstagePassesMoreThanTenDaysBeforeSellInDateQualityIncreasesByOnePerDay()
        {
            var backstagePasses = new BackstagePasses(20, 15);
            guildedRoseShop.Add(backstagePasses);

            guildedRoseShop.UpdateQuality();

            var updatedBackstagePasses = guildedRoseShop.GetItem(BackstagePasses);
            Assert.AreEqual(21, updatedBackstagePasses.Quality);
        }

        [TestMethod]
        public void BackstagePassesBetweenTenAndSixDaysInclusivelyBeforeSellInDateQualityIncreasesByTwoPerDay()
        {
            var backstagePasses = new BackstagePasses(20, 10);
            guildedRoseShop.Add(backstagePasses);

            AssertItemQualityIncreasesByGivenIntervalInInclusiveRange(backstagePasses, 2, 10, 6);
        }

        [TestMethod]
        public void BackstagePassesBetweenFiveAndOneDaysInclusivelyBeforeSellInDateQualityIncreasesByThreePerDay()
        {
            var backstagePasses = new BackstagePasses(20, 5);
            guildedRoseShop.Add(backstagePasses);

            AssertItemQualityIncreasesByGivenIntervalInInclusiveRange(backstagePasses, 3, 5, 1);
        }

        [TestMethod]
        public void BackstagePassesAfterSellInDateQualityIsZero()
        {
            var backstagePasses = new BackstagePasses(20, 0);
            guildedRoseShop.Add(backstagePasses);

            guildedRoseShop.UpdateQuality();

            var updatedBackstagePasses = guildedRoseShop.GetItem(BackstagePasses);
            Assert.AreEqual(0, updatedBackstagePasses.Quality);
        }

        [TestMethod]
        public void BackstagePassesWithSellInDateSevenWithQualityFortyNineIncreasesToFiftyAfterOneDay()
        {
            var backstagePasses = new BackstagePasses(49, 7);
            guildedRoseShop.Add(backstagePasses);

            guildedRoseShop.UpdateQuality();

            var updatedBackstagePasses = guildedRoseShop.GetItem(BackstagePasses);
            Assert.AreEqual(50, updatedBackstagePasses.Quality);
        }

        [TestMethod]
        public void BackstagePassesWithSellInDateThreeWithQualityFortyEightIncreasesToFiftyAfterOneDay()
        {
            var backstagePasses = new BackstagePasses(48, 3);
            guildedRoseShop.Add(backstagePasses);

            guildedRoseShop.UpdateQuality();

            var updatedBackstagePasses = guildedRoseShop.GetItem(BackstagePasses);
            Assert.AreEqual(50, updatedBackstagePasses.Quality);
        }

        [TestMethod]
        public void TwoNormalItemsBeforeSellInDateQualityDecreasesByOnePerDay()
        {
            var itemName1 = "Joe's sword of noodliness";
            var itemName2 = "Mario's spicy meatball";

            AddItemToShop(itemName1, 4, 10);
            AddItemToShop(itemName2, 8, 11);

            guildedRoseShop.UpdateQuality();

            var updatedItem1 = guildedRoseShop.GetItem(itemName1);
            var updatedItem2 = guildedRoseShop.GetItem(itemName2);

            Assert.AreEqual(3, updatedItem1.Quality);
            Assert.AreEqual(7, updatedItem2.Quality);
        }

        private void AssertItemQualityIncreasesByGivenIntervalInInclusiveRange(Item item, Int32 qualityInterval, Int32 startSellInDate, Int32 endSellInDate)
        {
            var itemName = item.Name;
            var expected = item.Quality;
            var daysToRun = startSellInDate - endSellInDate + 1;

            for (var i = 0; i < daysToRun; i++)
            {
                guildedRoseShop.UpdateQuality();
                expected += qualityInterval;
                var updatedItem = guildedRoseShop.GetItem(itemName);
                var actual = updatedItem.Quality;
                Assert.AreEqual(expected, actual);
            }
        }

        private Item CreateAndUpdateNormalItem(String name, Int32 quality, Int32 sellIn)
        {
            AddItemToShop(name, quality, sellIn);

            guildedRoseShop.UpdateQuality();

            return guildedRoseShop.GetItem(name);
        }

        private void AddItemToShop(String name, Int32 quality, Int32 sellIn)
        {
            var normalItem = new Item(name, quality, sellIn);
            guildedRoseShop.Add(normalItem);
        }
    }
}
