// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;

namespace NUnit.ProjectEditor
{
    public class ConfigurationEditor
    {
        #region Instance Variables

        private IProjectModel model;
        private IConfigurationEditorDialog view;

        #endregion

        #region Constructor

        public ConfigurationEditor(IProjectModel model, IConfigurationEditorDialog view)
        {
            this.model = model;
            this.view = view;

            UpdateConfigList();

            view.AddCommand.Execute += AddConfig;
            view.RemoveCommand.Execute += RemoveConfig;
            view.RenameCommand.Execute += RenameConfig;
            view.ActiveCommand.Execute += MakeActive;

            view.ConfigList.SelectionChanged += SelectedConfigChanged;
        }

        #endregion

        #region Command Event Handlers

        public void AddConfig()
        {
            IAddConfigurationDialog dlg = view.AddConfigurationDialog;
            new AddConfigurationPresenter(model, dlg);

            dlg.ShowDialog();

            UpdateConfigList();
        }

        public void RenameConfig()
        {
            string oldName = view.ConfigList.SelectedItem;
            if (oldName.EndsWith(" (active)"))
                oldName = oldName.Substring(0, oldName.Length - 9);

            IRenameConfigurationDialog dlg = view.RenameConfigurationDialog;
            new RenameConfigurationPresenter(model, dlg, oldName);

            dlg.ShowDialog();

            UpdateConfigList();
        }

        public void RemoveConfig()
        {
            model.RemoveConfigAt(view.ConfigList.SelectedIndex);

            UpdateConfigList();
        }

        public void MakeActive()
        {
            model.ActiveConfigName = view.ConfigList.SelectedItem;

            UpdateConfigList();
        }

        public void SelectedConfigChanged()
        {
            int index = view.ConfigList.SelectedIndex;

            view.AddCommand.Enabled = true;
            view.ActiveCommand.Enabled = index >= 0 && model.Configs[index].Name != model.ActiveConfigName;
            view.RenameCommand.Enabled = index >= 0;
            view.RemoveCommand.Enabled = index >= 0;
        }

        #endregion

        #region Helper Methods

        private void UpdateConfigList()
        {
            string selectedConfig = view.ConfigList.SelectedItem;
            if (selectedConfig != null && selectedConfig.EndsWith(" (active)"))
                selectedConfig = selectedConfig.Substring(0, selectedConfig.Length - 9);
            int selectedIndex = -1;
            int activeIndex = -1;

            int count = model.Configs.Count;
            string[] configList = new string[count];

            for (int index = 0; index < count; index++)
            {
                string config = model.Configs[index].Name;

                if (config == model.ActiveConfigName)
                    activeIndex = index;
                if (config == selectedConfig)
                    selectedIndex = index;

                configList[index] = config;
            }

            if (activeIndex >= 0)
                configList[activeIndex] += " (active)";

            view.ConfigList.SelectionList = configList;

            view.ConfigList.SelectedIndex = selectedIndex > 0
                ? selectedIndex
                : configList.Length > 0
                    ? 0 : -1;

            SelectedConfigChanged();
        }

        #endregion
    }
}
