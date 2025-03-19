using System.Collections;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class SetScenario
{
    [Fact]
    public void ShouldFailWhenSetIsTooShort()
    {
        var subject = new HashSet<int>{ 1, 2, 3, 4 };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3, 4, 5 }, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            [1, 2, 3, 4] [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 3, 4]

            Additional Info:
                Some additional context; [5] is expected but not found
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 3, 4]

            Additional Info:
                Some additional context; [5] is expected but not found
            """,

            MessageScrubber);
    }

    [Fact]
    public void ShouldFailWhenSetIsTooLong()
    {
        var subject = new HashSet<int>{ 1, 2, 3, 4, 5, 6 };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3, 4, 5 }, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            [1, 2, 3, 4, 5, 6] [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 3, 4, 5, 6]

            Additional Info:
                Some additional context; [6] is not expected but found
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 3, 4, 5, 6]

            Additional Info:
                Some additional context; [6] is not expected but found
            """,

            MessageScrubber);
    }

    [Fact]
    public void ShouldFailWhenSetDoNotMatch()
    {
        var subject = new HashSet<int>{ 1, 2, 6, 4, 3 };
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3, 4, 5 }, "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            [1, 2, 6, 4, 3] [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 6, 4, 3]

            Additional Info:
                Some additional context; [5] is expected but not found; [6] is not expected but found
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.Collections.Generic.HashSet[[System.Int32]]]

                Expected value to be
            [1, 2, 3, 4, 5]
                but was
            [1, 2, 6, 4, 3]

            Additional Info:
                Some additional context; [5] is expected but not found; [6] is not expected but found
            """,

            MessageScrubber);
    }

    [Fact]
    public void ShouldPassWhenSetIsISet()
    {
        var subject = new HashSet<int> { 1, 2, 6, 4, 5 };
        subject.ShouldBeEquivalentTo(new HashSet<int> { 6, 2, 1, 5, 4 });
    }

    [Fact]
    public void ShouldPassWhenSetIsIImmutableSet()
    {
        var subject = new TestImmutableSet([1, 2, 6, 4, 5]);
        subject.ShouldBeEquivalentTo(new TestImmutableSet([6, 2, 1, 5, 4]));
    }

    private static string MessageScrubber(string original)
    {
        const string pattern1 = @"\[System\.Int32,[^\]]+\]";
        const string replacement1 = "[System.Int32]";
        const string pattern2 = @"HashSet\`[0-9]";
        const string replacement2 = "HashSet";

        return Regex.Replace(
            Regex.Replace(original, pattern1, replacement1),
            pattern2,
            replacement2);
    }

    private class TestImmutableSet : IImmutableSet<int>
    {
        private readonly HashSet<int> set;

        public TestImmutableSet(HashSet<int> set) =>
            this.set = set;

        public IEnumerator<int> GetEnumerator() => set.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => set.Count;

        public IImmutableSet<int> Add(int value) =>
            set.Contains(value) ? this : new([..set, value]);

        public IImmutableSet<int> Clear() => new TestImmutableSet([]);

        public bool Contains(int value) => set.Contains(value);

        public IImmutableSet<int> Except(IEnumerable<int> other)
        {
            var newSet = new HashSet<int>(set);
            newSet.ExceptWith(other);
            return new TestImmutableSet(newSet);
        }

        public IImmutableSet<int> Intersect(IEnumerable<int> other)
        {
            var newSet = new HashSet<int>(set);
            newSet.IntersectWith(other);
            return new TestImmutableSet(newSet);
        }

        public bool IsProperSubsetOf(IEnumerable<int> other) => set.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<int> other) => set.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<int> other) => set.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<int> other) => set.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<int> other) => set.Overlaps(other);

        public IImmutableSet<int> Remove(int value)
        {
            if (!set.Contains(value))
                return this;
            var newSet = new HashSet<int>(set);
            newSet.Remove(value);
            return new TestImmutableSet(newSet);
        }

        public bool SetEquals(IEnumerable<int> other) => set.SetEquals(other);

        public IImmutableSet<int> SymmetricExcept(IEnumerable<int> other)
        {
            var newSet = new HashSet<int>(set);
            newSet.SymmetricExceptWith(other);
            return new TestImmutableSet(newSet);
        }

        public bool TryGetValue(int equalValue, out int actualValue)
        {
            if (set.Contains(equalValue))
            {
                actualValue = equalValue;
                return true;
            }

            actualValue = 0;
            return false;
        }

        public IImmutableSet<int> Union(IEnumerable<int> other)
        {
            var newSet = new HashSet<int>(set);
            newSet.UnionWith(other);
            return new TestImmutableSet(newSet);
        }
    }
}