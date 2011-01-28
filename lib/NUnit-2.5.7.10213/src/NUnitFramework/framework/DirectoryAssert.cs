// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.IO;
using System.ComponentModel;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
    /// <summary>
    /// Summary description for DirectoryAssert
    /// </summary>
    [Obsolete("Use Assert with constraint-based syntax")]
    public class DirectoryAssert
    {
        #region Equals and ReferenceEquals

        /// <summary>
        /// The Equals method throws an AssertionException. This is done 
        /// to make sure there is no mistake by calling this function.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new bool Equals(object a, object b)
        {
            throw new AssertionException("Assert.Equals should not be used for Assertions");
        }

        /// <summary>
        /// override the default ReferenceEquals to throw an AssertionException. This 
        /// implementation makes sure there is no mistake in calling this function 
        /// as part of Assert. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static new void ReferenceEquals(object a, object b)
        {
            throw new AssertionException("Assert.ReferenceEquals should not be used for Assertions");
        }

        #endregion

        #region Constructor

        /// <summary>
        /// We don't actually want any instances of this object, but some people
        /// like to inherit from it to add other static methods. Hence, the
        /// protected constructor disallows any instances of this object. 
        /// </summary>
        protected DirectoryAssert() { }

        #endregion

        #region AreEqual

        /// <summary>
        /// Verifies that two directories are equal.  Two directories are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory containing the value that is expected</param>
        /// <param name="actual">A directory containing the actual value</param>
        /// <param name="message">The message to display if directories are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void AreEqual(DirectoryInfo expected, DirectoryInfo actual, string message, params object[] args)
        {
            Assert.That(actual, new EqualConstraint(expected), message, args);
        }

        /// <summary>
        /// Verifies that two directories are equal.  Two directories are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory containing the value that is expected</param>
        /// <param name="actual">A directory containing the actual value</param>
        /// <param name="message">The message to display if directories are not equal</param>
        static public void AreEqual(DirectoryInfo expected, DirectoryInfo actual, string message)
        {
            AreEqual(actual, expected, message, null);
        }

        /// <summary>
        /// Verifies that two directories are equal.  Two directories are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory containing the value that is expected</param>
        /// <param name="actual">A directory containing the actual value</param>
        static public void AreEqual(DirectoryInfo expected, DirectoryInfo actual)
        {
            AreEqual(actual, expected, string.Empty, null);
        }

        /// <summary>
        /// Verifies that two directories are equal.  Two directories are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory path string containing the value that is expected</param>
        /// <param name="actual">A directory path string containing the actual value</param>
        /// <param name="message">The message to display if directories are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void AreEqual(string expected, string actual, string message, params object[] args)
        {
            // create a directory info object for the expected path
            DirectoryInfo diExpected = new DirectoryInfo(expected);

            // create a directory info object for the actual path
            DirectoryInfo diActual = new DirectoryInfo(actual);

            AreEqual(diExpected, diActual, message, args);
        }

        /// <summary>
        /// Verifies that two directories are equal.  Two directories are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory path string containing the value that is expected</param>
        /// <param name="actual">A directory path string containing the actual value</param>
        /// <param name="message">The message to display if directories are not equal</param>
        static public void AreEqual(string expected, string actual, string message)
        {
            AreEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Verifies that two directories are equal.  Two directories are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory path string containing the value that is expected</param>
        /// <param name="actual">A directory path string containing the actual value</param>
        static public void AreEqual(string expected, string actual)
        {
            AreEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region AreNotEqual

        /// <summary>
        /// Asserts that two directories are not equal. If they are equal
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory containing the value that is expected</param>
        /// <param name="actual">A directory containing the actual value</param>
        /// <param name="message">The message to display if directories are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void AreNotEqual(DirectoryInfo expected, DirectoryInfo actual, string message, params object[] args)
        {
            Assert.That(actual, new NotConstraint(new EqualConstraint(expected)), message, args);
        }

        /// <summary>
        /// Asserts that two directories are not equal. If they are equal
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory containing the value that is expected</param>
        /// <param name="actual">A directory containing the actual value</param>
        /// <param name="message">The message to display if directories are not equal</param>
        static public void AreNotEqual(DirectoryInfo expected, DirectoryInfo actual, string message)
        {
            AreNotEqual(actual, expected, message, null);
        }

        /// <summary>
        /// Asserts that two directories are not equal. If they are equal
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory containing the value that is expected</param>
        /// <param name="actual">A directory containing the actual value</param>
        static public void AreNotEqual(DirectoryInfo expected, DirectoryInfo actual)
        {
            AreNotEqual(actual, expected, string.Empty, null);
        }

        /// <summary>
        /// Asserts that two directories are not equal. If they are equal
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory path string containing the value that is expected</param>
        /// <param name="actual">A directory path string containing the actual value</param>
        /// <param name="message">The message to display if directories are equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void AreNotEqual(string expected, string actual, string message, params object[] args)
        {
            // create a directory info object for the expected path
            DirectoryInfo diExpected = new DirectoryInfo(expected);

            // create a directory info object for the actual path
            DirectoryInfo diActual = new DirectoryInfo(actual);

            AreNotEqual(diExpected, diActual, message, args);
        }

        /// <summary>
        /// Asserts that two directories are not equal. If they are equal
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory path string containing the value that is expected</param>
        /// <param name="actual">A directory path string containing the actual value</param>
        /// <param name="message">The message to display if directories are equal</param>
        static public void AreNotEqual(string expected, string actual, string message)
        {
            AreNotEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Asserts that two directories are not equal. If they are equal
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">A directory path string containing the value that is expected</param>
        /// <param name="actual">A directory path string containing the actual value</param>
        static public void AreNotEqual(string expected, string actual)
        {
            AreNotEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// Asserts that the directory is empty. If it is not empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="message">The message to display if directories are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void IsEmpty(DirectoryInfo directory, string message, params object[] args)
        {
            Assert.That( directory, new EmptyDirectoryContraint(), message, args);
        }

        /// <summary>
        /// Asserts that the directory is empty. If it is not empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="message">The message to display if directories are not equal</param>
        static public void IsEmpty(DirectoryInfo directory, string message)
        {
            IsEmpty(directory, message, null);
        }

        /// <summary>
        /// Asserts that the directory is empty. If it is not empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        static public void IsEmpty(DirectoryInfo directory)
        {
            IsEmpty(directory, string.Empty, null);
        }

        /// <summary>
        /// Asserts that the directory is empty. If it is not empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="message">The message to display if directories are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void IsEmpty(string directory, string message, params object[] args)
        {
            IsEmpty(new DirectoryInfo(directory), message, args);
        }

        /// <summary>
        /// Asserts that the directory is empty. If it is not empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="message">The message to display if directories are not equal</param>
        static public void IsEmpty(string directory, string message)
        {
            IsEmpty(directory, message, null);
        }

        /// <summary>
        /// Asserts that the directory is empty. If it is not empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        static public void IsEmpty(string directory)
        {
            IsEmpty(directory, string.Empty, null);
        }

        #endregion

        #region IsNotEmpty

        /// <summary>
        /// Asserts that the directory is not empty. If it is empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="message">The message to display if directories are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void IsNotEmpty(DirectoryInfo directory, string message, params object[] args)
        {
            Assert.That( directory, new NotConstraint(new EmptyDirectoryContraint()), message, args);
        }

        /// <summary>
        /// Asserts that the directory is not empty. If it is empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="message">The message to display if directories are not equal</param>
        static public void IsNotEmpty(DirectoryInfo directory, string message)
        {
            IsNotEmpty(directory, message, null);
        }

        /// <summary>
        /// Asserts that the directory is not empty. If it is empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        static public void IsNotEmpty(DirectoryInfo directory)
        {
            IsNotEmpty(directory, string.Empty, null);
        }

        /// <summary>
        /// Asserts that the directory is not empty. If it is empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="message">The message to display if directories are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void IsNotEmpty(string directory, string message, params object[] args)
        {
            DirectoryInfo diActual = new DirectoryInfo(directory);
            IsNotEmpty(diActual, message, args);
        }

        /// <summary>
        /// Asserts that the directory is not empty. If it is empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="message">The message to display if directories are not equal</param>
        static public void IsNotEmpty(string directory, string message)
        {
            IsNotEmpty(directory, message, null);
        }

        /// <summary>
        /// Asserts that the directory is not empty. If it is empty
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        static public void IsNotEmpty(string directory)
        {
            IsNotEmpty(directory, string.Empty, null);
        }

        #endregion

        #region IsWithin

        /// <summary>
        /// Asserts that path contains actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        /// <param name="message">The message to display if directory is not within the path</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void IsWithin(DirectoryInfo directory, DirectoryInfo actual, string message, params object[] args)
        {
            Assert.That(actual, new SubDirectoryConstraint(directory), message, args);
        }

        /// <summary>
        /// Asserts that path contains actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        /// <param name="message">The message to display if directory is not within the path</param>
        static public void IsWithin(DirectoryInfo directory, DirectoryInfo actual, string message)
        {
            IsWithin(directory, actual, message, null);
        }

        /// <summary>
        /// Asserts that path contains actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        static public void IsWithin(DirectoryInfo directory, DirectoryInfo actual)
        {
            IsWithin(directory, actual, string.Empty, null);
        }

        /// <summary>
        /// Asserts that path contains actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        /// <param name="message">The message to display if directory is not within the path</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void IsWithin(string directory, string actual, string message, params object[] args)
        {
            // create a directory info object for the expected path
            DirectoryInfo diExpected = new DirectoryInfo(directory);

            // create a directory info object for the actual path
            DirectoryInfo diActual = new DirectoryInfo(actual);

            IsWithin(diExpected, diActual, message, args);
        }

        /// <summary>
        /// Asserts that path contains actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        /// <param name="message">The message to display if directory is not within the path</param>
        static public void IsWithin(string directory, string actual, string message)
        {
            IsWithin(directory, actual, message, null);
        }

        /// <summary>
        /// Asserts that path contains actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        static public void IsWithin(string directory, string actual)
        {
            IsWithin(directory, actual, string.Empty, null);
        }

        #endregion

        #region IsNotWithin

        /// <summary>
        /// Asserts that path does not contain actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        /// <param name="message">The message to display if directory is not within the path</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void IsNotWithin(DirectoryInfo directory, DirectoryInfo actual, string message, params object[] args)
        {
            Assert.That(actual, new NotConstraint( new SubDirectoryConstraint(directory) ), message, args );
        }

        /// <summary>
        /// Asserts that path does not contain actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        /// <param name="message">The message to display if directory is not within the path</param>
        static public void IsNotWithin(DirectoryInfo directory, DirectoryInfo actual, string message)
        {
            IsNotWithin(directory, actual, message, null);
        }

        /// <summary>
        /// Asserts that path does not contain actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        static public void IsNotWithin(DirectoryInfo directory, DirectoryInfo actual)
        {
            IsNotWithin(directory, actual, string.Empty, null);
        }

        /// <summary>
        /// Asserts that path does not contain actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        /// <param name="message">The message to display if directory is not within the path</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void IsNotWithin(string directory, string actual, string message, params object[] args)
        {
            // create a directory info object for the expected path
            DirectoryInfo diExpected = new DirectoryInfo(directory);

            // create a directory info object for the actual path
            DirectoryInfo diActual = new DirectoryInfo(actual);

            IsNotWithin(diExpected, diActual, message, args);
        }

        /// <summary>
        /// Asserts that path does not contain actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        /// <param name="message">The message to display if directory is not within the path</param>
        static public void IsNotWithin(string directory, string actual, string message)
        {
            IsNotWithin(directory, actual, message, null);
        }

        /// <summary>
        /// Asserts that path does not contain actual as a subdirectory or
        /// an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="directory">A directory to search</param>
        /// <param name="actual">sub-directory asserted to exist under directory</param>
        static public void IsNotWithin(string directory, string actual)
        {
            IsNotWithin(directory, actual, string.Empty, null);
        }

        #endregion
    }
}