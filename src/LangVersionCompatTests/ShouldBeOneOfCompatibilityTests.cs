using Shouldly;
using Xunit;

namespace LangVersionCompatTests;

public class ShouldBeOneOfCompatibilityTests
{
    [Fact]
    public void Individual_values_bind_to_the_params_overload()
    {
        1.ShouldBeOneOf(1, 2, 3);
    }

    [Fact]
    public void Array_and_custom_message_overload_remains_available()
    {
        1.ShouldBeOneOf([1, 2, 3], "value should match");
    }
}