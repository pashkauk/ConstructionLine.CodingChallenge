using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Proof()
        {
            //This test designed especially to proof that requrements are not mach the test
            //This set up as per readme requrements and should pass extra asserts if tests and requrements are inline
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small, Size.Medium }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            //here we clearly can see that all previouse tests are passed


            //Shirts = List<Shirt> { SmallRedShirt },
            //SizeCounts = List<SizeCount> { Small(1), Medium(0), Large(0)},
            //ColorCounts = List<ColorCount> { Red(1), Blue(0), Yellow(0), White(0), Black(0)}

            var blueCount = results.ColorCounts.First(x => x.Color == Color.Blue).Count;
            //And this assert will be failing, however requrements stating it shoulb be 0
  
            Assert.That(blueCount, Is.EqualTo(0), $"Blue color count shoulbe 0 according requrements");
        }
    }
}
