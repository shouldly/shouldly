namespace Shouldly;

/// <summary>
/// Finds a test method in the stack trace that has a specific attribute
/// </summary>
/// <typeparam name="T">The attribute type to search for</typeparam>
public class FindMethodUsingAttribute<T> : ITestMethodFinder where T : Attribute
{
    /// <summary>
    /// Gets test method information from the stack trace
    /// </summary>
    /// <param name="stackTrace">The stack trace to analyze</param>
    /// <param name="startAt">The frame index to start searching from</param>
    /// <returns>Information about the found test method</returns>
    public TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0)
    {
        foreach (var frame in stackTrace.GetFrames().Skip(startAt))
        {
            var method = frame.GetMethod();
            var originalMethod = GetOriginalMethodInfoForStateMachineMethod(method);
            method = originalMethod != null ? originalMethod.Value.DeclaringType.GetMethod(originalMethod.Value.MethodName) : method;

            if ((method?.IsDefined(typeof(T), inherit: true)).GetValueOrDefault())
            {
                return new(frame);
            }
        }

        throw new InvalidOperationException($"Cannot find a method in the stack trace with attribute {typeof(T).FullName}.");
    }

    private static OriginalMethodInfo? GetOriginalMethodInfoForStateMachineMethod(MethodBase? method)
    {
        if (method?.DeclaringType is { IsByRef: false } declaringType
            && declaringType.DeclaringType is { } originalMethodDeclaringType
            && ContainsAttribute(declaringType, "System.Runtime.CompilerServices.CompilerGeneratedAttribute")
            && declaringType.GetInterface("System.Runtime.CompilerServices.IAsyncStateMachine") is object)
        {
            var stateMachineTypeName = declaringType.Name;
            var openingAngleBracket = stateMachineTypeName.IndexOf('<');
            if (openingAngleBracket != -1)
            {
                var closingAngleBracket = stateMachineTypeName.IndexOf('>', openingAngleBracket + 1);
                if (closingAngleBracket != -1)
                {
                    var originalMethodName = stateMachineTypeName.Substring(openingAngleBracket + 1, closingAngleBracket - (openingAngleBracket + 1));

                    return new OriginalMethodInfo(originalMethodName, originalMethodDeclaringType);
                }
            }
        }

        return null;
    }

    private static bool ContainsAttribute(MemberInfo member, string attributeName) =>
        member.CustomAttributes.Any(a =>
            a.AttributeType.FullName?.StartsWith(attributeName, StringComparison.Ordinal) ?? false);

    private readonly struct OriginalMethodInfo
    {
        public OriginalMethodInfo(string methodName, Type declaringType)
        {
            MethodName = methodName;
            DeclaringType = declaringType;
        }

        public string MethodName { get; }
        public Type DeclaringType { get; }
    }
}