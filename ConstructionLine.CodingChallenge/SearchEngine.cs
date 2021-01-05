using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }


        public SearchResults Search(SearchOptions options)
        {
            var result = _shirts.Where(x => options.Sizes.Any(s => s.Id == x.Size.Id) && options.Colors.Any(c => c.Id == x.Color.Id)).ToList();

            var colors = new List<ColorCount>();
            var sizes = new List<SizeCount>();

            foreach (var color in Color.All)
            {
                colors.Add(new ColorCount
                {
                    Color = color,
                    Count = _shirts.Count(c => c.Color.Id == color.Id
                      && (!options.Sizes.Any() || options.Sizes.Select(s => s.Id).Contains(c.Size.Id)))
                });
            };

            foreach (var size in Size.All)
            {
                sizes.Add(new SizeCount
                {
                    Size = size,
                    Count = _shirts.Count(s => s.Size.Id == size.Id
                                && (!options.Colors.Any() || options.Colors.Select(c => c.Id).Contains(s.Color.Id)))
                });
            };

            var searchResults = new SearchResults
            {
                Shirts = result,
                ColorCounts = colors,
                SizeCounts = sizes
            };

            return searchResults;
        }
    }
}