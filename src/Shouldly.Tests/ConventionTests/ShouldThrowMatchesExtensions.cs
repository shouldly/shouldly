﻿using System.Linq;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace Shouldly.Tests.ConventionTests
{
    public class ShouldThrowMatchesExtensionsConvention : IConvention<Types>
    {
        public void Execute(Types data, IConventionResultContext result)
        {
            var shouldThrowMethods = data
                .SelectMany(t => t.GetMethods())
                .Where(method =>
                    method.Name.StartsWith("Throw") || method.Name.StartsWith("ShouldThrow") ||
                    method.Name.StartsWith("NotThrow") || method.Name.StartsWith("ShouldNotThrow"))
                .Select(throwMethod => new ShouldThrowMethod(throwMethod))
                .ToList();

            var extensionMethods = shouldThrowMethods.Where(m => m.IsShouldlyExtension).ToList();
            var staticMethods = shouldThrowMethods.Where(m => !m.IsShouldlyExtension).ToList();

            var firstSetFailureData = staticMethods.Where(e => !extensionMethods.Any(e.Equals)).ToList();
            var secondSetFailureData = extensionMethods.Where(e => !staticMethods.Any(e.Equals)).ToList();
            result.IsSymmetric(
                "Should.Throw method without corresponding ShouldThrow extension method",
                firstSetFailureData,
                "ShouldThrow extension method without Should.Throw static method",
                secondSetFailureData);
        }

        public string ConventionReason =>
            "Keep Should.Throw methods in sync with the extension methods";
    }
}