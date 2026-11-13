namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// Numbers of different types are equal only when they hold the same mathematical value. Neither side is rounded to
/// the other's type first, so a floating-point value never equals an integer or decimal it only approximates. See
/// issue #1427.
/// </summary>
// TheDefaultToleranceStillApplies mutates the global ShouldlyConfiguration.DefaultFloatingPointTolerance, so the class
// runs in the non-parallel Global configuration collection to keep that change from leaking into other test classes.
[Collection(GlobalConfigCollection.Name)]
public class LosslessNumericEqualityScenarios
{
    public static TheoryData<object, object> DistinctValues() => new()
    {
        // 2^53 + 1 isn't a double; converting the long to double rounds it to 2^53.
        { 9007199254740993L, 9007199254740992.0 },
        { 9007199254740993UL, 9007199254740992.0 },
        // The decimal rounds to the double nearest 0.1.
        { 0.1000000000000000001m, 0.1 },
        // The double nearest 0.1 is 0.1000000000000000055511151231257827..., not 0.1.
        { 0.1m, 0.1 },
        { 0.1m, 0.1f },
        // 2^24 + 1 isn't a float; converting the int to float rounds it to 2^24.
        { 16777217, 16777216f },
        { 16777217L, 16777216f },
        { 16777217m, 16777216f },
        // A float widens to a double exactly, so it isn't the double nearest 0.1.
        { 0.1f, 0.1 },
        // Out of decimal range, and not an integer.
        { decimal.MaxValue, 1e30 },
        { 1m, 1.5 },
        { 0m, double.Epsilon },
        { 1, double.NaN },
        { long.MaxValue, double.PositiveInfinity },
        { decimal.MinValue, double.NegativeInfinity },
    };

    [Theory]
    [MemberData(nameof(DistinctValues))]
    public void DistinctValuesOfDifferentTypesAreNotEqual(object first, object second)
    {
        first.ShouldNotBe(second);
        second.ShouldNotBe(first);
        Should.Throw<ShouldAssertException>(() => first.ShouldBe(second));
        Should.Throw<ShouldAssertException>(() => second.ShouldBe(first));
    }

    public static TheoryData<object, object> SameValues() => new()
    {
        { 9007199254740992L, 9007199254740992.0 },
        { 16777216, 16777216f },
        { 1, 1.0 },
        { 1, 1f },
        { -3L, -3.0 },
        { 0.5m, 0.5 },
        { 0.5m, 0.5f },
        { 1.00m, 1.0 },
        { -2.25m, -2.25f },
        { 0.1m, 0.1m },
        { 0, -0.0 },
        { 0m, -0.0f },
        // 2^95 and 2^-28, at the edges of what a decimal holds exactly.
        { 39614081257132168796771975168m, 39614081257132168796771975168.0 },
        { 0.0000000037252902984619140625m, 1.0 / 268435456 },
        { 1.5f, 1.5 },
        { 0.1f, (double)0.1f },
        { (byte)200, 200.0 },
        { (sbyte)-1, -1f },
        { (short)-300, -300.0 },
        { (ushort)300, 300f },
        { 4000000000u, 4e9 },
    };

    [Theory]
    [MemberData(nameof(SameValues))]
    public void SameValuesOfDifferentTypesAreEqual(object first, object second)
    {
        first.ShouldBe(second);
        second.ShouldBe(first);
        Should.Throw<ShouldAssertException>(() => first.ShouldNotBe(second));
        Should.Throw<ShouldAssertException>(() => second.ShouldNotBe(first));
    }

    [Fact]
    public void LargeIntegersEqualTheDoubleTheyAreExactly()
    {
        // 2^63 and 2^64 - 2^11 are doubles; long.MaxValue (2^63 - 1) and ulong.MaxValue aren't.
        ((object)((ulong)long.MaxValue + 1)).ShouldBe(9223372036854775808.0);
        ((object)long.MinValue).ShouldBe(-9223372036854775808.0);
        ((object)18446744073709549568UL).ShouldBe(18446744073709549568.0);
        ((object)long.MaxValue).ShouldNotBe(9223372036854775808.0);
        ((object)ulong.MaxValue).ShouldNotBe(18446744073709551616.0);
    }

    [Fact]
    public void ObjectArrayElements()
    {
        new object[] { 9007199254740993L, 0.1m }.ShouldNotBe([9007199254740992.0, 0.1]);
        new object[] { 9007199254740992L, 0.5m }.ShouldBe([9007199254740992.0, 0.5]);
    }

    [Fact]
    public void TheDefaultToleranceStillApplies()
    {
        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            ((object)0.1m).ShouldBe(0.1);
            ((object)9007199254740993L).ShouldBe(9007199254740992.0);
            ((object)1).ShouldNotBe(1.1);
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }

    [Fact]
    public void NothingIsNarrowedToFloatUnderTheDefaultTolerance()
    {
        // 16777217 isn't a float: converting it to float rounds it to 16777216, and the tolerance would then compare
        // two equal floats. Both are widened to double instead, as ShouldBeEquivalentTo does, so the tolerance
        // compares the values they hold.
        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            ((object)16777217).ShouldNotBe(16777216f);
            ((object)16777216f).ShouldNotBe(16777217L);
            ((object)16777217m).ShouldNotBe(16777216f);
            new object[] { 16777217 }.ShouldNotBe([16777216f]);
            ((object)16777216).ShouldBe(16777216f);
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }
}
