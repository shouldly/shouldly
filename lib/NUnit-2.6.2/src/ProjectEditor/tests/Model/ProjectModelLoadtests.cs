// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.IO;
using NUnit.Framework;

namespace NUnit.ProjectEditor.Tests.Model
{
    [TestFixture]
    public class ProjectModelLoadtests
    {
        private ProjectDocument doc;
        private ProjectModel project;
        private string xmlfile;

        [SetUp]
        public void SetUp()
        {
            xmlfile = Path.ChangeExtension(Path.GetTempFileName(), ".nunit");
            doc = new ProjectDocument(xmlfile);
            project = new ProjectModel(doc);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(xmlfile))
                File.Delete(xmlfile);
        }

        [Test]
        public void LoadEmptyProject()
        {
            doc.LoadXml(NUnitProjectXml.EmptyProject);

            Assert.AreEqual(Path.GetFullPath(xmlfile), project.ProjectPath);
            
            Assert.AreEqual(null, project.BasePath);
            Assert.AreEqual(Path.GetDirectoryName(project.ProjectPath), project.EffectiveBasePath);

            Assert.AreEqual("Default", project.ProcessModel);
            Assert.AreEqual("Default", project.DomainUsage);

            Assert.AreEqual(0, project.Configs.Count);
            Assert.AreEqual(0, project.ConfigNames.Length);

            Assert.AreEqual(null, project.ActiveConfigName);
        }

        [Test]
        public void LoadEmptyConfigs()
        {
            doc.LoadXml(NUnitProjectXml.EmptyConfigs);

            Assert.AreEqual(Path.GetFullPath(xmlfile), project.ProjectPath);

            Assert.AreEqual(null, project.BasePath);
            Assert.AreEqual(Path.GetDirectoryName(project.ProjectPath), project.EffectiveBasePath);

            Assert.AreEqual("Default", project.ProcessModel);
            Assert.AreEqual("Default", project.DomainUsage);

            Assert.AreEqual(2, project.Configs.Count);
            Assert.AreEqual(new string[] { "Debug", "Release" }, project.ConfigNames);

            Assert.AreEqual("Debug", project.ActiveConfigName);

            Assert.AreEqual("Debug", project.Configs[0].Name);
            Assert.AreEqual("Release", project.Configs[1].Name);
        }

        [Test]
        public void LoadNormalProject()
        {
            doc.LoadXml(NUnitProjectXml.NormalProject);

            Assert.AreEqual(Path.GetFullPath(xmlfile), project.ProjectPath);

            Assert.AreEqual(null, project.BasePath);
            Assert.AreEqual(Path.GetDirectoryName(project.ProjectPath), project.EffectiveBasePath);

            Assert.AreEqual("Default", project.ProcessModel);
            Assert.AreEqual("Default", project.DomainUsage);

            Assert.AreEqual(2, project.Configs.Count);
            Assert.AreEqual(new string[] { "Debug", "Release" }, project.ConfigNames);

            Assert.AreEqual("Debug", project.ActiveConfigName);

            IProjectConfig config1 = project.Configs[0];
            Assert.AreEqual(2, config1.Assemblies.Count);
            Assert.AreEqual(
                "assembly1.dll",
                config1.Assemblies[0]);
            Assert.AreEqual(
                "assembly2.dll",
                config1.Assemblies[1]);

            IProjectConfig config2 = project.Configs[1];
            Assert.AreEqual(2, config2.Assemblies.Count);
            Assert.AreEqual(
                "assembly1.dll",
                config2.Assemblies[0]);
            Assert.AreEqual(
                "assembly2.dll",
                config2.Assemblies[1]);
        }

        [Test]
        public void LoadProjectWithManualBinPath()
        {
            doc.LoadXml(NUnitProjectXml.ManualBinPathProject);

            Assert.AreEqual(Path.GetFullPath(xmlfile), project.ProjectPath);

            Assert.AreEqual(null, project.BasePath);
            Assert.AreEqual(Path.GetDirectoryName(project.ProjectPath), project.EffectiveBasePath);

            Assert.AreEqual("Default", project.ProcessModel);
            Assert.AreEqual("Default", project.DomainUsage);

            Assert.AreEqual(1, project.Configs.Count);
            Assert.AreEqual(new string[] { "Debug" }, project.ConfigNames);

            IProjectConfig config1 = project.Configs["Debug"];
            Assert.AreEqual("bin_path_value", config1.PrivateBinPath);
        }

        [Test]
        public void LoadProjectWithComplexSettings()
        {
            doc.LoadXml(NUnitProjectXml.ComplexSettingsProject);
            Assert.AreEqual("bin", project.BasePath);
            Assert.AreEqual("Separate", project.ProcessModel);
            Assert.AreEqual("Multiple", project.DomainUsage);

            Assert.AreEqual(2, project.Configs.Count);

            IProjectConfig config1 = project.Configs[0];
            Assert.AreEqual(
                "debug",
                config1.BasePath);
            Assert.AreEqual(RuntimeType.Any, config1.RuntimeFramework.Runtime);
            Assert.AreEqual("2.0", config1.RuntimeFramework.Version.ToString(2));
            Assert.AreEqual(2, config1.Assemblies.Count);
            Assert.AreEqual(
                "assembly1.dll",
                config1.Assemblies[0]);
            Assert.AreEqual(
                "assembly2.dll",
                config1.Assemblies[1]);

            IProjectConfig config2 = project.Configs[1];
            Assert.AreEqual(2, config2.Assemblies.Count);
            Assert.AreEqual(
                "release",
                config2.BasePath);
            Assert.AreEqual(RuntimeType.Any, config2.RuntimeFramework.Runtime);
            Assert.AreEqual("4.0", config2.RuntimeFramework.Version.ToString(2));
            Assert.AreEqual(
                "assembly1.dll",
                config2.Assemblies[0]);
            Assert.AreEqual(
                "assembly2.dll",
                config2.Assemblies[1]);
        }

        [Test]
        public void CanSaveAndReloadProject()
        {
            doc.LoadXml(NUnitProjectXml.NormalProject);
            doc.Save(xmlfile);
            Assert.IsTrue(File.Exists(xmlfile));

            ProjectDocument doc2 = new ProjectDocument(xmlfile);
            doc2.Load();
            ProjectModel project2 = new ProjectModel(doc2);

            Assert.AreEqual(2, project2.Configs.Count);

            Assert.AreEqual(2, project2.Configs[0].Assemblies.Count);
            Assert.AreEqual("assembly1.dll", project2.Configs[0].Assemblies[0]);
            Assert.AreEqual("assembly2.dll", project2.Configs[0].Assemblies[1]);

            Assert.AreEqual(2, project2.Configs[1].Assemblies.Count);
            Assert.AreEqual("assembly1.dll", project2.Configs[1].Assemblies[0]);
            Assert.AreEqual("assembly2.dll", project2.Configs[1].Assemblies[1]);
        }
    }
}
