using System;
using System.Linq;
using System.Reflection;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainATypeWithDefaultToStringMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return context.ShouldMethod.StartsWith("ShouldContain")
                   && ExpectedTypeHasDefaultToStringImpl(context);
        }

        private bool ExpectedTypeHasDefaultToStringImpl(ShouldlyAssertionContext context)
        {
            return TypeHasDefaultToString(context.Expected.GetType());
        }

        private bool TypeHasDefaultToString(Type type)
        {
            var ToStringMethodInfo = type.GetMethods().FirstOrDefault(m =>
                m.Name.Equals("ToString") &&
                m.ReturnType.Equals(typeof(String)) &&
                m.GetParameters().Length == 0);
            return (ToStringMethodInfo != null) && (ToStringMethodInfo.DeclaringType.FullName.Equals("System.Object"));
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            string message = string.Format(@"
    {0}
        {1}
    {2}
        but did not",
                codePart, context.ShouldMethod.PascalToSpaced(), context.Expected.Inspect());
            return message;
        }
    }
}