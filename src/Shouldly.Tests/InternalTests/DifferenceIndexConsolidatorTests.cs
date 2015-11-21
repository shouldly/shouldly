using System.Collections.Generic;
using Shouldly.DifferenceHighlighting;
using Xunit.Extensions;

namespace Shouldly.Tests.InternalTests
{
    public class DifferenceIndexConsolidatorTests
    {
        const int MaxDiffLength = 5;
        const int MaxLengthOfStrings = 21;

        [Theory]
        [PropertyData("IndexConsolidationTestCases")]
        public void ShouldConsolidate_Indices_Correctly_GivenInput(List<int> indicesOfAllDifferences, List<int> expectedConsolidatedOutputIndices)
        {
            var consolidator = new DifferenceIndexConsolidator(MaxDiffLength, MaxLengthOfStrings, indicesOfAllDifferences);
            var outputFromConsolidator = consolidator.GetConsolidatedIndices();
            outputFromConsolidator.ShouldBe(expectedConsolidatedOutputIndices);
        }

        public static IEnumerable<object[]> IndexConsolidationTestCases { get; private set; } = new[]
            {
                // indicesOfAllDifferences,  expectedConsolidatedOutputIndices
                // Simple cases, no consolidation so far. Each diff index is > maxDiffLength from the next.
                new object[] {new List<int> {2}, new List<int> {0}},
                new object[] {new List<int> {0}, new List<int> {0}},
                new object[] {new List<int> {18}, new List<int> {16}},
                new object[] {new List<int> {20}, new List<int> {16}},
                new object[] {new List<int> {2, 18}, new List<int> {0, 16}},
                new object[] {new List<int> {0, 20}, new List<int> {0, 16}},
                new object[] {new List<int> {0, 5, 10, 15}, new List<int> {0, 3, 8, 13}},
                new object[] {new List<int> {2, 7, 12, 17}, new List<int> {0, 5, 10, 15}},
                new object[] {new List<int> {3, 8, 13, 18}, new List<int> {1, 6, 11, 16}},

                //// Complex cases, start consolidating indices as each of the diff index is < maxDiffLength from the next
                new object[] {new List<int> {0, 4, 5, 9, 10, 14, 15, 19}, new List<int> {0, 5, 10, 15}},
                // Perhaps this consolidation can be improved. Group 4,5 together into the middle of a diff window rather than at the edges of 2 different diff windows.
                new object[] {new List<int> {0, 2, 4, 5, 7, 9, 10, 12, 14, 15, 17, 19}, new List<int> {0, 5, 10, 15}},
                new object[]
                {new List<int> {0, 6, 8, 9, 10, 11, 12, 13, 14, 15, 17, 19, 20}, new List<int> {0, 6, 11, 16}},
                new object[]
                {new List<int> {3, 4, 5, 7, 8, 9, 10, 13, 14, 15, 17, 19, 20}, new List<int> {3, 7, 13, 16}},
                new object[] {new List<int> {8, 12, 13, 18}, new List<int> {8, 11, 16}},
                new object[] {new List<int> {2, 7, 11, 15, 19, 20}, new List<int> {0, 7, 15, 16}}
            };
    }
}
