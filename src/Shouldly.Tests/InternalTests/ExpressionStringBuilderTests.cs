using System;
using System.Linq.Expressions;
using NUnit.Framework;
using Shouldly.Internals;

namespace Shouldly.Tests.InternalTests
{
    public class ExpressionStringBuilderTests
    {
        [Test]
        public void ToString_ReturnsReadableString()
        {
            Expression<Func<int, bool>> func = number => number > 4;

            string actual = ExpressionStringBuilder.ToString(func.Body);

            actual.ShouldBe("(number > 4)");
        }

        [Test]
        public void ToString_ClosureOperand_ReturnsReadableString()
        {
            var capturedOuterVariable = 4;
            Expression<Func<int, bool>> func = number => number > capturedOuterVariable;

            var actual = ExpressionStringBuilder.ToString(func.Body);

            actual.ShouldBe("(number > capturedOuterVariable)");
        }
    }
}