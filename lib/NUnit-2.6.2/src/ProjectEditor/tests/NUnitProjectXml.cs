// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;

namespace NUnit.ProjectEditor.Tests
{
	/// <summary>
	/// Summary description for NUnitProjectXml.
	/// </summary>
	public class NUnitProjectXml
	{
		public static readonly string EmptyProject = "<NUnitProject />";
		
		public static readonly string EmptyConfigs = 
			"<NUnitProject>" + System.Environment.NewLine +
			"  <Settings activeconfig=\"Debug\" />" + System.Environment.NewLine +
			"  <Config name=\"Debug\" binpathtype=\"Auto\" />" + System.Environment.NewLine +
			"  <Config name=\"Release\" binpathtype=\"Auto\" />" + System.Environment.NewLine +
			"</NUnitProject>";

        public static readonly string NormalProject =
            "<NUnitProject>" + System.Environment.NewLine +
            "  <Settings activeconfig=\"Debug\" />" + System.Environment.NewLine +
            "  <Config name=\"Debug\" appbase=\"bin" + Path.DirectorySeparatorChar + "debug\" binpathtype=\"Auto\">" + System.Environment.NewLine +
            "    <assembly path=\"assembly1.dll\" />" + System.Environment.NewLine +
            "    <assembly path=\"assembly2.dll\" />" + System.Environment.NewLine +
            "  </Config>" + System.Environment.NewLine +
            "  <Config name=\"Release\" appbase=\"bin" + Path.DirectorySeparatorChar + "release\" binpathtype=\"Auto\">" + System.Environment.NewLine +
            "    <assembly path=\"assembly1.dll\" />" + System.Environment.NewLine +
            "    <assembly path=\"assembly2.dll\" />" + System.Environment.NewLine +
            "  </Config>" + System.Environment.NewLine +
            "</NUnitProject>";

        public static readonly string ManualBinPathProject =
			"<NUnitProject>" + System.Environment.NewLine +
			"  <Settings activeconfig=\"Debug\" />" + System.Environment.NewLine +
			"  <Config name=\"Debug\" binpath=\"bin_path_value\"  /> " + System.Environment.NewLine +
			"</NUnitProject>";

        public static readonly string ComplexSettingsProject =
        "<NUnitProject>" + System.Environment.NewLine +
        "  <Settings activeconfig=\"Release\" appbase=\"bin\" processModel=\"Separate\" domainUsage=\"Multiple\" />" + System.Environment.NewLine +
        "  <Config name=\"Debug\" appbase=\"debug\" binpathtype=\"Auto\" runtimeFramework=\"v2.0\">" + System.Environment.NewLine +
        "    <assembly path=\"assembly1.dll\" />" + System.Environment.NewLine +
        "    <assembly path=\"assembly2.dll\" />" + System.Environment.NewLine +
        "  </Config>" + System.Environment.NewLine +
        "  <Config name=\"Release\" appbase=\"release\" binpathtype=\"Auto\" runtimeFramework=\"v4.0\">" + System.Environment.NewLine +
        "    <assembly path=\"assembly1.dll\" />" + System.Environment.NewLine +
        "    <assembly path=\"assembly2.dll\" />" + System.Environment.NewLine +
        "  </Config>" + System.Environment.NewLine +
        "</NUnitProject>";
    }
}
