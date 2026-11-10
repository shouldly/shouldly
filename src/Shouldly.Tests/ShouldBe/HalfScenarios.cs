#if NET5_0_OR_GREATER

namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// <see cref="Half"/> is compared like the other floating-point types: widened to <see cref="double"/> when it meets
/// another numeric type, rather than converted through <see cref="IConvertible"/>, which it doesn't implement. See
/// issue #1405.
/// </summary>
// HalfHonoursTheDefaultTolerance mutates the global ShouldlyConfiguration.DefaultFloatingPointTolerance, so the class
// runs in the non-parallel Global configuration collection to keep that change from leaking into other test classes.
[Collection(GlobalConfigCollection.Name)]
public class HalfScenarios
{
    [Fact]
    public void ShouldPass_SameInstance()
    {
        Half h = (Half)12345;
        h.ShouldBe(h);
    }

    [Fact]
    public void ShouldPass_DifferentInstances()
    {
        object h1 = (Half)12345;
        object h2 = (Half)12345;
        h1.ShouldBe(h2);
    }

    public static TheoryData<object> OneOfEveryOtherNumericType() =>
    [
        1.0, 1f, 1m, 1, 1u, 1L, 1UL, (short)1, (ushort)1, (byte)1, (sbyte)1,
    ];

    [Theory]
    [MemberData(nameof(OneOfEveryOtherNumericType))]
    public void HalfEqualsTheSameValueOfAnotherNumericType(object one)
    {
        object half = (Half)1;

        half.ShouldBe(one);
        one.ShouldBe(half);
        Should.Throw<ShouldAssertException>(() => half.ShouldNotBe(one));
        Should.Throw<ShouldAssertException>(() => one.ShouldNotBe(half));
    }

    [Theory]
    [MemberData(nameof(OneOfEveryOtherNumericType))]
    public void HalfDoesNotEqualADifferentValueOfAnotherNumericType(object one)
    {
        object two = (Half)2;

        two.ShouldNotBe(one);
        one.ShouldNotBe(two);
        Should.Throw<ShouldAssertException>(() => two.ShouldBe(one));
        Should.Throw<ShouldAssertException>(() => one.ShouldBe(two));
    }

    [Fact]
    public void HalfIsComparedExactlyAfterWidening()
    {
        // (Half)0.1 is 0.0999755859375, so it is not the float or double nearest 0.1.
        object half = (Half)0.1;

        half.ShouldNotBe(0.1f);
        half.ShouldNotBe(0.1);
        half.ShouldBe((double)(Half)0.1);
        half.ShouldBe((float)(Half)0.1);

        // 1.00000001m rounds to 1f, but not to 1.0, so a decimal isn't narrowed to float on the way.
        ((object)(Half)1).ShouldNotBe(1.00000001m);
    }

    [Fact]
    public void HalfSpecialValuesAgreeAcrossTypes()
    {
        ((object)Half.NaN).ShouldBe(double.NaN);
        ((object)Half.PositiveInfinity).ShouldBe(float.PositiveInfinity);
        ((object)Half.NegativeInfinity).ShouldBe(double.NegativeInfinity);
        ((object)Half.PositiveInfinity).ShouldNotBe(double.NegativeInfinity);
        ((object)Half.NegativeZero).ShouldBe(0);
    }

    [Fact]
    [UseCulture("en-US")]
    public void ObjectHalfShouldBeAnotherNumericTypeShouldFail()
    {
        object half = (Half)1.5;
        Verify.ShouldFail(() =>
            half.ShouldBe(2, "Some additional context"));
    }

    [Fact]
    public void ObjectArrayElements()
    {
        object[] halves = [(Half)1, (Half)2.5, (Half)(-3)];

        halves.ShouldBe([1, 2.5, -3f]);
        halves.ShouldBe([-3L, 1m, 2.5], ignoreOrder: true);
        halves.ShouldNotBe([1, 2.5, -4f]);
        new List<object> { (Half)1, 2 }.ShouldBe([1, (Half)2]);
    }

    [Fact]
    [UseCulture("en-US")]
    public void ObjectArrayElementsShouldFail()
    {
        object[] halves = [(Half)1, (Half)2.5];
        Verify.ShouldFail(() =>
            halves.ShouldBe([1, 3], "Some additional context"));
    }

    [Fact]
    public void DictionaryValues()
    {
        var dictionary = new Dictionary<string, object> { ["half"] = (Half)1, ["int"] = 1 };

        dictionary.ShouldContainKeyAndValue("half", 1);
        dictionary.ShouldContainKeyAndValue("half", 1.0);
        dictionary.ShouldContainKeyAndValue("int", (Half)1);
        dictionary.ShouldNotContainValueForKey("half", 2);
        dictionary.ShouldNotContainValueForKey("int", (Half)2);
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldNotContainValueForKey("half", 1f));
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldContainKeyAndValue("half", 2m));
    }

    [Fact]
    public void HalfHonoursTheDefaultTolerance()
    {
        object half = (Half)1;

        half.ShouldNotBe(1.05);

        ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.1;
        try
        {
            half.ShouldBe(1.05);
            half.ShouldBe((Half)1.05);
            ((Half)1).ShouldBe((Half)1.05);
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }
}

#endif
