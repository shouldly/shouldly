#if net40
using System;

namespace Shouldly
{

    internal class ShouldThrowAsyncAssertionContext : ShouldlyAssertionContext
    {
        public ShouldThrowAsyncAssertionContext(Type exception) : base("Should", "Throw", exception)
        {
        }
    }
}
#endif
