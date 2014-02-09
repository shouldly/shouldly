// ****************************************************************
// Copyright 2010, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Core;

namespace NUnit.Gui.SettingsPages
{
    public partial class RuntimeSelectionSettingsPage : NUnit.UiKit.SettingsPage
    {
        private static readonly string RUNTIME_SELECTION_ENABLED =
            "Options.TestLoader.RuntimeSelectionEnabled";

        public RuntimeSelectionSettingsPage(string key) : base(key)
        {
            InitializeComponent();
        }

        public override void LoadSettings()
        {
            runtimeSelectionCheckBox.Checked = settings.GetSetting(RUNTIME_SELECTION_ENABLED, true);
        }

        public override void ApplySettings()
        {
            settings.SaveSetting(RUNTIME_SELECTION_ENABLED, runtimeSelectionCheckBox.Checked);
        }
    }
}
