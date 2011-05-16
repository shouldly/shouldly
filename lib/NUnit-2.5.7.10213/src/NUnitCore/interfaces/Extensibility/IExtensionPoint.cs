// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;

namespace NUnit.Core.Extensibility
{
    /// <summary>
    /// Represents a single point of extension for NUnit. Some extension
    /// points may accept only a single extension, while others may
    /// accept more than one at the same time.
    /// </summary>
    public interface IExtensionPoint
	{
		/// <summary>
		/// Get the name of this extension point
		/// </summary>
		string Name { get; }

        /// <summary>
        /// Get the host that provides this extension point
        /// </summary>
        IExtensionHost Host { get; }

        /// <summary>
        /// Install an extension at this extension point. If the
        /// extension object does not meet the requirements for
        /// this extension point, an exception is thrown.
        /// </summary>
        /// <param name="extension">The extension to install</param>
        void Install(object extension);

        /// <summary>
		/// Removes an extension from this extension point. If the
		/// extension object is not present, the method returns
		/// without error.
        /// </summary>
        /// <param name="extension"></param>
		void Remove( object extension );
	}

    /// <summary>
    /// Represents a single point of extension for NUnit. Some extension
    /// points may accept only a single extension, while others may
    /// accept more than one at the same time. This interface enhances
    /// IExtensionPoint by allowing specification of a priority
    /// order for applying addins.
    /// </summary>
    public interface IExtensionPoint2 : IExtensionPoint
    {
        /// <summary>
        /// Install an extension at this extension point specifying
        /// an integer priority value for the extension.If the
        /// extension object does not meet the requirements for
        /// this extension point, or if the extension point does
        /// not support the requested priority level, an exception 
        /// is thrown.
        /// </summary>
        /// <param name="extension">The extension to install</param>
        /// <param name="priority">The priority level for this extension</param>
        void Install(object extension, int priority);
    }
}
