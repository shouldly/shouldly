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
    public class XmlHelper
    {
        #region Attributes

        public static string GetAttribute(XmlNode node, string name)
        {
            XmlAttribute attr = node.Attributes[name];
            return attr == null ? null : attr.Value;
        }

        public static T GetAttributeAsEnum<T>(XmlNode node, string name, T defaultValue)
        {
            string attrVal = XmlHelper.GetAttribute(node, name);
            if (attrVal == null)
                return defaultValue;

            if (typeof(T).IsEnum)
            {
                foreach (string s in Enum.GetNames(typeof(T)))
                    if (s.Equals(attrVal, StringComparison.OrdinalIgnoreCase))
                        return (T)Enum.Parse(typeof(T), attrVal, true);
            }

            throw new XmlException(
                string.Format("Invalid attribute value: {0}", node.Attributes[name].OuterXml));
        }

        /// <summary>
        /// Adds an attribute with a specified name and value to an existing XmlNode.
        /// </summary>
        /// <param name="node">The node to which the attribute should be added.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public static void AddAttribute(XmlNode node, string name, string value)
        {
            XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
            attr.Value = value;
            node.Attributes.Append(attr);
        }

        public static void RemoveAttribute(XmlNode node, string name)
        {
            XmlAttribute attr = node.Attributes[name];
            if (attr != null)
                node.Attributes.Remove(attr);
        }

        public static void SetAttribute(XmlNode node, string name, object value)
        {
            bool attrAdded = false;

            XmlAttribute attr = node.Attributes[name];
            if (attr == null)
            {
                attr = node.OwnerDocument.CreateAttribute(name);
                node.Attributes.Append(attr);
                attrAdded = true;
            }

            string valString = value.ToString();
            if (attrAdded || attr.Value != valString)
                attr.Value = valString;
        }
        
        #endregion

        #region Elements

        /// <summary>
        /// Adds a new element as a child of an existing XmlNode and returns it.
        /// </summary>
        /// <param name="node">The node to which the element should be added.</param>
        /// <param name="name">The element name.</param>
        /// <returns>The newly created child element</returns>
        public static XmlNode AddElement(XmlNode node, string name)
        {
            XmlNode childNode = node.OwnerDocument.CreateElement(name);
            node.AppendChild(childNode);
            return childNode;
        }

        /// <summary>
        /// Inserts a new element as a child of an existing XmlNode and returns it.
        /// </summary>
        /// <param name="node">The node to which the element should be inserted as a child.</param>
        /// <param name="name">The element name.</param>
        /// <param name="index">The index at which the element should be inserted.</param>
        /// <returns>The newly created child element</returns>
        public static XmlNode InsertElement(XmlNode node, string name, int index)
        {
            XmlNode childNode = node.OwnerDocument.CreateElement(name);
            int childCount = node.ChildNodes.Count;

            if (index < 0 || index > childCount)
                throw new ArgumentOutOfRangeException("index");

            if (index == node.ChildNodes.Count)
                node.AppendChild(childNode);
            else
                node.InsertBefore(childNode, node.ChildNodes[index]);

            return childNode;
        }
        #endregion
    }
}
