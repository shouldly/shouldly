#if !PORTABLE
using Xunit;

namespace Shouldly.Tests.Shared.ShouldMatchApproved
{
    public class ShouldMatchApprovedScenarios
    {
        [Fact]
        public void Simple()
        {
            "Foo".ShouldMatchApproved();
        }
    }
}
#endif
