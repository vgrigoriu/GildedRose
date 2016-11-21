using FsCheck;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Tests
{
    [TestFixture]
    public class RegularItemPropertyTests
    {
        [Test]
        public void SellByDateAlwaysDecreasesByOne()
        {
            Prop.ForAll(
                Arb.From<string>(),
                Arb.From<int>(),
                Arb.From<int>(),
                (name, sellIn, quality) =>
                {
                    var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
                    var items = new List<Item> { item };
                    GildedRose app = new GildedRose(items);
                    app.UpdateQuality();

                    return item.SellIn == sellIn - 1;
                })
                .QuickCheck();
        }

        [Test]
        public void QualityAlwaysDecreasesByOne()
        {
            Prop.ForAll(
                ItemArb.PositiveQualityNonExpiredItems(),
                item =>
                {
                    var originalQuality = item.Quality;
                    var items = new List<Item> { item };
                    GildedRose app = new GildedRose(items);
                    app.UpdateQuality();

                    return item.Quality == originalQuality - 1;
                }).VerboseCheckThrowOnFailure();
        }

        [Test]
        public void ExpiredQualityAlwaysDecreasesByTwo()
        {
            Prop.ForAll(
                ItemArb.PositiveQualityExpiredItems(),
                item =>
                {
                    var originalQuality = item.Quality;
                    var items = new List<Item> { item };
                    GildedRose app = new GildedRose(items);
                    app.UpdateQuality();

                    return item.Quality == Math.Max(originalQuality - 2, 0);
                }).VerboseCheckThrowOnFailure();
        }
    }

    public static class ItemArb
    {
        public static Arbitrary<Item> PositiveQualityNonExpiredItems()
        {
            var genItem = from name in Arb.Generate<string>()
                          from sellIn in Arb.Generate<PositiveInt>()
                          from quality in Arb.Generate<PositiveInt>()
                          select new Item { Name = name, SellIn = sellIn.Item, Quality = quality.Item };

            return genItem.ToArbitrary();
        }

        public static Arbitrary<Item> PositiveQualityExpiredItems()
        {
            var genItem = from name in Arb.Generate<string>()
                          from sellIn in Arb.Generate<NonNegativeInt>()
                          from quality in Arb.Generate<PositiveInt>()
                          select new Item { Name = name, SellIn = - sellIn.Item, Quality = quality.Item };

            return genItem.ToArbitrary();
        }
    }
}
