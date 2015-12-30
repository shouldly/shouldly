using System.Collections.Generic;

namespace Shouldly.Tests.TestHelpers
{
    public static class EnumerableExtensionHelpers
    {
        public static IEnumerable<T> ToEnumerable<T>(this T obj)
        {
            yield return obj;
        }
    }
}