// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Reflection;
using NUnit.Core.Extensibility;
#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif

namespace NUnit.Core.Builders
{
    public class CombinatorialStrategy : CombiningStrategy
    {
        public CombinatorialStrategy(IEnumerable[] sources) : base(sources) { }

        public override IEnumerable GetTestCases()
        {
            IEnumerator[] enumerators = new IEnumerator[Sources.Length];
            int index = -1;

#if CLR_2_0 || CLR_4_0
            List<ParameterSet> testCases = new List<ParameterSet>();
#else
			ArrayList testCases = new ArrayList();
#endif

            for (; ; )
            {
                while (++index < Sources.Length)
                {
                    enumerators[index] = Sources[index].GetEnumerator();
                    if (!enumerators[index].MoveNext())
						return testCases;
                }

                object[] testdata = new object[Sources.Length];

                for (int i = 0; i < Sources.Length; i++)
                    testdata[i] = enumerators[i].Current;

                ParameterSet testCase = new ParameterSet();
                testCase.Arguments = testdata;
				testCases.Add(testCase);

                index = Sources.Length;

                while (--index >= 0 && !enumerators[index].MoveNext()) ;

                if (index < 0) break;
            }

			return testCases;
        }
    }
}
