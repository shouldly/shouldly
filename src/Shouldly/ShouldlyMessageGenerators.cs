using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Shouldly
{
    internal class DynamicShouldMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("HaveProperty", RegexOptions.Compiled);
        private static readonly Regex DynamicObjectNameExtractor = new Regex(@"DynamicShould.HaveProperty\((?<dynamicObjectName>.*),(?<propertyName>.*)\)", RegexOptions.Compiled);
        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod);
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format =  @"
    Dynamic object
        ""{0}""
    should contain property
        {1}
    but does not.";

            var testFileName = environment.OriginatingFrame.GetFileName();
            var assertionLineNumber = environment.OriginatingFrame.GetFileLineNumber();

            var codeLine = string.Join("", File.ReadAllLines(testFileName).ToArray().Skip(assertionLineNumber - 1).Select(s => s.Trim()));
            var dynamicObjectName = DynamicObjectNameExtractor.Match(codeLine).Groups["dynamicObjectName"];
            var propertyName = DynamicObjectNameExtractor.Match(codeLine).Groups["propertyName"];

            return String.Format(format, dynamicObjectName, propertyName);
        }
    }
    internal class DictionaryShouldNotContainValueForKeyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldNotContainValueForKey", RegexOptions.Compiled);
        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod);
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    Dictionary
        ""{0}""
    should not contain key
        ""{1}""
    with value
        ""{2}""
    {3}";

            var codePart = environment.GetCodePart();
            var expectedValue = environment.Expected.Inspect();
            var actualValue = environment.Actual.Inspect();
            var keyValue = environment.Key.Inspect();

            if (environment.HasKey)
            {
                var valueString = "but does";
                return String.Format(format, codePart, keyValue.Trim('"'), expectedValue.Trim('"'), valueString);
            }
            else
            {
                return String.Format(format, codePart, actualValue.Trim('"'), expectedValue.Trim('"'), "but the key does not exist");
            }
        }
    }

    internal class DictionaryShouldContainKeyAndValueMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldContainKeyAndValue", RegexOptions.Compiled);
        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod);
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    Dictionary
        ""{0}""
    should contain key
        ""{1}""
    with value
        ""{2}""
    {3}";

            var codePart = environment.GetCodePart();
            var expectedValue = environment.Expected.Inspect();
            var actualValue = environment.Actual.Inspect();
            var keyValue = environment.Key.Inspect();

            if (environment.HasKey)
            {
                var valueString = string.Format("but value was \"{0}\"", actualValue.Trim('"'));
                return String.Format(format, codePart, keyValue.Trim('"'), expectedValue.Trim('"'), valueString);
            }
            else
            {
                return String.Format(format, codePart, actualValue.Trim('"'), expectedValue.Trim('"'), "but the key does not exist");
            }
        }
    }

    internal class DictionaryShouldOrNotConatinKeyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?ContainKey", RegexOptions.Compiled);
        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod) && !environment.HasActual;
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    Dictionary
        ""{0}""
    {1}
        ""{2}""
    but does {3}";

            var codePart = environment.GetCodePart();
            var expectedValue = environment.Expected.Inspect();

            if (environment.IsNegatedAssertion)
                return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), environment.Expected, "");

            return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), expectedValue.Trim('"'), "not");
        }
    }

    internal class ShouldBeNullOrEmptyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?BeNullOrEmpty", RegexOptions.Compiled);
        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod) && !environment.HasActual;
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    {0}
            {1}";

            var codePart = environment.GetCodePart();
            var expectedValue = environment.Expected.Inspect();

            if (environment.IsNegatedAssertion)
                return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), environment.Expected == null ? "null" : "");

            return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), expectedValue);
        }
    }

    internal class ShouldBeEmptyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?BeEmpty", RegexOptions.Compiled);

        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod) && !environment.HasActual;
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    {0}
            {1}
        but was {2}";

            var codePart = environment.GetCodePart();
            var expectedValue = environment.Expected.Inspect();

            if (environment.IsNegatedAssertion)
                return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), environment.Expected == null ? "null" : "");

            return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), expectedValue);
        }
    }

    internal abstract class ShouldlyMessageGenerator
    {
        public abstract bool CanProcess(TestEnvironment environment);
        public abstract string GenerateErrorMessage(TestEnvironment environment);
    }
}