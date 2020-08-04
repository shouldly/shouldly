.. _gettingStarted:

Getting Started
===============

.. A shorcut to create a bash code block (see https://stackoverflow.com/questions/10870719/inline-code-highlighting-in-restructuredtext)

.. role:: bash(code)
   :language: bash

Here are some guides to setup a Shouldly project.

.. contents::

Setup a project with Dotnet CLI
-------------------------------

In this guide, we will set up a project with Shouldly unit tests.

We won't use Visual Studio, we will build our project bare bones with Dotnet CLI and a text editor.

Prerequisites
^^^^^^^^^^^^^^

- You need to install `Dotnet CLI Tools <https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install>`_.

- You need to install a text editor (Notepad++, VSCode, etc...).

Creating the main program
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

The project structure will be as follows:

.. code-block:: none

    /project-name
        project-name.sln
        /program
            ...
        /test
            ...

#. Create your root directory, name it your project name.
#. Open a terminal in your root directory.
#. Run :bash:`dotnet new sln`.
#. Go into program folder and run :bash:`dotnet new classlib`.
#. Rename *Class1.cs* to *Program.cs*.
#. Add the following code to *Program.cs*:

   .. code-block:: c#

    using System;

    namespace project_name
    {
        public static class Program
        {
            public static string testWorks()
            {
                return "Works";
            }
        }
    }

#. Go to the root folder and run :bash:`dotnet sln add program/program.csproj`, in order to add *program* to your solution.


Creating unit tests
^^^^^^^^^^^^^^^

In this part we will create the unit tests for the main program.

#. Move to test folder and run :bash:`dotnet new nunit` *(you can use others unit tests frameworks but in this guide we will stick to nunit)*.
#. Add a refrence to the main program by running :bash:`dotnet add reference ../program/program.csproj`
#. In the root folder add the tests to the solution by running :bash:`dotnet sln add test/test.csproj`.


Adding Shouldly
^^^^^^^^^^^^^^^

Now comes the important part, when we actually add Shouldly.

#. Go to the test folder and run :bash:`dotnet add package Shouldly`, to add Shouldly as a NuGet Package.
#. Add the following code to *UnitTest1.cs*:

   .. code-block:: c#

    using NUnit.Framework;
    using project_name;
    using Shouldly;

    namespace Tests
    {
        public class Tests
        {

            [Test]
            public void Test1()
            {
                Program.testWorks().ShouldBe("Works");
            }
        }
    }

Testing
^^^^^^^

To test your project, in your root folder run :bash:`dotnet test`.

And there you go, you have a basic project with unit tests using Shouldly.

Summary
^^^^^^^

We created a classlib project using Dotnet CLI Tools,
then added unit tests and added Shouldly as a NuGet package to be able to use Shouldly in our tests.