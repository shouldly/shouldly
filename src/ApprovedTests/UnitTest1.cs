using System;
using Shouldly;
using Shouldly.Configuration;
using Xunit;

namespace ApprovedTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // var diffTool = new DiffTool("Beyond Compare", @"/Applications/Beyond Compare.app/Contents/MacOS/bcomp", (received, approved, approvedExists) =>
            // /Applications/Beyond Compare.app/Contents/MacOS
            "Bar3".ShouldMatchApproved();
        }
    }
}