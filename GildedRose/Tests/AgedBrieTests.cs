using NUnit.Framework;
using System.Collections.Generic;

namespace GildedRose.Tests
{
    [TestFixture]
    public class AgedBrieTests
    {
        [Test]
        public void AgebBrieIncreasesInQuality()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 10 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual(11, items[0].Quality);
        }

        [Test]
        public void AgedBrieQualityIncreasesByTwoAfterSellByDate()
        {
            IList<Item> items = new List<Item>
            {
                new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 },
                new Item { Name = "Aged Brie", SellIn = -3, Quality = 10 },
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual(12, items[0].Quality, "SellBy == 0");
            Assert.AreEqual(12, items[1].Quality, "Negative SellBy value");
        }
    }
}
