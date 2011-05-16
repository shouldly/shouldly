using System;

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class ReusableConstraintTests
    {
        [Datapoints]
        static readonly ReusableConstraint[] constraints = new ReusableConstraint[] {
            Is.Not.Empty,
            Is.Not.Null,
            Has.Length.GreaterThan(3),
            Has.Property("Length").EqualTo(4).And.StartsWith("te")
        };

        [Theory]
        public void CanReuseReusableConstraintMultipleTimes(ReusableConstraint c)
        {
            string s = "test";

            Assume.That(s, c);

            Assert.That(s, c, "Should pass first time");
            Assert.That(s, c, "Should pass second time");
            Assert.That(s, c, "Should pass third time");
        }

        [Test]
        public void CanCreateReusableConstraintByImplicitConversion()
        {
            ReusableConstraint c = Is.Not.Null;

            string s = "test";
            Assert.That(s, c, "Should pass first time");
            Assert.That(s, c, "Should pass second time");
            Assert.That(s, c, "Should pass third time");
        }
    }
}
