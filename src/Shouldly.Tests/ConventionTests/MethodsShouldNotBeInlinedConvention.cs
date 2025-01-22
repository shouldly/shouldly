using System.Reflection;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace Shouldly.Tests.ConventionTests;

public class MethodsShouldNotBeInlinedConvention : IConvention<Types>
{
    public void Execute(Types data, IConventionResultContext result)
    {
        var shouldlyMethods = data
            .SelectMany(t => t.GetMethods())
            .Where(m => m.IsStatic)
            .ToList();

        var extensionMethods = shouldlyMethods.Where(m => m.Name.StartsWith("Should", StringComparison.Ordinal)).ToList();
        var staticMethods = shouldlyMethods.Where(m => !m.Name.StartsWith("Should", StringComparison.Ordinal)).ToList();

        List<string> failedMethods = []; 
        foreach (var extensionMethod in extensionMethods.Concat(staticMethods))
        {
            var flags = extensionMethod.GetMethodImplementationFlags();
            if (!flags.HasFlag(MethodImplAttributes.NoInlining))
            {
                failedMethods.Add(extensionMethod.FormatMethod());
            }
        }
        
        result.Is(
            $"The following Shouldly methods are missing the NoInlining attribute",
            failedMethods);
    }

    public string ConventionReason => "Inlining methods breaks our expression detection which uses the stack trace";
}