// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Text;
using System.Xml;
using System.IO;
using NUnit.Framework;

namespace NUnit.ProjectEditor.Tests.Model
{
	[TestFixture]
	public class NUnitProjectSave
	{
        private ProjectDocument doc;
        private ProjectModel project;
        private string xmlfile;

		[SetUp]
		public void SetUp()
		{
            doc = new ProjectDocument();
            project = new ProjectModel(doc);
            doc.CreateNewProject();
            xmlfile = Path.ChangeExtension(Path.GetTempFileName(), ".nunit");
		}

		[TearDown]
		public void TearDown()
		{
			if ( File.Exists( xmlfile ) )
				File.Delete( xmlfile );
		}

		[Test]
		public void EmptyProject()
		{
			CheckContents( NUnitProjectXml.EmptyProject );
		}

		[Test]
		public void EmptyConfigs()
		{
            project.AddConfig("Debug");
            project.AddConfig("Release");
            project.ActiveConfigName = "Debug";
            project.Configs["Debug"].BinPathType = BinPathType.Auto;
            project.Configs["Release"].BinPathType = BinPathType.Auto;

			CheckContents( NUnitProjectXml.EmptyConfigs );			
		}

        [Test]
        public void NormalProject()
        {
            IProjectConfig config1 = project.AddConfig("Debug");
            config1.BasePath = "bin" + Path.DirectorySeparatorChar + "debug";
            config1.BinPathType = BinPathType.Auto;
            config1.Assemblies.Add("assembly1.dll");
            config1.Assemblies.Add("assembly2.dll");

            IProjectConfig config2 = project.AddConfig("Release");
            config2.BasePath = "bin" + Path.DirectorySeparatorChar + "release";
            config2.BinPathType = BinPathType.Auto;
            config2.Assemblies.Add("assembly1.dll");
            config2.Assemblies.Add("assembly2.dll");

            project.ActiveConfigName = "Debug";

            CheckContents(NUnitProjectXml.NormalProject);
        }

        [Test]
        public void NormalProject_RoundTrip()
        {
            doc.LoadXml(NUnitProjectXml.NormalProject);
            CheckContents(NUnitProjectXml.NormalProject);
        }

        [Test]
        public void ProjectWithComplexSettings()
        {
            IProjectConfig config1 = project.AddConfig("Debug");
            config1.BasePath = "debug";
            config1.BinPathType = BinPathType.Auto;
            config1.RuntimeFramework = new RuntimeFramework(RuntimeType.Any, new Version(2, 0));
            config1.Assemblies.Add("assembly1.dll");
            config1.Assemblies.Add("assembly2.dll");

            IProjectConfig config2 = project.AddConfig("Release");
            config2.BasePath = "release";
            config2.BinPathType = BinPathType.Auto;
            config2.RuntimeFramework = new RuntimeFramework(RuntimeType.Any, new Version(4, 0));
            config2.Assemblies.Add("assembly1.dll");
            config2.Assemblies.Add("assembly2.dll");

            project.ActiveConfigName = "Release";
            project.BasePath = "bin";
            project.ProcessModel = "Separate";
            project.DomainUsage = "Multiple";

            CheckContents(NUnitProjectXml.ComplexSettingsProject);
        }

        [Test]
        public void ProjectWithComplexSettings_RoundTrip()
        {
            doc.LoadXml(NUnitProjectXml.ComplexSettingsProject);
            CheckContents(NUnitProjectXml.ComplexSettingsProject);
        }

        [Test]
        public void ProjectWithComplexSettings_RoundTripWithChanges()
        {
            doc.LoadXml(NUnitProjectXml.ComplexSettingsProject);
            project.ProcessModel = "Single";
            CheckContents(NUnitProjectXml.ComplexSettingsProject
                .Replace("Separate", "Single"));
        }

        private void CheckContents(string expected)
        {
            doc.Save(xmlfile);
            StreamReader reader = new StreamReader(xmlfile);
            string contents = reader.ReadToEnd();
            reader.Close();
            Assert.That(contents, Is.EqualTo(expected));
        }
    }
}
