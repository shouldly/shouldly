using System;
using System.Collections.Generic;
using Xunit;

namespace Shouldly.Tests
{
    public class ShouldlyWebsiteExampleTests
    {
        // Tests for the examples shown on http://shouldly.github.io/ (https://github.com/shouldly/shouldly.github.com)

        public class ContestantPoints
        {
            [Fact]
            public void Shouldly_ContestantPointsShouldBe1337()
            {
                var contestant = new Contestant { Points = 0 };

                TestHelpers.Should.Error(
                    () => contestant.Points.ShouldBe(1337),
                    "contestant.Points should be 1337 but was 0");
            }

            [Fact]
            public void Shouldly_ContestantPointsShouldBe1337_HappyPath()
            {
                var contestant = new Contestant { Points = 0 };

                contestant.Points.ShouldBe(0);
            }

            private class Contestant
            {
                public int Points { get; set; }
            }
        }

        public class MapIndexOfBoo
        {
            private IList<string> GetMap()
            {
                return new[]
                {
                    "aoo",
                    "boo",
                    "coo"
                };
            }

            [Fact]
            public void Shouldly_IndexOfBoo()
            {
                var map = GetMap();

                TestHelpers.Should.Error(
                    () => map.IndexOf("boo").ShouldBe(2),
                    "map.IndexOf(\"boo\") should be 2 but was 1");
            }

            [Fact]
            public void Shouldly_IndexOfBoo_HappyPath()
            {
                var map = GetMap();

                map.IndexOf("boo").ShouldBe(1);
            }
        }

        public class CompareTwoCollections
        {
            [Fact]
            public void Shouldly_CompareTwoCollections()
            {
                TestHelpers.Should.Error(
                    () => new[] { 1, 2, 3 }.ShouldBe(new[] { 1, 2, 4 }),
                    "new[] {1, 2, 3} should be [1, 2, 4] but was [1, 2, 3] difference [1, 2, *3*]");
            }

            [Fact]
            public void Shouldly_CompareTwoCollections_HappyPath()
            {
                new[] { 1, 2, 3 }.ShouldBe(new[] { 1, 2, 3 });
            }
        }

        public class ShouldThrowException
        {
            [Fact]
            public void Shouldly_ShouldThrowException()
            {
                var widget = new Widget();

                Should.Throw<ArgumentOutOfRangeException>(() => widget.Twist(-1));
            }

            [Fact]
            public void Shouldly_ShouldNotThrowException()
            {
                var widget = new Widget();

                Should.NotThrow(() => widget.Twist(5));
            }

            [Fact]
            public void Shouldly_ShouldErrorIfCatchingExceptionOfWrongType()
            {
                var widget = new Widget();

                TestHelpers.Should.Error(
                    () => Should.Throw<ArgumentOutOfRangeException>(() => widget.Twist(-2)),
                    "`widget.Twist(-2)` should throw System.ArgumentOutOfRangeException but threw System.InvalidOperationException");
            }

            [Fact]
            public void Shouldly_ShouldErrorIfNoExceptionWasThrown()
            {
                var widget = new Widget();

                TestHelpers.Should.Error(
                    () => Should.Throw<ArgumentOutOfRangeException>(() => widget.Twist(5)),
                    "`widget.Twist(5)` should throw System.ArgumentOutOfRangeException but did not");
            }

            private class Widget
            {
                public void Twist(int i)
                {
                    if (i == -1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(i));
                    }

                    if (i == -2)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
