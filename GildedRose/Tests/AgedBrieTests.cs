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
    }
}
