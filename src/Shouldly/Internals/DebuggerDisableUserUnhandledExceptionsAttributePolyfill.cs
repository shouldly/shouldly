#if !NET9_0_OR_GREATER
// ReSharper disable once CheckNamespace
namespace System.Diagnostics;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class DebuggerDisableUserUnhandledExceptionsAttribute : Attribute;
#endif
