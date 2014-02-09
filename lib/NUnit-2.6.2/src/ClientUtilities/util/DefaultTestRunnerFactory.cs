// ****************************************************************
// Copyright 2010, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Core;

namespace NUnit.Util
{
    /// <summary>
    /// DefaultTestRunnerFactory handles creation of a suitable test 
    /// runner for a given package to be loaded and run either in a 
    /// separate process or within the same process. 
    /// </summary>
    public class DefaultTestRunnerFactory : InProcessTestRunnerFactory, ITestRunnerFactory
    {
#if CLR_2_0 || CLR_4_0
        private RuntimeFrameworkSelector selector = new RuntimeFrameworkSelector();        
        
        /// <summary>
        /// Returns a test runner based on the settings in a TestPackage.
        /// Any setting that is "consumed" by the factory is removed, so
        /// that downstream runners using the factory will not repeatedly
        /// create the same type of runner.
        /// </summary>
        /// <param name="package">The TestPackage to be loaded and run</param>
        /// <returns>A TestRunner</returns>
        public override TestRunner MakeTestRunner(TestPackage package)
        {
            ProcessModel processModel = GetTargetProcessModel(package);

            switch (processModel)
            {
                case ProcessModel.Multiple:
                    package.Settings.Remove("ProcessModel");
                    return new MultipleTestProcessRunner();
                case ProcessModel.Separate:
                    package.Settings.Remove("ProcessModel");
                    return new ProcessRunner();
                default:
                    return base.MakeTestRunner(package);
            }
        }

        public override bool CanReuse(TestRunner runner, TestPackage package)
        {
            RuntimeFramework currentFramework = RuntimeFramework.CurrentFramework;
            RuntimeFramework targetFramework = selector.SelectRuntimeFramework(package);

            ProcessModel processModel = (ProcessModel)package.GetSetting("ProcessModel", ProcessModel.Default);
            if (processModel == ProcessModel.Default)
                if (!currentFramework.Supports(targetFramework))
                    processModel = ProcessModel.Separate;

            switch (processModel)
            {
                case ProcessModel.Multiple:
                    return runner is MultipleTestProcessRunner;
                case ProcessModel.Separate:
                    ProcessRunner processRunner = runner as ProcessRunner;
                    return processRunner != null && processRunner.RuntimeFramework == targetFramework;
                default:
                    return base.CanReuse(runner, package);
            }
        }

        private ProcessModel GetTargetProcessModel(TestPackage package)
        {
            RuntimeFramework currentFramework = RuntimeFramework.CurrentFramework;
            RuntimeFramework targetFramework = selector.SelectRuntimeFramework(package);

            ProcessModel processModel = (ProcessModel)package.GetSetting("ProcessModel", ProcessModel.Default);
            if (processModel == ProcessModel.Default)
                if (!currentFramework.Supports(targetFramework))
                    processModel = ProcessModel.Separate;
            return processModel;
        }
#endif
    }
}
