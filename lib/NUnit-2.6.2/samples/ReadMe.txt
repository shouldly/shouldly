NUnit Samples

This directory contains sample applications demonstrating the use of NUnit and organized as follows...

  CSharp: Samples in C#

    Failures: Demonstrates 4 failing tests and one that is not run.

    Money: This is a C# version of the money example which is found in most xUnit implementations. Thanks to Kent Beck.

    Syntax: Illustrates most Assert methods using both the classic and constraint-based syntax.

  FSHARP: Samples in F#

    Failures: Demonstrates 4 failing tests and one that is not run.

    Money: This is an F# version of the money example which is found in most xUnit implementations. Thanks to Kent Beck.

    Syntax: Illustrates most Assert methods using both the classic and constraint-based syntax.

  CPP: C++ Samples

   MANAGED: Managed C++ Samples (VS 2003 compatible)

    Failures: Demonstrates 4 failing tests and one that is not run.

   CPP-CLI: C++/CLI Samples

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

A Visual Studio 2010 project is included for each sample. 

In most cases, you will need to remove the reference to the
nunit.framework assembly and replace it with a reference to 
your installed copy of NUnit.
