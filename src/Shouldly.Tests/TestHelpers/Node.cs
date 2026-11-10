using System.Collections;

namespace Shouldly.Tests.TestHelpers;

/// <summary>
/// Emulates Newtonsoft.Json's <c>JToken</c>: an abstract base whose subtypes enumerate their children and compare by
/// reference, with a static <see cref="DeepEquals"/> that compares them structurally. Walked as a sequence, a
/// <see cref="Leaf"/> has no children, so any two leaves look alike; that is why <see cref="ModuleInitializer"/>
/// registers <see cref="DeepEquals"/> as the equality rule for <see cref="Node"/>.
/// </summary>
public abstract class Node : IEnumerable<Node>
{
    public abstract IEnumerator<Node> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static bool DeepEquals(Node? expected, Node? actual) =>
        (expected, actual) switch
        {
            (null, null) => true,
            (Leaf e, Leaf a) => e.Text == a.Text,
            (Branch e, Branch a) => e.Children.Count == a.Children.Count &&
                                    e.Children.Zip(a.Children, DeepEquals).All(equal => equal),
            _ => false
        };
}

public class Leaf(string text) : Node
{
    public string Text { get; } = text;

    public override IEnumerator<Node> GetEnumerator() => Enumerable.Empty<Node>().GetEnumerator();

    public override string ToString() => $"Leaf({Text})";
}

/// <summary>
/// A leaf with an equality rule of its own, more specific than <see cref="Node"/>'s: it ignores case.
/// </summary>
public sealed class CaseInsensitiveLeaf(string text) : Leaf(text)
{
    public override string ToString() => $"CaseInsensitiveLeaf({Text})";
}

public sealed class Branch(params Node[] children) : Node
{
    public IReadOnlyList<Node> Children { get; } = children;

    public override IEnumerator<Node> GetEnumerator() => Children.GetEnumerator();

    public override string ToString() => $"Branch({string.Join(", ", Children)})";
}
