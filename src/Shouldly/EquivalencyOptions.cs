namespace Shouldly;

/// <summary>
/// Options controlling how <c>ShouldBeEquivalentTo</c> compares object graphs.
/// </summary>
public class EquivalencyOptions
{
    /// <summary>
    /// When true, collections are compared order-insensitively: every expected element must have
    /// an equivalent element in the actual collection, regardless of position. Dictionaries and sets
    /// are compared order-insensitively without it, against each other and against other collections,
    /// except that a sorted or ordered one is compared in order against a list or an array.
    /// </summary>
    public bool IgnoreOrder { get; set; }

    /// <summary>
    /// Names of members to skip wherever they appear in the compared object graphs.
    /// </summary>
    public ICollection<string> MembersToIgnore { get; } = new HashSet<string>(StringComparer.Ordinal);
}
