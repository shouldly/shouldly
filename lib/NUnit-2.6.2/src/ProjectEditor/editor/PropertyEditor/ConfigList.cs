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
	/// Summary description for ConfigList.
	/// </summary>
	public class ConfigList : IEnumerable<IProjectConfig>
	{
        private IProjectModel project;
        private XmlNode projectNode;

        public ConfigList(IProjectModel project)
        {
            this.project = project;
            this.projectNode = project.Document.RootNode;
        }

		#region Properties

        public int Count
        {
            get { return ConfigNodes.Count; }
        }

		public IProjectConfig this[int index]
		{
            get { return new ProjectConfig(project, ConfigNodes[index]); }
		}

        public IProjectConfig this[string name]
        {
            get
            {
                int index = IndexOf(name);
                return index >= 0 ? this[index] : null;
            }
        }

        private XmlNodeList ConfigNodes
        {
            get { return projectNode.SelectNodes("Config"); }
        }

        private XmlNode SettingsNode
        {
            get { return projectNode.SelectSingleNode("Settings"); }
        }

		#endregion

		#region Methods

        //public IProjectConfig Add(string name)
        //{
        //    XmlNode configNode = XmlHelper.AddElement(projectNode, "Config");
        //    XmlHelper.AddAttribute(configNode, "name", name);

        //    return new ProjectConfig(project, configNode);
        //}

        //public void RemoveAt(int index)
        //{
        //    bool wasActive = project.ActiveConfigName == this[index].Name;
        //    projectNode.RemoveChild(ConfigNodes[index]);
        //    if (wasActive)
        //        project.ActiveConfigName = null;
        //}

        //public void Remove(string name)
        //{
        //    int index = IndexOf(name);
        //    if (index >= 0)
        //    {
        //        RemoveAt(index);
        //    }
        //}

        private int IndexOf(string name)
        {
            for (int index = 0; index < ConfigNodes.Count; index++)
            {
                if (XmlHelper.GetAttribute(ConfigNodes[index], "name") == name)
                    return index;
            }

            return -1;
        }

        //public bool Contains(string name)
        //{
        //    return IndexOf(name) >= 0;
        //}

		#endregion

        #region IEnumerable<IProjectConfig> Members

        public IEnumerator<IProjectConfig> GetEnumerator()
        {
            foreach (XmlNode node in ConfigNodes)
                yield return new ProjectConfig(project, node);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
