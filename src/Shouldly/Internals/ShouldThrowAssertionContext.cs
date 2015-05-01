#if net40
using System;

namespace Shouldly
{

    internal class ShouldThrowAssertionContext : ShouldlyAssertionContext
    {
        public ShouldThrowAssertionContext(Type exception) : base("Should", "Throw", exception)
        {
        }
    }
}
#endif
