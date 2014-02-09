// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework.Constraints;

namespace NUnit.Framework.Syntax
{
    [TestFixture]
    public class ThrowsTests
    {
        [Test]
        public void ThrowsException()
        {
            IResolveConstraint expr = Throws.Exception;
            Assert.AreEqual(
                "<throws>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsExceptionWithConstraint()
        {
            IResolveConstraint expr = Throws.Exception.With.Property("ParamName").EqualTo("myParam");
            Assert.AreEqual(
                @"<throws <property ParamName <equal ""myParam"">>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsExceptionTypeOf()
        {
            IResolveConstraint expr = Throws.Exception.TypeOf(typeof(ArgumentException));
            Assert.AreEqual(
                "<throws <typeof System.ArgumentException>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsTypeOf()
        {
            IResolveConstraint expr = Throws.TypeOf(typeof(ArgumentException));
            Assert.AreEqual(
                "<throws <typeof System.ArgumentException>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsTypeOfAndConstraint()
        {
            IResolveConstraint expr = Throws.TypeOf(typeof(ArgumentException)).And.Property("ParamName").EqualTo("myParam");
            Assert.AreEqual(
                @"<throws <and <typeof System.ArgumentException> <property ParamName <equal ""myParam"">>>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsExceptionTypeOfAndConstraint()
        {
            IResolveConstraint expr = Throws.Exception.TypeOf(typeof(ArgumentException)).And.Property("ParamName").EqualTo("myParam");
            Assert.AreEqual(
                @"<throws <and <typeof System.ArgumentException> <property ParamName <equal ""myParam"">>>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsTypeOfWithConstraint()
        {
            IResolveConstraint expr = Throws.TypeOf(typeof(ArgumentException)).With.Property("ParamName").EqualTo("myParam");
            Assert.AreEqual(
                @"<throws <and <typeof System.ArgumentException> <property ParamName <equal ""myParam"">>>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsTypeofWithMessage()
        {
            IResolveConstraint expr = Throws.TypeOf(typeof(ArgumentException)).With.Message.EqualTo("my message");
            Assert.AreEqual(
                @"<throws <and <typeof System.ArgumentException> <property Message <equal ""my message"">>>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsInstanceOf()
        {
            IResolveConstraint expr = Throws.InstanceOf(typeof(ArgumentException));
            Assert.AreEqual(
                "<throws <instanceof System.ArgumentException>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsExceptionInstanceOf()
        {
            IResolveConstraint expr = Throws.Exception.InstanceOf(typeof(ArgumentException));
            Assert.AreEqual(
                "<throws <instanceof System.ArgumentException>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsInnerException()
        {
            IResolveConstraint expr = Throws.InnerException.TypeOf(typeof(ArgumentException));
            Assert.AreEqual(
                "<throws <property InnerException <typeof System.ArgumentException>>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsExceptionWithInnerException()
        {
            IResolveConstraint expr = Throws.Exception.With.InnerException.TypeOf(typeof(ArgumentException));
            Assert.AreEqual(
                "<throws <property InnerException <typeof System.ArgumentException>>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsTypeOfWithInnerException()
        {
            IResolveConstraint expr = Throws.TypeOf(typeof(System.Reflection.TargetInvocationException))
                .With.InnerException.TypeOf(typeof(ArgumentException));
            Assert.AreEqual(
                "<throws <and <typeof System.Reflection.TargetInvocationException> <property InnerException <typeof System.ArgumentException>>>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsTargetInvocationExceptionWithInnerException()
        {
            IResolveConstraint expr = Throws.TargetInvocationException
                .With.InnerException.TypeOf(typeof(ArgumentException));
            Assert.AreEqual(
                "<throws <and <typeof System.Reflection.TargetInvocationException> <property InnerException <typeof System.ArgumentException>>>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsArgumentException()
        {
            IResolveConstraint expr = Throws.ArgumentException;
            Assert.AreEqual(
                "<throws <typeof System.ArgumentException>>",
                expr.Resolve().ToString());
        }

        [Test]
        public void ThrowsInvalidOperationException()
        {
            IResolveConstraint expr = Throws.InvalidOperationException;
            Assert.AreEqual(
                "<throws <typeof System.InvalidOperationException>>",
                expr.Resolve().ToString());
        }

        // TODO: Move these to AssertThat tests
#if CLR_2_0 || CLR_4_0
#if CS_3_0 || CS_4_0 || CS_5_0
        [Test]
        public void DelegateThrowsException()
        {
            Assert.That(
                delegate { throw new ApplicationException(); },
                Throws.Exception);
        }

        [Test]
        public void LambdaThrowsException()
        {
            Assert.That(
                () => new MyClass(null),
                Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void LambdaThrowsExceptionWithMessage()
        {
            Assert.That(
                () => new MyClass(null),
                Throws.InstanceOf<ArgumentNullException>());
        }

        internal class MyClass
        {
            public MyClass(string s)
            {
                if (s == null)
                {
                    throw new ArgumentNullException();
                }
            }
        }

        [Test]
        public void LambdaThrowsNothing()
        {
            Assert.That(
                () => null,
                Throws.Nothing);
        }
#else
        [Test]
        public void DelegateThrowsException()
        {
            Assert.That(
                delegate { Throw(); return; },
                Throws.Exception);
        }

        // Encapsulate throw to trick compiler and
        // avoid unreachable code warning. Can't
        // use pragma because this is also compiled
        // under the .NET 1.0 and 1.1 compilers.
        private void Throw()
        {
            throw new ApplicationException();
        }
#endif
#endif
    }
}
