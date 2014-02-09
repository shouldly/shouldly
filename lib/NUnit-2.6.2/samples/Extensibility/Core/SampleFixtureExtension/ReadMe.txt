SampleSuiteExtension Example

This is a minimal example of a SuiteBuilder extension. It extends 
NUnit.Core.TestSuite test suite and creates a fixture that runs every 
test starting with "SampleTest..." It packages both the core extension
and the attribute used in the tests in the same assembly.

SampleSuiteExtension Class

This class derives from NUnit.Framework.TestSuite and represents the
extended suite within NUnit. Because it inherits from TestSuite,
rather than TestFixture, it has to construct its own fixture object and 
find its own tests. Everything is done in the constructor for simplicity.

SampleSuiteExtensionBuilder

This class is the actual SuiteBuilder loaded by NUnit as an add-in.
It recognizes the SampleSuiteExtensionAttribute and invokes the
SampleSuiteExtension constructor to build the suite.

SampleSuiteExtensionAttribute

This is the special attribute used to mark tests to be constructed
using this add-in. It is the only class referenced from the user tests.

Note on Building this Extension

If you use the Visual Studio solution, the NUnit references in both
included projects must be changed so that they refer to the copy of 
NUnit in which you want to install the extension. The post-build step 
for the SampleSuiteExtension project must be changed to copy the 
extension into the addins directory for your NUnit install.

NOTE:

The references to nunit.core and nunit.common in the 
SampleSuiteExtension project have their Copy Local property set to 
false, rather than the Visual Studio default of true. In developing
extensions, it is essential there be no extra copies of these assemblies
be created. Once the extension is complete, those who install it in
binary form will not need to deal with this issue.


