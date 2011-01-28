NUnit Samples

This directory contains sample applications demonstrating the use of NUnit and organized as follows...

  CSharp: Samples in C#

    Failures: Demonstrates 4 failing tests and one that is not run.

    Money: This is a C# version of the money example which is found in most xUnit implementations. Thanks to Kent Beck.

    Money-Port: This shows how the Money example can be ported from Version 1 of NUnit with minimal changes.

    Syntax: Illustrates most Assert methods using both the classic and constraint-based syntax.

  JSharp: Samples in J#

    Failures: Demonstrates 4 failing tests and one that is not run.

  CPP: C++ Samples

   MANAGED: Managed C++ Samples (VS 2003 compatible)

    Failures: Demonstrates 4 failing tests and one that is not run.

   CPP-CLI: C++/CLI Samples (VS 2005 only)

    Failures: Demonstrates 4 failing tests and one that is not run.

    Syntax: Illustrates most Assert methods using both the classic and constraint-based syntax.

  VB: Samples in VB.NET

    Failures: Demonstrates 4 failing tests and one that is not run.

    Money: This is a VB.NET version of the money example found in most xUnit implementations. Thanks to Kent Beck.

    Syntax: Illustrates most Assert methods using both the classic and constraint-based syntax.

  Extensibility: Examples of extending NUnit

    Framework:

    Core:
    
      TestSuiteExtension

      TestFixtureExtension


Building the Samples

A Visual Studio 2003 project is included for most samples. 
Visual Studio 2005 will convert the format automatically upon
opening it. The C++/CLI samples, as well as other samples that
depend on .NET 2.0 features, include Visual Studio 2005 projects.

In most cases, you will need to remove the reference to the
nunit.framework assembly and replace it with a reference to 
your installed copy of NUnit.

To build using the Microsoft compiler, use a command similar 
to the following:

  csc /target:library /r:<path-to-NUnit>/nunit.framework.dll example.cs

To build using the mono compiler, use a command like this:

  msc /target:library /r:<path-to-NUNit>/nunit.framework.dll example.cs  

