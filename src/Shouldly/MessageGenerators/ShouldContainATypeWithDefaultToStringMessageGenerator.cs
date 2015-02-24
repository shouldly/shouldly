using System;
using System.Linq;
using System.Reflection;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainATypeWithDefaultToStringMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return context.ShouldMethod.EndsWith("Contain")
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
            string format = @"
    {0}
        {1}
    {2}
        but does{3}";
            if (context.IsNegatedAssertion)
                return String.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Expected.ToStringAwesomely(), "");
            return String.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Expected.ToStringAwesomely(), " not");
        }
    }
}