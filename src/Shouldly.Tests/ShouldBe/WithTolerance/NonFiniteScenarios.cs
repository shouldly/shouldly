namespace Shouldly.Tests.ShouldBe.WithTolerance;

/// <summary>
/// NaN and infinities compare under a tolerance as they do without one: two NaNs are equal, an infinity equals only an
/// infinity of the same sign, and no finite number is equal to an infinity, however large the tolerance.
/// </summary>
public class NonFiniteScenarios
{
    public static TheoryData<double, double> EqualValues() => new()
    {
        { double.NaN, double.NaN },
        { double.PositiveInfinity, double.PositiveInfinity },
        { double.NegativeInfinity, double.NegativeInfinity },
    };

    public static TheoryData<double, double, double> UnequalValues() => new()
    {
        { double.PositiveInfinity, double.NegativeInfinity, 0.01 },
        { double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity },
        { 1.0, double.PositiveInfinity, double.PositiveInfinity },
        { -1.0, double.NegativeInfinity, double.PositiveInfinity },
        { 1e30, double.PositiveInfinity, double.PositiveInfinity },
        { double.NaN, 1.0, double.PositiveInfinity },
        { double.NaN, double.PositiveInfinity, double.PositiveInfinity },
    };

    [Theory]
    [MemberData(nameof(EqualValues))]
    public void DoublesAreEqualWithinATolerance(double first, double second)
    {
        first.ShouldBe(second, 0.01);
        new[] { first }.ShouldBe([second], 0.01);
        new[] { 0.0, first }.ShouldContain(second, 0.01);
    }

    [Theory]
    [MemberData(nameof(EqualValues))]
    public void FloatsAreEqualWithinATolerance(double first, double second)
    {
        var firstFloat = (float)first;
        var secondFloat = (float)second;
        firstFloat.ShouldBe(secondFloat, 0.01);
        new[] { firstFloat }.ShouldBe([secondFloat], 0.01);
        new[] { 0f, firstFloat }.ShouldContain(secondFloat, 0.01);
    }

    [Theory]
    [MemberData(nameof(UnequalValues))]
    public void DoublesAreNotEqualWithinAnyTolerance(double first, double second, double tolerance)
    {
        Should.Throw<ShouldAssertException>(() => first.ShouldBe(second, tolerance));
        Should.Throw<ShouldAssertException>(() => second.ShouldBe(first, tolerance));
        Should.Throw<ShouldAssertException>(() => new[] { first }.ShouldBe([second], tolerance));
        Should.Throw<ShouldAssertException>(() => new[] { first }.ShouldContain(second, tolerance));
    }

    [Theory]
    [MemberData(nameof(UnequalValues))]
    public void FloatsAreNotEqualWithinAnyTolerance(double first, double second, double tolerance)
    {
        var firstFloat = (float)first;
        var secondFloat = (float)second;
        Should.Throw<ShouldAssertException>(() => firstFloat.ShouldBe(secondFloat, tolerance));
        Should.Throw<ShouldAssertException>(() => secondFloat.ShouldBe(firstFloat, tolerance));
        Should.Throw<ShouldAssertException>(() => new[] { firstFloat }.ShouldBe([secondFloat], tolerance));
        Should.Throw<ShouldAssertException>(() => new[] { firstFloat }.ShouldContain(secondFloat, tolerance));
    }
}
