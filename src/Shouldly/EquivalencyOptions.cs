namespace Shouldly;

/// <summary>
/// Options controlling how <c>ShouldBeEquivalentTo</c> compares object graphs.
/// </summary>
public class EquivalencyOptions
{
    /// <summary>
    /// When true, collections are compared order-insensitively: every expected element must have
    /// an equivalent element in the actual collection, regardless of position. Sets are always
    /// compared order-insensitively.
    /// </summary>
    public bool IgnoreOrder { get; set; }

    /// <summary>
    /// Names of members to skip wherever they appear in the compared object graphs.
    /// </summary>
    public ICollection<string> MembersToIgnore { get; } = new HashSet<string>(StringComparer.Ordinal);
}
