using Xunit;

namespace Shouldly.Tests.ShouldBeEquivalentTo
{
    public class ObjectScenario
    {
        [Fact]
        public void ShouldPassWhenReferencesAreEqual()
        {
            var subject = new FakeObject();
            var expected = subject;
            subject.ShouldBeEquivalentTo(expected);
        }
    }
}
