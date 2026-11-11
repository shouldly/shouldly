using System.Collections;

namespace Shouldly.Tests.TestHelpers;

/// <summary>
/// An enumerable whose <c>Equals</c> compares only <see cref="Id"/>, not its items, as in the documentation's
/// <c>UseEquality</c> example. Its equality rule is registered in <see cref="ModuleInitializer"/>.
/// </summary>
public sealed class IdBag(int id, params int[] items) : IEnumerable<int>
{
    public int Id { get; } = id;

    public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)items).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override bool Equals(object? obj) => obj is IdBag other && other.Id == Id;

    public override int GetHashCode() => Id;

    public override string ToString() => $"IdBag({Id})";
}
