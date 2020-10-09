﻿using System;
using System.Collections;
using System.Linq;

namespace Shouldly.MessageGenerators
{
    internal class ShouldHaveSingleItemMessageGenerator : ShouldlyMessageGenerator
    {
        const string ShouldBeAssertion = "ShouldHaveSingleItem";

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod.Equals(ShouldBeAssertion, StringComparison.OrdinalIgnoreCase);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expected = context.Expected.ToStringAwesomely();
            var count = (context.Expected ?? Enumerable.Empty<object>()).As<IEnumerable>().Cast<object>().Count();
            var should = context.ShouldMethod.PascalToSpaced();
            if (codePart != "null")
            {
                return
                    $@"{codePart}
    {should} but had
{count}
    items and was
{expected}";
            }

            return
                $@"{expected}
    {should} but had
{count}
    items";
        }
    }
}
