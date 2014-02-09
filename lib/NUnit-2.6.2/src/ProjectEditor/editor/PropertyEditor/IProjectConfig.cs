// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.ProjectEditor
{
    public interface IProjectConfig
    {
        string Name { get; set; }

        string BasePath { get; set; }

        string RelativeBasePath { get; }

        string EffectiveBasePath { get; }

        string ConfigurationFile { get; set; }

        string PrivateBinPath { get; set; }

        BinPathType BinPathType { get; set; }

        AssemblyList Assemblies { get; }

        RuntimeFramework RuntimeFramework { get; set; }
    }
}
