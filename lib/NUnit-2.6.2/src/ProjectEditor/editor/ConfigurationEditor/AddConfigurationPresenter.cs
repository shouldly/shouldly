// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.ProjectEditor
{
    public class AddConfigurationPresenter
    {
        private IProjectModel model;
        private IAddConfigurationDialog dlg;

        public AddConfigurationPresenter(IProjectModel model, IAddConfigurationDialog dlg)
        {
            this.model = model;
            this.dlg = dlg;

            dlg.ConfigList = model.ConfigNames;

            dlg.OkButton.Execute += delegate
            {
                if (dlg.ConfigToCreate == string.Empty)
                {
                    dlg.MessageDisplay.Error("No configuration name provided");
                    return;
                }

                foreach (string config in model.ConfigNames)
                {
                    if (config == dlg.ConfigToCreate)
                    {
                        dlg.MessageDisplay.Error("A configuration with that name already exists");
                        return;
                    }
                }

                IProjectConfig newConfig = model.AddConfig(dlg.ConfigToCreate);

                if (dlg.ConfigToCopy != null)
                {
                    IProjectConfig copyConfig = model.Configs[dlg.ConfigToCopy];
                    if (copyConfig != null)
                    {
                        newConfig.BasePath = copyConfig.BasePath;
                        newConfig.BinPathType = copyConfig.BinPathType;
                        if (newConfig.BinPathType == BinPathType.Manual)
                            newConfig.PrivateBinPath = copyConfig.PrivateBinPath;
                        newConfig.ConfigurationFile = copyConfig.ConfigurationFile;
                        newConfig.RuntimeFramework = copyConfig.RuntimeFramework;

                        foreach (string assembly in copyConfig.Assemblies)
                            newConfig.Assemblies.Add(assembly);
                    }
                }

                dlg.Close();
            };
        }
    }
}
