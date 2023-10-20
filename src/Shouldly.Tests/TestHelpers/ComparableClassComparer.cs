namespace Shouldly.Tests.TestHelpers;

public class ComparableClassComparer : IEqualityComparer<ComparableClass>
{
    public bool Equals(ComparableClass? x, ComparableClass? y)
    {
        if (x == y) return true;
        if (x is null || y is null) return false;

        return x.Property == y.Property;
    }

    public int GetHashCode(ComparableClass obj) =>
        EqualityComparer<string?>.Default.GetHashCode(obj.Property!);
}