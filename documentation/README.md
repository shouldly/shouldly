# Overview

![Shouldly Logo](https://raw.githubusercontent.com/shouldly/shouldly/master/assets/logo_350x84.png)

[![CI](https://github.com/shouldly/shouldly/actions/workflows/CI.yml/badge.svg?branch=master)](https://github.com/shouldly/shouldly/actions/workflows/CI.yml)
[![NuGet](https://img.shields.io/nuget/dt/shouldly.svg)](https://www.nuget.org/packages/Shouldly)
[![NuGet](https://img.shields.io/nuget/vpre/shouldly.svg)](https://www.nuget.org/packages/Shouldly)
[![Join the chat at https://gitter.im/shouldly/shouldly](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/shouldly/shouldly?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

Shouldly is an assertion framework which focuses on giving great error messages when the assertion fails while being simple and terse.

This is the old _Assert_ way:

```csharp
Assert.That(contestant.Points, Is.EqualTo(1337));
```

For your troubles, you get this message, when it fails:

```text
Expected 1337 but was 0
```

How it **Should** be:

```csharp
contestant.Points.ShouldBe(1337);
```

Which is just syntax, so far, but check out the message when it fails:

```text
contestant.Points should be 1337 but was 0
```

It might be easy to underestimate how useful this is. Another example, side by side:

```csharp
Assert.That(map.IndexOfValue("boo"), Is.EqualTo(2));
// -> Expected 2 but was -1

map.IndexOfValue("boo").ShouldBe(2);
// -> map.IndexOfValue("boo") should be 2 but was -1
```

**Shouldly** uses the code before the _ShouldBe_ statement to report on errors, which makes diagnosing easier.


## Installation

Shouldly can be [found here on NuGet](https://www.nuget.org/packages/Shouldly/) and can be installed by copying and pasting the following command into your [Package Manager Console within Visual Studio](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-powershell).

```bash
Install-Package Shouldly
```

Alternatively if you're using .NET Core then you can install Shouldly via the command line interface with the following command:

```bash
dotnet add package Shouldly
```


## Contributing

Contributions to Shouldly are very welcome. For guidance, please see [CONTRIBUTING.md](https://github.com/shouldly/shouldly/blob/master/CONTRIBUTING.md)


## Prerequisites for running on build server

Shouldly uses the source code to make its error messages better. Hence, on the build server you will need to have the "full" pdb files available where the tests are being run.

What is meant by "full" is that when you set up your "release" configuration in Visual Studio and you go to Project Properties &gt; Build &gt; Advanced &gt; Debug, you should set it to "full" rather than "pdb-only".


## Currently maintained by

* [Jake Ginnivan](https://github.com/JakeGinnivan)
* [Joseph Woodward](https://github.com/JosephWoodward)

If you are interested in helping out, jump on [Gitter](https://gitter.im/shouldly/shouldly) and have a chat.


## Brought to you by

* Dave Newman
* Xerxes Battiwalla
* Anthony Egerton
* Peter van der Woude
* Jake Ginnivan
