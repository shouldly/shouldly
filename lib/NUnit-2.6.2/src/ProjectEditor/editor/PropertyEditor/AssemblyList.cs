// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Xml;

namespace NUnit.ProjectEditor
{
	/// <summary>
	/// Represents a list of assemblies. It stores paths 
	/// that are added and fires an event whenevever it
	/// changes. All paths should be added as absolute paths.
	/// </summary>
	public class AssemblyList
	{
        private XmlNode configNode;

        public AssemblyList(XmlNode configNode)
        {
            this.configNode = configNode;
        }

        #region Properties

        public string this[int index]
        {
            get { return XmlHelper.GetAttribute(AssemblyNodes[index], "path"); }
            set { XmlHelper.SetAttribute(AssemblyNodes[index], "path", value); }
        }

        public int Count
        {
            get { return AssemblyNodes.Count; }
        }

        #endregion

        #region Methods

        public void Add(string assemblyPath)
        {
            XmlHelper.AddAttribute(
                XmlHelper.AddElement(configNode, "assembly"),
                "path",
                assemblyPath);
        }

        public void Insert(int index, string assemblyPath)
        {
            XmlHelper.AddAttribute(
                XmlHelper.InsertElement(configNode, "assembly", index),
                "path",
                assemblyPath);
        }

        public void Remove(string assemblyPath)
        {
            foreach (XmlNode node in configNode.SelectNodes("assembly"))
            {
                string path = XmlHelper.GetAttribute(node, "path");
                if (path == assemblyPath)
                {
                    configNode.RemoveChild(node);
                    break;
                }
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (XmlNode node in AssemblyNodes)
                yield return XmlHelper.GetAttribute(node, "path");
        }

        #endregion

        #region private Properties

        private XmlNodeList AssemblyNodes
        {
            get { return configNode.SelectNodes("assembly"); }
        }

        private XmlNode GetAssemblyNodes(int index)
        {
            return AssemblyNodes[index];
        }

        #endregion
    }
}
