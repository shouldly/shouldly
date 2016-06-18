# Release Process
The Shouldly release process is pretty easy now, there are two small manual steps to give you a chance to edit release notes.

Deployments are initiated through AppVeyor, navigate to the build you want to deploy at [https://ci.appveyor.com/project/shouldly/shouldly](https://ci.appveyor.com/project/shouldly/shouldly), then press the `Deploy` button.

![Deploy Button](https://raw.githubusercontent.com/shouldly/shouldly/master/docs/readme/ReleaseProcess_1.png)

Choose the GitHub Release environment

![Deploy GitHub Release](https://raw.githubusercontent.com/shouldly/shouldly/master/docs/readme/ReleaseProcess_2.png)

And deploy

![Deployment](https://raw.githubusercontent.com/shouldly/shouldly/master/docs/readme/ReleaseProcess_3.png)

Once the release completes, head to the releases section on GitHub

![GitHub Release](https://raw.githubusercontent.com/shouldly/shouldly/master/docs/readme/ReleaseProcess_4.png)

And download the release notes, then edit the draft release

![GitHub Release](https://raw.githubusercontent.com/shouldly/shouldly/master/docs/readme/ReleaseProcess_5.png)

Remove the build meta data from the tag and release title

![GitHub Release](https://raw.githubusercontent.com/shouldly/shouldly/master/docs/readme/ReleaseProcess_6.png)

Copy the contents of the release notes file into the release description field and make any manual cleanups you need to. Then Publish the release

![GitHub Release](https://raw.githubusercontent.com/shouldly/shouldly/master/docs/readme/ReleaseProcess_7.png)


Make sure deployment from [Shouldly-deploy](https://ci.appveyor.com/project/shouldly/shouldly-lk0o2) build succeeds