#if ShouldMatchApproved
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Shouldly.Configuration
{
    public class TestMethodInfo
    {
        public TestMethodInfo(StackFrame callingFrame)
        {
            SourceFileDirectory = Path.GetDirectoryName(callingFrame.GetFileName());
            var realMethod = GetRealMethod(callingFrame.GetMethod());
            MethodName = realMethod.Name;
            DeclaringTypeName = realMethod.DeclaringType?.Name;
        }

        static MethodBase GetRealMethod(MethodBase method)
        {
            var declaringType = method.DeclaringType;
            if (declaringType == null || declaringType.IsByRef)
            {
                return method;
            }
#if NETSTANDARD2_0
            if (!ContainsAttribute(declaringType.GetTypeInfo().GetCustomAttributes(false) as object[], "System.Runtime.CompilerServices.CompilerGeneratedAttribute"))
            {
                return method;
            }
            if (declaringType.GetTypeInfo().GetInterface("System.Runtime.CompilerServices.IAsyncStateMachine") == null)
            {
                return method;
            }
#else
            if (!ContainsAttribute(declaringType.GetCustomAttributes(false), "System.Runtime.CompilerServices.CompilerGeneratedAttribute"))
            {
                return method;
            }
            if (declaringType.GetInterface("System.Runtime.CompilerServices.IAsyncStateMachine") == null)
            {
                return method;
            }
#endif
            if (!declaringType.Name.Contains("<") || !declaringType.Name.Contains(">"))
            {
                return method;
            }
            var trueMethodName = declaringType.Name.TrimStart('<').Split('>').First();
            MethodInfo methodInfo;
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            try
            {
                methodInfo = declaringType.DeclaringType.GetMethod(trueMethodName, bindingFlags);
            }
            catch (AmbiguousMatchException)
            {
                //TODO: Should this throw??
                //var message = string.Format("Could not derive root method for async method '{0}' since there are multiple methods named '{1}'.", method.Name, trueMethodName);
                return method;
            }
            return methodInfo;
        }

        static bool ContainsAttribute(object[] attributes, string attributeName)
        {
            {
                return attributes.Any(a => a.GetType().FullName.StartsWith(attributeName));
            }
        }

        public string SourceFileDirectory { get; private set; }
        public string MethodName { get; private set; }
        public string DeclaringTypeName { get; private set; }
    }
}
#endif