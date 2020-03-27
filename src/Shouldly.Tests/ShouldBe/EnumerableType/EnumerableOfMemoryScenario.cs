using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class EnumerableOfMemoryScenario
    {
        [Fact]
        public void EnumerableOfMemoryScenarioShouldFail()
        {
            Memory<byte> foo = new byte[] { 1, 2, 3 }.AsMemory();
            Memory<byte> bar = new byte[] { 1, 2 }.AsMemory();

            Verify.ShouldFail(() =>
                    foo.ShouldBe(bar, "Some additional context"),

errorWithSource:
@"foo
    should be
System.Memory<Byte>[2]
    but was
System.Memory<Byte>[3]

Additional Info:
    Some additional context",

errorWithoutSource:
@"System.Memory<Byte>[3]
    should be
System.Memory<Byte>[2]
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            Memory<byte> foo = new byte[] { 1, 2, 3 }.AsMemory();
            foo.ShouldBe(new byte[] { 1, 2, 3 });
        }
    }
}