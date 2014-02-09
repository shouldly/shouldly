using System;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Xml.Serialization;

using PNUnit.Framework;

namespace PNUnit.Launcher
{
    [Serializable]
    public class TestGroup
    {
        public Variable[] Variables;
        public ParallelTest[] ParallelTests;
    }

    [Serializable]
    public class ParallelTest
    {
        public string Name;
        public string[] Agents;
        public TestConf[] Tests;
    }


    [Serializable]
    public class TestConf
    {
        public string Name;
        public string Assembly;
        public string TestToRun;
        public string Machine;
        public string[] TestParams;
        public string StartBarrier = Names.ServerBarrier;
        public string EndBarrier = Names.EndBarrier;
        public string[] WaitBarriers;
    }

    public class Variable
    {
        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public string value;

        public override string ToString()
        {
            return string.Format("[{0}]=[{1}]", this.name, this.value);
        }
    }

    public class TestConfLoader
    {
        public static TestGroup LoadFromFile(string file, string[] args)
        {
            FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read);
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(TestGroup));
                
                TestGroup result = (TestGroup)ser.Deserialize(reader);

                Variable[] processedVars = ParseVariablesFromCommandLine(args, result.Variables);
                
                ReplaceVariables(result.ParallelTests, processedVars);

                return result;
            }
            finally
            {
                reader.Close();
            }
        }

        public static void WriteToFile(TestGroup group, string file)
        {
            FileStream writer = new FileStream(file, FileMode.Create, FileAccess.Write);
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(TestGroup));
                ser.Serialize(writer, group);
            }
            finally
            {
                writer.Close();
            }
        }

        // Variable replacement

        private static void ReplaceVariables(ParallelTest[] target, Variable[] variables)
        {
            if (variables == null || target == null) return;

            foreach (Variable var in variables)
                foreach (ParallelTest test in target) 
                    ReplaceVariableOnTest(test, var);
        }

        private static void ReplaceVariableOnTest(ParallelTest t, Variable var)
        {
            t.Agents = ReplaceVarArray(t.Agents, var);
            t.Name = ReplaceVar (t.Name, var);

            foreach (TestConf tc in t.Tests)
            {
                tc.Name = ReplaceVar(tc.Name, var);
                tc.Assembly = ReplaceVar(tc.Assembly, var);
                tc.TestToRun = ReplaceVar(tc.TestToRun, var);
                tc.Machine = ReplaceVar(tc.Machine, var);
                tc.TestParams = ReplaceVarArray(tc.TestParams, var);
                tc.StartBarrier = ReplaceVar(tc.StartBarrier, var);
                tc.EndBarrier = ReplaceVar(tc.EndBarrier, var);
                tc.WaitBarriers = ReplaceVarArray(tc.WaitBarriers, var);
            }
        }

        private static string[] ReplaceVarArray(string[] source, Variable var)
        {
            if (source == null) return null;

            for (int i = 0; i < source.Length; ++i)
            {
                source[i] = ReplaceVar(source[i], var);
            }

            return source;
        }

        private static string ReplaceVar(string source, Variable var)
        {
            if (source == null) return null;

            return source.Replace(var.name, var.value);
        }

        // Command line variable handling

        private static Variable[] ParseVariablesFromCommandLine(
            string[] args, Variable[] variables)
        {
            // variables from the command line, if defined, are prefixed with "-D:"

            if (args == null) return variables;

            IDictionary varsFromCli = ParseCliVars(args);

            if (varsFromCli == null) return variables;

            return MergeVars(varsFromCli, variables);
        }

        private static IDictionary ParseCliVars(string[] args)
        {
            Hashtable result = new Hashtable();

            foreach (string s in args)
            {
                if (s.StartsWith("-D:") || s.StartsWith("-d:"))
                {
                    Variable var = new Variable();

                    string[] v = s.Substring(3).Split('=');

                    if (v.Length >= 1)
                    {
                        var.name = v[0];

                        if (v.Length == 2) var.value = v[1];

                        result[var.name] = var;
                    }
                }
            }

            return (result.Count > 0) ? result: null;
        }

        private static Variable[] MergeVars(IDictionary overrides, IList originals)
        {
            ArrayList result = new ArrayList();

            if( originals != null )
            {
                foreach( Variable v in originals )
                {
                    Variable ovr = overrides[v.name] as Variable;

                    if( ovr != null )
                    {
                        v.value = ovr.value;
                        overrides.Remove(v.name);
                    }

                    result.Add(v);
                }
            }

            foreach( Variable v in overrides.Values ) result.Add(v);

            return result.ToArray(typeof(Variable)) as Variable[];
        }
    }

}
