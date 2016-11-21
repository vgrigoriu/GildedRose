using NUnit.Framework;
using System.Collections.Generic;

namespace GildedRose
{
    [TestFixture]
    public class RegularItemTests
    {
        [Test]
        public void SellInDecreasesByOne()
        {
            IList<Item> items = new List<Item> { new Item { Name = "foo", SellIn = 10, Quality = 10 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual(9, items[0].SellIn);
        }

        [Test]
        public void QualityDecreasesByOne()
        {
            IList<Item> items = new List<Item> { new Item { Name = "foo", SellIn = 10, Quality = 10 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual(9, items[0].Quality);
        }

        [Test]
        public void QualityDecreasesByTwoAfterSellByDate()
        {
            IList<Item> items = new List<Item>
            {
                new Item { Name = "foo", SellIn = 0, Quality = 10 },
                new Item { Name = "foo", SellIn = -3, Quality = 10 },
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual(8, items[0].Quality, "SellBy == 0");
            Assert.AreEqual(8, items[1].Quality, "Negative SellBy value");
        }

        [Test]
        public void QualityIsNeverNegative()
        {
            IList<Item> items = new List<Item>
            {
                new Item { Name = "normal item at 0 quality", SellIn = 10, Quality = 0 },
                new Item { Name = "expired item at 1 quality", SellIn = 0, Quality = 1 }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual(0, items[0].Quality, items[0].Name);
            Assert.AreEqual(0, items[1].Quality, items[1].Name);
        }
    }
}
