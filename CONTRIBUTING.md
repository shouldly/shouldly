![Icon](https://raw.github.com/shouldly/shouldly/master/package_icon.png)

Contributing to Shouldly
========================

**Getting started with Git and GitHub**

 * [Setting up Git for Windows and connecting to GitHub](http://help.github.com/win-set-up-git/)
 * [Forking a GitHub repository](http://help.github.com/fork-a-repo/)
 * [The simple guide to GIT guide](http://rogerdudler.github.com/git-guide/)
 * [Open an issue](https://github.com/shouldly/shouldly/issues) if you encounter a bug or have a suggestion for improvements/features
 * [Submit documentation improvements](http://shouldly.readthedocs.org/en/latest) by submitting pull requests, the docs are in the `docs` folder in this repo

Once you're familiar with Git and GitHub, clone the repository and start contributing.

**Potential setup issues and solutions**

`"C:\Program Files (x86)\MSBuild\Microsoft\VisualStudio\v14.0\DotNet\Microsoft.DotNet.Props" was not found.`

* Microsoft has [acknowledged this bug](https://docs.microsoft.com/en-us/dotnet/articles/core/windows-prerequisites) and it can be fixed by...
* Opening command prompt
* Navigating to the directory containing your Visual Studio upgrade installer ([get the installer here](https://www.microsoft.com/net/core#windows))
* Running the following: DotNetCore.1.0.0-VS2015Tools.Preview2.exe SKIP_VSU_CHECK=1
* Restarting Visual Studio
* Opening Shouldly solution

`Missing .NET Framework 3.5 reference`

* [Enable .NET 3.5 by following these instructions](https://msdn.microsoft.com/en-us/library/hh506443(v=vs.110).aspx#ControlPanel)

If you need inspiration for which issue to pick up have a look for the [Jump-In](https://github.com/shouldly/shouldly/labels/Jump-In) label on issues which are put on issues which are ready to be picked up by anyone. 