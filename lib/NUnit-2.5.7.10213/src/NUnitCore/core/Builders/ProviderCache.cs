// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Text;

namespace NUnit.Core.Builders
{
    class ProviderCache
    {
        private static IDictionary instances = new Hashtable();

        public static object GetInstanceOf(Type providerType)
        {
            return GetInstanceOf(providerType, null);
        }

        public static object GetInstanceOf(Type providerType, object[] providerArgs)
        {
            CacheEntry entry = new CacheEntry(providerType, providerArgs);

            object instance = instances[entry];
            return instance == null
                ? instances[entry] = Reflect.Construct(providerType, providerArgs)
                : instance;
        }

        public static void Clear()
        {
            foreach (object key in instances.Keys)
            {
                IDisposable provider = instances[key] as IDisposable;
                if (provider != null)
                    provider.Dispose();
            }

            instances.Clear();
        }

        class CacheEntry
        {
            private Type providerType;
            private object[] providerArgs;

            public CacheEntry(Type providerType, object[] providerArgs)
            {
                this.providerType = providerType;
                this.providerArgs = providerArgs;
            }

            public override bool Equals(object obj)
            {
                CacheEntry other = obj as CacheEntry;
                if (other == null) return false;

                return this.providerType == other.providerType;
            }

            public override int GetHashCode()
            {
                return providerType.GetHashCode();
            }
        }
    }
}
