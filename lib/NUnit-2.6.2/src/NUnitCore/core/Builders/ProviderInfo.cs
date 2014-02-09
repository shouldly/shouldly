// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Reflection;

namespace NUnit.Core.Builders
{
    class ProviderReference
    {
        private Type providerType;
        private string providerName;
        private object[] providerArgs;
        private string category;

        public ProviderReference(Type providerType, string providerName, string category)
        {
            if (providerType == null)
                throw new ArgumentNullException("providerType");
            if (providerName == null && providerType.GetInterface("System.Collections.IEnumerable") == null)
                throw new ArgumentNullException("providerName");

            this.providerType = providerType;
            this.providerName = providerName;
            this.category = category;
        }

        public ProviderReference(Type providerType, object[] args, string providerName, string category)
            : this(providerType, providerName, category)
        {
            this.providerArgs = args;
        }

        public string Name
        {
            get { return this.providerName; }
        }

        public string Category
        {
            get { return this.category; }
        }

        public IEnumerable GetInstance()
        {
            if (providerName != null)
            {
                MemberInfo[] members = providerType.GetMember(
                    providerName,
                    MemberTypes.Field | MemberTypes.Method | MemberTypes.Property,
                    BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (members.Length == 0)
                    throw new Exception(string.Format(
                        "Unable to locate {0}.{1}", providerType.FullName, providerName));

                return (IEnumerable)GetProviderObjectFromMember(members[0]);
            }
            else
                return Reflect.Construct(providerType) as IEnumerable;
        }

        private object GetProviderObjectFromMember(MemberInfo member)
        {
            object providerObject = null;
            object instance = null;

            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    PropertyInfo providerProperty = member as PropertyInfo;
                    MethodInfo getMethod = providerProperty.GetGetMethod(true);
                    if (!getMethod.IsStatic)
                        //instance = ProviderCache.GetInstanceOf(providerType);
                        instance = Reflect.Construct(providerType, providerArgs);
                    providerObject = providerProperty.GetValue(instance, null);
                    break;

                case MemberTypes.Method:
                    MethodInfo providerMethod = member as MethodInfo;
                    if (!providerMethod.IsStatic)
                        //instance = ProviderCache.GetInstanceOf(providerType);
                        instance = Reflect.Construct(providerType, providerArgs);
                    providerObject = providerMethod.Invoke(instance, null);
                    break;

                case MemberTypes.Field:
                    FieldInfo providerField = member as FieldInfo;
                    if (!providerField.IsStatic)
                        //instance = ProviderCache.GetInstanceOf(providerType);
                        instance = Reflect.Construct(providerType, providerArgs);
                    providerObject = providerField.GetValue(instance);
                    break;
            }

            return providerObject;
        }
    }
}
