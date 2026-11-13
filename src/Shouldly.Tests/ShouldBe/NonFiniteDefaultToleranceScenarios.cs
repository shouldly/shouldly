namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// Within the default floating-point tolerance, as within an assertion's own, no finite number is equal to an infinity,
/// however large the tolerance.
/// </summary>
// The tests mutate the global ShouldlyConfiguration.DefaultFloatingPointTolerance, so the class runs in the
// non-parallel Global configuration collection to keep that change from leaking into other test classes.
[Collection(GlobalConfigCollection.Name)]
public class NonFiniteDefaultToleranceScenarios
{
    [Fact]
    public void AFiniteNumberIsNotWithinAnInfiniteToleranceOfAnInfinity()
    {
        ShouldlyConfiguration.DefaultFloatingPointTolerance = double.PositiveInfinity;
        try
        {
            1.0.ShouldNotBe(double.PositiveInfinity);
            double.PositiveInfinity.ShouldNotBe(1.0);
            1f.ShouldNotBe(float.NegativeInfinity);
            float.NegativeInfinity.ShouldNotBe(1f);
            double.PositiveInfinity.ShouldNotBe(double.NegativeInfinity);
            double.PositiveInfinity.ShouldBe(double.PositiveInfinity);
            double.NaN.ShouldBe(double.NaN);
            1.0.ShouldBe(1e300);
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }
}
