using System.Collections;

namespace Shouldly.Tests.TestHelpers;

class Strange : IEnumerable<Strange>
{
    private readonly string? _thing;

    public Strange()
    {
    }

    private Strange(string thing)
    {
        _thing = thing;
    }

    public IEnumerator<Strange> GetEnumerator() =>
        new List<Strange>().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    public static implicit operator Strange(string thing) =>
        new(thing);

    protected bool Equals(Strange other) =>
        string.Equals(_thing, other._thing);

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Strange)obj);
    }

    public override int GetHashCode() =>
        _thing != null ? _thing.GetHashCode() : 0;

    public static bool operator ==(Strange left, Strange right) =>
        Equals(left, right);

    public static bool operator !=(Strange left, Strange right) =>
        !Equals(left, right);

    public override string ToString() => _thing ?? "null";
}