public class EqualityComparerTests
{
    /*
     * Code heavily influenced by code from xunit assertion tests
     * at https://github.com/xunit/xunit/blob/master/test/test.xunit2.assert/Asserts/EqualityAssertsTests.cs
     */
    [Fact]
    public void EqualityComparer_WhenGivenEqualLists_ShouldBeTrue()
    {
        var x = new List<object> { new List<object> { new List<object> { new List<object>() } } };
        var y = new List<object> { new List<object> { new List<object> { new List<object>() } } };

        x.ShouldBe(y);
    }

    [Fact]
    public void EqualityComparer_WhenGivenNonComparableObject_ShouldBeTrue()
    {
        var nco1 = new NonComparableObject();
        var nco2 = new NonComparableObject();

        nco1.ShouldBe(nco2);
    }

    [Fact]
    public void EqualityComparer_WhenGivenComparableObject_ShouldBeTrue()
    {
        var co1 = new SpyComparable();
        var co2 = new SpyComparable();

        co1.ShouldBe(co2);
    }

    [Fact]
    public void EqualityComparer_WhenGivenComparableGeneric_ShouldBeTrue()
    {
        var co1 = new SpyComparableGeneric();
        var co2 = new SpyComparableGeneric();

        co1.ShouldBeGreaterThanOrEqualTo(co2);
        co1.CompareCalled.ShouldBe(true);
    }

    [Fact]
    public void EqualityComparer_WhenGivenOverriddenEquatable_ShouldBeTrue()
    {
        var eq1 = new SpyEquatable();
        var eq2 = new SpyEquatable();

        eq1.ShouldBe(eq2);
        eq2.EqualsCalled.ShouldBe(true);
        eq2.EqualsOther.ShouldBeSameAs(eq1);
    }

    private class NonComparableObject
    {
        public override bool Equals(object? obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 42;
        }
    }

    private class SpyComparable : IComparable
    {
        public int CompareTo(object? obj)
        {
            return 0;
        }
    }

    private class SpyComparableGeneric : IComparable<SpyComparableGeneric>
    {
        public bool CompareCalled;

        public int CompareTo(SpyComparableGeneric? other)
        {
            CompareCalled = true;
            return 0;
        }
    }

    public class SpyEquatable : IEquatable<SpyEquatable>
    {
        public bool EqualsCalled;
        public SpyEquatable? EqualsOther;

        public bool Equals(SpyEquatable? other)
        {
            EqualsCalled = true;
            EqualsOther = other;

            return true;
        }
    }
}