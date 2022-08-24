namespace Shouldly.Tests.TestHelpers
{
    internal class CustomComparer<T> : IComparer<T>
    {
        public int Compare(T? x, T? y)
        {
            Custom x1 = (Custom)(object)x!;
            Custom x2 = (Custom)(object)y!;
            if (x1.Val == x2.Val)
                return 0;
            return x1.Val > x2.Val ? 1 : -1;
        }
    }

    internal class Custom
    {
        public int Val { get; set; }
    }
}
