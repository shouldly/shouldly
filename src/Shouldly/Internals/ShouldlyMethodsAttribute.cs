namespace Shouldly;

/// <summary>
/// Marks a class containing Shouldly assertion methods. Since Shouldly 5, call-site
/// expressions are captured via <see cref="System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/>,
/// so this attribute is only consulted by the legacy stack-walking fallback used when a
/// netstandard2.0 consumer's compiler does not supply caller argument expressions. Apply it to
/// custom assertion classes only if they need to support that scenario; on modern targets it has no effect.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ShouldlyMethodsAttribute : Attribute { }
