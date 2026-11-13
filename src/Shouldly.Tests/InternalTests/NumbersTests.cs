using System.Numerics;
using Shouldly.Internals;

namespace Shouldly.Tests.InternalTests;

/// <summary>
/// The rules of <see cref="Numbers"/> that are awkward to reach through an assertion. How two numbers compare is
/// tested through ShouldBe, ShouldContain and ShouldBeEquivalentTo.
/// </summary>
public class NumbersTests
{
    public static TheoryData<object> NumbersOfEveryType() =>
    [
        1.0, 1f, 1m, 1, 1u, 1L, 1UL, (short)1, (ushort)1, (byte)1, (sbyte)1,
#if NET5_0_OR_GREATER
        (Half)1,
#endif
    ];

    [Theory]
    [MemberData(nameof(NumbersOfEveryType))]
    public void EveryBuiltInNumericTypeIsANumber(object value) =>
        Numbers.IsNumber(value).ShouldBeTrue();

    public static TheoryData<object> NotNumbers() =>
    [
        'a', "1", DayOfWeek.Monday, new BigInteger(1), true, TimeSpan.Zero,
    ];

    [Theory]
    [MemberData(nameof(NotNumbers))]
    public void OtherValuesAreNotNumbers(object value) =>
        Numbers.IsNumber(value).ShouldBeFalse();

    [Fact]
    public void NullIsNotANumber() =>
        Numbers.IsNumber(null).ShouldBeFalse();

    [Fact]
    public void OnlyFloatingPointTypesHonourTheDefaultTolerance()
    {
        Numbers.HonoursDefaultTolerance(typeof(double)).ShouldBeTrue();
        Numbers.HonoursDefaultTolerance(typeof(float)).ShouldBeTrue();
#if NET5_0_OR_GREATER
        Numbers.HonoursDefaultTolerance(typeof(Half)).ShouldBeTrue();
#endif
        Numbers.HonoursDefaultTolerance(typeof(decimal)).ShouldBeFalse();
        Numbers.HonoursDefaultTolerance(typeof(int)).ShouldBeFalse();
        Numbers.HonoursDefaultTolerance(typeof(double?)).ShouldBeFalse();
        Numbers.HonoursDefaultTolerance(typeof(object)).ShouldBeFalse();
    }

    [Fact]
    public void DecimalsTooFarApartToSubtractAreNotWithinAnyTolerance()
    {
        Numbers.EqualWithin(decimal.MaxValue, decimal.MinValue, decimal.MaxValue).ShouldBeFalse();
        Numbers.EqualWithin(decimal.MinValue, decimal.MaxValue, decimal.MaxValue).ShouldBeFalse();
        Numbers.EqualWithin(decimal.MaxValue, 0m, decimal.MaxValue).ShouldBeTrue();
        Numbers.EqualWithin(decimal.MinValue, 0m, decimal.MaxValue).ShouldBeTrue();
    }
}
