#if NET5_0_OR_GREATER

namespace Shouldly.Tests.ShouldBe;

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
}

#endif