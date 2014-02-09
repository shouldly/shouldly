// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Reflection;
using System.Collections;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Builders
{
    /// <summary>
    /// Provides data from fields marked with the DatapointAttribute or the
    /// DatapointsAttribute.
    /// </summary>
    public class DatapointProvider : IDataPointProvider
    {
        private static readonly string DatapointAttribute = "NUnit.Framework.DatapointAttribute";
        private static readonly string DatapointsAttribute = "NUnit.Framework.DatapointsAttribute";

        #region IDataPointProvider Members

        public bool HasDataFor(System.Reflection.ParameterInfo parameter)
        {
            Type parameterType = parameter.ParameterType;
            MemberInfo method = parameter.Member;
            Type fixtureType = method.ReflectedType;

            if (!Reflect.HasAttribute(method, NUnitFramework.TheoryAttribute, true))
                return false;

            if (parameterType == typeof(bool) || parameterType.IsEnum)
                return true;

            foreach (MemberInfo member in fixtureType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            {
                if (Reflect.HasAttribute(member, DatapointAttribute, true) &&
                    GetTypeFromMemberInfo(member) == parameterType)
                        return true;
                else if (Reflect.HasAttribute(member, DatapointsAttribute, true) &&
                    GetElementTypeFromMemberInfo(member) == parameterType)
                        return true;
            }

            return false;
        }

        public System.Collections.IEnumerable GetDataFor(System.Reflection.ParameterInfo parameter)
        {
            ArrayList datapoints = new ArrayList();

            Type parameterType = parameter.ParameterType;
            Type fixtureType = parameter.Member.ReflectedType;

            foreach (MemberInfo member in fixtureType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            {
                if (Reflect.HasAttribute(member, DatapointAttribute, true))
                {
                    if (GetTypeFromMemberInfo(member) == parameterType &&
                        member.MemberType == MemberTypes.Field)
                    {
                        FieldInfo field = member as FieldInfo;
                        if (field.IsStatic)
                            datapoints.Add(field.GetValue(null));
                        else
                            datapoints.Add(field.GetValue(ProviderCache.GetInstanceOf(fixtureType)));
                    }
                }
                else if (Reflect.HasAttribute(member, DatapointsAttribute, true))
                {
                    if (GetElementTypeFromMemberInfo(member) == parameterType)
                    {
                        object instance;

                        switch(member.MemberType)
                        {
                            case MemberTypes.Field:
                                FieldInfo field = member as FieldInfo;
                                instance = field.IsStatic ? null : ProviderCache.GetInstanceOf(fixtureType);
                                foreach (object data in (IEnumerable)field.GetValue(instance))
                                    datapoints.Add(data);
                                break;
                            case MemberTypes.Property:
                                PropertyInfo property = member as PropertyInfo;
                                MethodInfo getMethod = property.GetGetMethod(true);
                                instance = getMethod.IsStatic ? null : ProviderCache.GetInstanceOf(fixtureType);
                                foreach (object data in (IEnumerable)property.GetValue(instance, null))
                                    datapoints.Add(data);
                                break;
                            case MemberTypes.Method:
                                MethodInfo method = member as MethodInfo;
                                instance = method.IsStatic ? null : ProviderCache.GetInstanceOf(fixtureType);
                                foreach (object data in (IEnumerable)method.Invoke(instance, Type.EmptyTypes))
                                    datapoints.Add(data);
                                break;
                        }
                    }
                }
            }

            if (datapoints.Count == 0)
            {
                if (parameterType == typeof(bool))
                {
                    datapoints.Add(true);
                    datapoints.Add(false);
                }
                else if (parameterType.IsEnum)
                {
                    datapoints.AddRange(System.Enum.GetValues(parameterType));
                }
            }

            return datapoints;
        }

        private Type GetTypeFromMemberInfo(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                default:
                    return null;
            }
        }

        private Type GetElementTypeFromMemberInfo(MemberInfo member)
        {
            Type type = GetTypeFromMemberInfo(member);

            if (type == null)
                return null;

            if (type.IsArray)
                return type.GetElementType();

#if CLR_2_0 || CLR_4_0
            if (type.IsGenericType && type.Name == "IEnumerable`1")
                return type.GetGenericArguments()[0];
#endif

            return null;
        }

        #endregion
    }
}
