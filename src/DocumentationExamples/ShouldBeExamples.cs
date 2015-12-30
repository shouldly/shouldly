using Shouldly;
using Xunit;

namespace DocumentationExamples
{
    public class ShouldBeExamples
    {
        [Fact]
        public void BooleanExample()
        {
            DocExampleWriter.Document(
                () =>
{
    const bool myValue = false;
    myValue.ShouldBe(true, "Some additional context");
},
@"myValue
    should be
true
    but was
false

Additional Info:
    Some additional context"
            );
            
        }
    }
}
