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
    /// InProcessTestRunnerFactory handles creation of a suitable test 
    /// runner for a given package to be loaded and run within the
    /// same process.
    /// </summary>
    public class InProcessTestRunnerFactory : ITestRunnerFactory
    {
        #region ITestRunnerFactory Members

        /// <summary>
        /// Returns a test runner based on the settings in a TestPackage.
        /// Any setting that is "consumed" by the factory is removed, so
        /// that downstream runners using the factory will not repeatedly
        /// create the same type of runner.
        /// </summary>
        /// <param name="package">The TestPackage to be loaded and run</param>
        /// <returns>A TestRunner</returns>
        public virtual TestRunner MakeTestRunner(TestPackage package)
        {
            DomainUsage domainUsage = 
                (DomainUsage)package.GetSetting("DomainUsage", DomainUsage.Default);

            switch (domainUsage)
            {
                case DomainUsage.Multiple:
                    package.Settings.Remove("DomainUsage");
                    return new MultipleTestDomainRunner();
                case DomainUsage.None:
                    return new RemoteTestRunner();
                case DomainUsage.Single:
                default:
                    return new TestDomain();
            }
        }

        public virtual bool CanReuse(TestRunner runner, TestPackage package)
        {
            return false;
        }

        #endregion
    }
}
