![Icon](https://raw.github.com/shouldly/shouldly/master/package_icon.png)

Shouldly
========

[![Join the chat at https://gitter.im/shouldly/shouldly](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/shouldly/shouldly?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
Shouldly is an assertion framework which focuses on giving great error messages when the assertion fails while being simple and terse.

This is the old *Assert* way: 

    Assert.That(contestant.Points, Is.EqualTo(1337));

For your troubles, you get this message, when it fails:

    Expected 1337 but was 0

How it **Should** be:

    contestant.Points.ShouldBe(1337);

Which is just syntax, so far, but check out the message when it fails:

    contestant.Points should be 1337 but was 0

It might be easy to underestimate how useful this is. Another example, side by side:

    Assert.That(map.IndexOfValue("boo"), Is.EqualTo(2));    // -> Expected 2 but was 1
    map.IndexOfValue("boo").ShouldBe(2);                    // -> map.IndexOfValue("boo") should be 2 but was 1

**Shouldly** uses the code before the *ShouldBe* statement to report on errors, which makes diagnosing easier.

Read more about Shouldly and it's features at [http://docs.shouldly-lib.net/](http://docs.shouldly-lib.net/)

## Contributing
**Getting started with Git and GitHub**

 * [Setting up Git for Windows and connecting to GitHub](http://help.github.com/win-set-up-git/)
 * [Forking a GitHub repository](http://help.github.com/fork-a-repo/)
 * [The simple guide to GIT guide](http://rogerdudler.github.com/git-guide/)
 * [Open an issue](https://github.com/shouldly/shouldly/issues) if you encounter a bug or have a suggestion for improvements/features
 * [Submit documentation improvements](http://docs.shouldly-lib.net) by clicking 'Suggest edit' on any docs

Once you're familiar with Git and GitHub, clone the repository and start contributing.

If you need inspiration for which issue to pick up have a look for the [Jump-In](https://github.com/shouldly/shouldly/labels/Jump-In) label on issues which are put on issues which are ready to be picked up by anyone. 

## Pre-requisites for running on build server
Shouldly uses the source code to make its error messages better. Hence, on the build server you will need to have the "full" pdb files available where the tests are being run. 

What is meant by "full" is that when you set up your "release" configuration in Visual Studio and you go to Project Properties > Build > Advanced > Debug, you should set it to "full" rather than "pdb-only". 

## Icon
[Star](https://thenounproject.com/term/star/20931/) created by [Luboš Volkov](https://thenounproject.com/Lubo%C5%A1%20Volkov/) from The Noun Project