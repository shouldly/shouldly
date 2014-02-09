// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NUnit.Core
{
    public class TypeHelper
    {
        public static string GetDisplayName(Type type)
        {
#if CLR_2_0 || CLR_4_0
            if (type.IsGenericParameter)
                return type.Name;

            if (type.IsGenericType)
            {
                string name = type.FullName;
                if (name == null)
                    name = type.Name;

                int index = name.IndexOf("[[");
                if (index > 0)
                {
                    int index2 = name.LastIndexOf("]]");
                    if (index2 > index)
                        name = name.Substring(0, index) + name.Substring(index2 + 2);
                }

                index = name.LastIndexOf('.');
                if (index >= 0) name = name.Substring(index + 1);

                index = name.IndexOf('`');
                while (index >= 0)
                {
                    int index2 = name.IndexOf('+', index);
                    if (index2 >= 0)
                        name = name.Substring(0, index) + name.Substring(index2);
                    else
                        name = name.Substring(0, index);

                    index = name.IndexOf('`');
                }

                StringBuilder sb = new StringBuilder(name);

                sb.Append("<");
                int cnt = 0;
                foreach (Type t in type.GetGenericArguments())
                {
                    if (cnt++ > 0) sb.Append(",");
                    sb.Append(GetDisplayName(t));
                }
                sb.Append(">");

                return sb.ToString();
            }
#endif
            int lastdot = type.FullName.LastIndexOf('.');
            return lastdot >= 0
                ? type.FullName.Substring(lastdot + 1)
                : type.FullName;
        }

        public static string GetDisplayName(Type type, object[] arglist)
        {
            string baseName = GetDisplayName(type);
            if (arglist == null || arglist.Length == 0)
                return baseName;

            StringBuilder sb = new StringBuilder( baseName );

            sb.Append("(");
            for (int i = 0; i < arglist.Length; i++)
            {
                if (i > 0) sb.Append(",");

                object arg = arglist[i];
                string display = arg == null ? "null" : arg.ToString();

                if (arg is double || arg is float)
                {
                    if (display.IndexOf('.') == -1)
                        display += ".0";
                    display += arg is double ? "d" : "f";
                }
                else if (arg is decimal) display += "m";
                else if (arg is long) display += "L";
                else if (arg is ulong) display += "UL";
                else if (arg is string) display = "\"" + display + "\"";

                sb.Append(display);
            }
            sb.Append(")");

            return sb.ToString();
        }

        public static Type BestCommonType(Type type1, Type type2)
        {
            if (type1 == type2) return type1;
            if (type1 == null) return type2;
            if (type2 == null) return type1;

            if (TypeHelper.IsNumeric(type1) && TypeHelper.IsNumeric(type2))
            {
                if (type1 == typeof(double)) return type1;
                if (type2 == typeof(double)) return type2;

                if (type1 == typeof(float)) return type1;
                if (type2 == typeof(float)) return type2;

                if (type1 == typeof(decimal)) return type1;
                if (type2 == typeof(decimal)) return type2;

                if (type1 == typeof(UInt64)) return type1;
                if (type2 == typeof(UInt64)) return type2;

                if (type1 == typeof(Int64)) return type1;
                if (type2 == typeof(Int64)) return type2;

                if (type1 == typeof(UInt32)) return type1;
                if (type2 == typeof(UInt32)) return type2;

                if (type1 == typeof(Int32)) return type1;
                if (type2 == typeof(Int32)) return type2;

                if (type1 == typeof(UInt16)) return type1;
                if (type2 == typeof(UInt16)) return type2;

                if (type1 == typeof(Int16)) return type1;
                if (type2 == typeof(Int16)) return type2;

                if (type1 == typeof(byte)) return type1;
                if (type2 == typeof(byte)) return type2;

                if (type1 == typeof(sbyte)) return type1;
                if (type2 == typeof(sbyte)) return type2;
            }

            return type1;
        }

        public static bool IsNumeric(Type type)
        {
            return type == typeof(double) ||
                    type == typeof(float) ||
                    type == typeof(decimal) ||
                    type == typeof(Int64) ||
                    type == typeof(Int32) ||
                    type == typeof(Int16) ||
                    type == typeof(UInt64) ||
                    type == typeof(UInt32) ||
                    type == typeof(UInt16) ||
                    type == typeof(byte) ||
                    type == typeof(sbyte);
        }

#if CLR_2_0 || CLR_4_0
        public static Type MakeGenericType(Type type, Type[] typeArgs)
        {
            // TODO: Add error handling
            return type.MakeGenericType(typeArgs);
        }

        public static bool CanDeduceTypeArgsFromArgs(Type type, object[] arglist, ref Type[] typeArgsOut)
        {
            Type[] typeParameters = type.GetGenericArguments();

            foreach (ConstructorInfo ctor in type.GetConstructors())
            {
                ParameterInfo[] parameters = ctor.GetParameters();
                if (parameters.Length != arglist.Length)
                    continue;

                Type[] typeArgs = new Type[typeParameters.Length];
                for (int i = 0; i < typeArgs.Length; i++)
                {
                    for (int j = 0; j < arglist.Length; j++)
			        {
                        if (parameters[j].ParameterType.Equals(typeParameters[i]))
                            typeArgs[i] = TypeHelper.BestCommonType(
                                typeArgs[i],
                                arglist[j].GetType());
			        }

                    if (typeArgs[i] == null)
                    {
                        typeArgs = null;
                        break;
                    }
                }

                if (typeArgs != null)
                {
                    typeArgsOut = typeArgs;
                    return true;
                }
            }

            return false;
        }
#endif
    }
}
