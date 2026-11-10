using System.Collections;
using System.Runtime.CompilerServices;

#region IdBag

// An enumerable type whose equality is its Id, not its items.
public sealed class IdBag(int id, params int[] items) : IEnumerable<int>
{
    public int Id { get; } = id;

    public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)items).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override bool Equals(object? obj) => obj is IdBag other && other.Id == Id;

    public override int GetHashCode() => Id;
}

#endregion

static class CompareAsObjectTypesRegistration
{
    #region RegisterCompareAsObjectType

    [ModuleInitializer]
    internal static void Initialize() =>
        ShouldlyConfiguration.CompareAsObjectTypes.Add(typeof(IdBag).FullName!);

    #endregion
}
