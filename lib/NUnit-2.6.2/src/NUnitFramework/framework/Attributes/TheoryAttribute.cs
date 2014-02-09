// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework
{
    using System;

    /// <summary>
    /// Adding this attribute to a method within a <seealso cref="TestFixtureAttribute"/> 
    /// class makes the method callable from the NUnit test runner. There is a property 
    /// called Description which is optional which you can provide a more detailed test
    /// description. This class cannot be inherited.
    /// </summary>
    /// 
    /// <example>
    /// [TestFixture]
    /// public class Fixture
    /// {
    ///   [Test]
    ///   public void MethodToTest()
    ///   {}
    ///   
    ///   [Test(Description = "more detailed description")]
    ///   publc void TestDescriptionMethod()
    ///   {}
    /// }
    /// </example>
    /// 
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited=true)]
    public class TheoryAttribute : Attribute
    {
        //private string description;

        ///// <summary>
        ///// Descriptive text for this test
        ///// </summary>
        //public string Description
        //{
        //    get { return description; }
        //    set { description = value; }
        //}
    }
}
