Minimal Addin Example


MinimalAddin Class

This class represents the addin. It is marked by the NUnitAddinAttribute
and implements the required IAddin interface. When called by NUnit to
install itself, it simply returns false.

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


