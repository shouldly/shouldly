![Shouldly Logo](https://raw.githubusercontent.com/shouldly/shouldly/master/assets/logo_350x84.png)  
========

[![CI](https://github.com/shouldly/shouldly/actions/workflows/CI.yml/badge.svg?branch=master)](https://github.com/shouldly/shouldly/actions/workflows/CI.yml)
[![NuGet](https://img.shields.io/nuget/dt/shouldly.svg)](https://www.nuget.org/packages/Shouldly) 
[![NuGet](https://img.shields.io/nuget/vpre/shouldly.svg)](https://www.nuget.org/packages/Shouldly)
[![Join the chat at https://gitter.im/shouldly/shouldly](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/shouldly/shouldly?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge) 

Shouldly is an assertion framework which focuses on giving great error messages when the assertion fails while being simple and terse.

This is the old *Assert* way: 
```cs
Assert.That(contestant.Points, Is.EqualTo(1337));
```
For your troubles, you get this message, when it fails:

    Expected 1337 but was 0

How it **Should** be:
```cs
contestant.Points.ShouldBe(1337);
```
Which is just syntax, so far, but check out the message when it fails:

    contestant.Points should be 1337 but was 0

It might be easy to underestimate how useful this is. Another example, side by side:
```cs
Assert.That(map.IndexOfValue("boo"), Is.EqualTo(2));    // -> Expected 2 but was -1
map.IndexOfValue("boo").ShouldBe(2);                    // -> map.IndexOfValue("boo") should be 2 but was -1
```
**Shouldly** uses the code before the *ShouldBe* statement to report on errors, which makes diagnosing easier.

Read more about Shouldly and its features at https://docs.shouldly.org/.

## Installation

Shouldly can be [found here on NuGet](https://www.nuget.org/packages/Shouldly/) and can be installed by copying and pasting the following command into your Package Manager Console within Visual Studio (Tools > NuGet Package Manager > Package Manager Console).

```bash
Install-Package Shouldly
```

Alternatively if you're using .NET Core then you can install Shouldly via the command line interface with the following command:

```bash
dotnet add package Shouldly
```

To have `ShouldMatchApproval` display a diff of the expected and actual files, you will need to install the `Shouldly.DiffEngine` package and configure it.

```bash
Install-Package Shouldly.ShouldlyShouldMatchApproved
```

```csharp
ShouldMatchConfiguration.ShouldMatchApprovedDefaults.ConfigureDiffEngine();
```

## Contributing
Contributions to Shouldly are very welcome. For guidance, please see [CONTRIBUTING.md](CONTRIBUTING.md)
