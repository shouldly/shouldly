namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// An unsigned integer never equals a negative signed integer. Comparing them used to convert the negative value to
/// the unsigned type, which threw <see cref="OverflowException"/> instead of failing the assertion. See issue #1426.
/// </summary>
public class UnsignedAndNegativeSignedScenarios
{
    private static readonly object[] Unsigned = [(byte)5, (ushort)5, 5u, 5ul];
    private static readonly object[] NegativeSigned = [(sbyte)-1, (short)-1, -1, -1L];

    public static TheoryData<object, object> UnsignedAndNegativeSignedPairs()
    {
        var data = new TheoryData<object, object>();
        foreach (var unsigned in Unsigned)
        foreach (var signed in NegativeSigned)
        {
            data.Add(unsigned, signed);
            data.Add(signed, unsigned);
        }

        return data;
    }

    [Theory]
    [MemberData(nameof(UnsignedAndNegativeSignedPairs))]
    public void ShouldBeFails(object actual, object expected) =>
        Should.Throw<ShouldAssertException>(() => actual.ShouldBe(expected));

    [Theory]
    [MemberData(nameof(UnsignedAndNegativeSignedPairs))]
    public void ShouldNotBePasses(object actual, object expected) =>
        actual.ShouldNotBe(expected);

    [Theory]
    [MemberData(nameof(UnsignedAndNegativeSignedPairs))]
    public void CollectionsAreUnequal(object actual, object expected)
    {
        new List<object> { actual }.ShouldNotBe(new List<object> { expected });
        Should.Throw<ShouldAssertException>(() => new List<object> { actual }.ShouldBe(new List<object> { expected }));
        Should.Throw<ShouldAssertException>(() => new object[] { actual }.ShouldBe([expected], ignoreOrder: true));
    }

    [Theory]
    [MemberData(nameof(UnsignedAndNegativeSignedPairs))]
    public void ShouldContainFails(object element, object expected)
    {
        Should.Throw<ShouldAssertException>(() => new[] { element }.ShouldContain(expected));
        new[] { element }.ShouldNotContain(expected);
    }

    [Theory]
    [MemberData(nameof(UnsignedAndNegativeSignedPairs))]
    public void OtherMembershipAssertionsTreatThemAsDifferent(object actual, object other)
    {
        actual.ShouldNotBeOneOf([other]);
        Should.Throw<ShouldAssertException>(() => actual.ShouldBeOneOf([other]));
        new[] { actual, other }.ShouldBeUnique();
        Should.Throw<ShouldAssertException>(() => new[] { actual }.ShouldBeSubsetOf([other]));
    }

    [Fact]
    public void DictionaryValuesAreUnequal()
    {
        var dictionary = new Dictionary<string, object> { ["key"] = 5ul };

        dictionary.ShouldNotContainValueForKey("key", -1L);
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldContainKeyAndValue("key", -1L));
    }

    [Fact]
    public void ValuesThatLookAlikeBitwiseAreUnequal()
    {
        // These share a bit pattern, so a reinterpreting conversion would call them equal.
        ((object)ulong.MaxValue).ShouldNotBe(-1L);
        ((object)uint.MaxValue).ShouldNotBe(-1);
        ((object)(1ul << 63)).ShouldNotBe(long.MinValue);
        ((object)ulong.MaxValue).ShouldNotBe(long.MaxValue);
    }

    [Fact]
    public void EqualNonNegativeValuesAcrossSignednessStillMatch()
    {
        ((object)5ul).ShouldBe(5L);
        ((object)5u).ShouldBe(5);
        ((object)5L).ShouldBe(5ul);
        ((object)(sbyte)5).ShouldBe(5u);
        ((object)0ul).ShouldBe((sbyte)0);
        ((object)(ulong)long.MaxValue).ShouldBe(long.MaxValue);
        new object[] { 1ul }.ShouldContain(1);
        new List<object> { 1ul, 2u }.ShouldBe(new List<object> { 1, 2L });
    }

    [Fact]
    public void ShouldBeFailsWithMessage()
    {
        object actual = 5ul;
        Verify.ShouldFail(() =>
            actual.ShouldBe(-1L, "Some additional context"));
    }

    [Fact]
    public void ShouldContainFailsWithMessage()
    {
        var items = new object[] { 1ul };
        Verify.ShouldFail(() =>
            items.ShouldContain(-1, "Some additional context"));
    }
}
