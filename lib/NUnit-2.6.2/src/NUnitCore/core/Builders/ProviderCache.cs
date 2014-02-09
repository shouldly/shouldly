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
            CacheEntry entry = new CacheEntry(providerType);

            object instance = instances[entry];
            return instance == null
                ? instances[entry] = Reflect.Construct(providerType)
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

            public CacheEntry(Type providerType)
            {
                this.providerType = providerType;
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
