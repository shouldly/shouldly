namespace Shouldly.Tests.ShouldNotBe;

/// <summary>
/// For a type with an equality rule (<see cref="ShouldlyConfiguration.UseEquality{T}(Func{T, T, bool})"/>),
/// <c>ShouldNotBe</c> compares by the rule, as <c>ShouldBe</c> does, so it passes exactly when <c>ShouldBe</c> fails.
/// See issue #1425.
/// </summary>
public class EqualityRuleScenario
{
    [Fact]
    public void DifferentIdSameItems()
    {
        var bag = new IdBag(1, 1, 2);
        var other = new IdBag(2, 1, 2);

        Should.Throw<ShouldAssertException>(() => bag.ShouldBe<IdBag>(other));
        bag.ShouldNotBe<IdBag>(other);
        bag.ShouldNotBe(other);
    }

    [Fact]
    public void SameIdDifferentItems()
    {
        var bag = new IdBag(1, 1, 2);
        var other = new IdBag(1, 9);

        bag.ShouldBe<IdBag>(other);
        Should.Throw<ShouldAssertException>(() => bag.ShouldNotBe<IdBag>(other));
    }

    [Fact]
    public void SelfEnumerableRegisteredType()
    {
        Strange value = "string";
        Strange different = "other";

        value.ShouldNotBe(different);
        Should.Throw<ShouldAssertException>(() => value.ShouldNotBe((Strange)"string"));
    }

    [Fact]
    public void ShouldNotBeOneOfAgrees()
    {
        var bag = new IdBag(1, 1, 2);

        bag.ShouldNotBeOneOf([new IdBag(2, 1, 2)]);
        Should.Throw<ShouldAssertException>(() => bag.ShouldNotBeOneOf([new IdBag(1, 9)]));
    }

    [Fact]
    public void ShouldNotContainAgrees()
    {
        IdBag[] bags = [new IdBag(1, 1, 2)];

        bags.ShouldNotContain(new IdBag(2, 1, 2));
        Should.Throw<ShouldAssertException>(() => bags.ShouldNotContain(new IdBag(1, 9)));
    }

    [Fact]
    public void SameIdDifferentItemsShouldFail()
    {
        var bag = new IdBag(1, 1, 2);

        Verify.ShouldFail(() =>
            bag.ShouldNotBe<IdBag>(new IdBag(1, 9), "Some additional context"));
    }
}
