#if NETSTANDARD2_0

// Polyfills for attributes introduced in .NET 5 that we use to describe
// trim/AOT compatibility. On netstandard2.0 the AOT analyzers are not active,
// so these types just need to exist and be assembly-internal — they are never
// read at runtime.

#pragma warning disable IDE0130

namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method,
    Inherited = false)]
sealed class RequiresUnreferencedCodeAttribute(string message) : Attribute
{
    public string Message { get; } = message;
    public string? Url { get; set; }
}

[AttributeUsage(
    AttributeTargets.All,
    AllowMultiple = true,
    Inherited = false)]
sealed class UnconditionalSuppressMessageAttribute(string category, string checkId) : Attribute
{
    public string Category { get; } = category;
    public string CheckId { get; } = checkId;
    public string? Scope { get; set; }
    public string? Target { get; set; }
    public string? MessageId { get; set; }
    public string? Justification { get; set; }
}

#endif
