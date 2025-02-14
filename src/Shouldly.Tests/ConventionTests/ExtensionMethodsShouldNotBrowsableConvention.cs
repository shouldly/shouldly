using System.ComponentModel;
using System.Reflection;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace Shouldly.Tests.ConventionTests;

public class ExtensionMethodsShouldNotBeBrowsableConvention : IConvention<Types>
{
    public void Execute(Types data, IConventionResultContext result)
    {
        var shouldlyExtensionMethodClasses = data
            .Where(t =>
            {
                var methods = t.GetMethods()
                    .Where(m => m.IsStatic && m.Name.StartsWith("Should", StringComparison.Ordinal))
                    .ToList();

                return methods.Any() && methods.All(m =>
                    m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false)
                        .Any());
            })
            .ToList();

        List<string> failedClasses = [];

        foreach (var shouldlyExtensionMethodClass in shouldlyExtensionMethodClasses)
        {
            var browsableAttributes =
                shouldlyExtensionMethodClass.GetCustomAttributes<EditorBrowsableAttribute>()
                    .FirstOrDefault();

            if (browsableAttributes is null || browsableAttributes.State != EditorBrowsableState.Never)
            {
                failedClasses.Add(shouldlyExtensionMethodClass.FormatType());
            }
        }

        result.Is(
            "The following Shouldly extension classes are missing the EditorBrowsable(EditorBrowsableState.Never) attribute",
            failedClasses);
    }

    public string ConventionReason => "Inlining methods breaks our expression detection which uses the stack trace";
}