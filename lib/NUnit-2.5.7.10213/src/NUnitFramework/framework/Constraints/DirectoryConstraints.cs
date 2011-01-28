// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.IO;

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// EmptyDirectoryConstraint is used to test that a directory is empty
    /// </summary>
    public class EmptyDirectoryContraint : Constraint
    {
        private int files = 0;
        private int subdirs = 0;

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            DirectoryInfo dirInfo = actual as DirectoryInfo;
            if (dirInfo == null)
                throw new ArgumentException("The actual value must be a DirectoryInfo", "actual");

            files = dirInfo.GetFiles().Length;
            subdirs = dirInfo.GetDirectories().Length;

            return files == 0 && subdirs == 0;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write( "An empty directory" );
        }

        /// <summary>
        /// Write the actual value for a failing constraint test to a
        /// MessageWriter. The default implementation simply writes
        /// the raw value of actual, leaving it to the writer to
        /// perform any formatting.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public override void WriteActualValueTo(MessageWriter writer)
        {
            DirectoryInfo dir = actual as DirectoryInfo;
            if (dir == null)
                base.WriteActualValueTo(writer);
            else
            {
                writer.WriteActualValue(dir);
                writer.Write(" with {0} files and {1} directories", files, subdirs);
            }
        }
    }

    /// <summary>
    /// SubDirectoryConstraint is used to test that one directory is a subdirectory of another.
    /// </summary>
    [Obsolete("Use SamePathOrUnderConstraint")]
    public class SubDirectoryConstraint : Constraint
    {
        private DirectoryInfo parentDir;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SubDirectoryConstraint"/> class.
        /// </summary>
        /// <param name="dirInfo">The dir info.</param>
        public SubDirectoryConstraint( DirectoryInfo dirInfo)
        {
            parentDir = dirInfo;
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            DirectoryInfo dirInfo = actual as DirectoryInfo;
            if (dirInfo == null)
                throw new ArgumentException("The actual value must be a DirectoryInfo", "actual");

            return IsDirectoryOnPath(parentDir, dirInfo);
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("A subdirectory of");
            writer.WriteExpectedValue(parentDir.FullName);
        }

        /// <summary>
        /// Builds a list of DirectoryInfo objects, recursing where necessary
        /// </summary>
        /// <param name="StartingDirectory">directory to recurse</param>
        /// <returns>list of DirectoryInfo objects from the top level</returns>
        private ArrayList BuildDirectoryList(DirectoryInfo StartingDirectory)
        {
            ArrayList alDirectories = new ArrayList();

            // recurse each directory
            foreach (DirectoryInfo adirectory in StartingDirectory.GetDirectories())
            {
                alDirectories.Add(adirectory);
                alDirectories.AddRange(BuildDirectoryList(adirectory));
            }

            return alDirectories;
        }

        /// <summary>
        /// private method to determine whether a directory is within the path
        /// </summary>
        /// <param name="ParentDirectory">top-level directory to search</param>
        /// <param name="SearchDirectory">directory to search for</param>
        /// <returns>true if found, false if not</returns>
        private bool IsDirectoryOnPath(DirectoryInfo ParentDirectory, DirectoryInfo SearchDirectory)
        {
            if (ParentDirectory == null)
            {
                return false;
            }

            ArrayList listDirectories = BuildDirectoryList(ParentDirectory);

            foreach (DirectoryInfo adirectory in listDirectories)
            {
                if (DirectoriesEqual(adirectory, SearchDirectory))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method to compare two DirectoryInfo objects
        /// </summary>
        /// <param name="expected">first directory to compare</param>
        /// <param name="actual">second directory to compare</param>
        /// <returns>true if equivalent, false if not</returns>
        private bool DirectoriesEqual(DirectoryInfo expected, DirectoryInfo actual)
        {
            // Do quick compares first
            if (expected.Attributes != actual.Attributes ||
                expected.CreationTime != actual.CreationTime ||
                expected.LastAccessTime != actual.LastAccessTime)
            {
                return false;
            }

            // TODO: Find a cleaner way to do this
            return new SamePathConstraint(expected.FullName).Matches(actual.FullName);
        }
    }
}
