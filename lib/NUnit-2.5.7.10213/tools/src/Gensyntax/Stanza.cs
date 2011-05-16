using System;
using System.IO;
using System.Collections.Generic;
using System.CodeDom.Compiler;

namespace GenSyntax
{
    class Stanza
    {
        private List<string> lines = new List<string>();
        private string typeName = "void";
        private List<string> comments = new List<string>();
        private List<GenSpec> genSpecs = new List<GenSpec>();
        private List<string> defaults = new List<string>();

        private string currentRegion;

        public static Stanza Read(TextReader rdr)
        {
            Stanza stanza = new Stanza();
            string line = rdr.ReadLine();

            while (line != null && line != "%")
            {
                if (!line.StartsWith("#"))
                    stanza.AddLine(line);

                line = rdr.ReadLine();
            }

            stanza.ProcessLines();

            return stanza;
        }

        private void AddLine(string line)
        {
            int count = lines.Count;

            if (char.IsWhiteSpace(line[0]) && count > 0)
                lines[count - 1] += line.Trim();
            else
                lines.Add(line);
        }

        private void ProcessLines()
        {
            foreach (string line in lines)
            {
                if (line.StartsWith("Type:"))
                    this.typeName = line.Substring(5).Trim();
                else if (line.StartsWith("///"))
                    this.comments.Add(line);
                else if (line.StartsWith("Gen:") || line.StartsWith("Assert:"))
                    AddSyntaxElement(line, typeName == "void");
                else if (line.StartsWith("Gen3:"))
                    AddStandardSyntaxElements(line.Substring(5).Trim());
                //else if (line.StartsWith("Assert:"))
                //    ProcessAssert(line.Substring(7).Trim());
                else if (line.StartsWith("Default:"))
                    this.defaults.Add(line.Substring(8).Trim());
                else
                    IssueFormatError(line);
            }
        }

        private void AddSyntaxElement(string line, bool isVoid)
        {
            this.genSpecs.Add(new GenSpec(line, typeName == "void"));
        }

        private void AddStandardSyntaxElements(string element)
        {
            int arrow = element.IndexOf("=>");
            if (arrow < 0) IssueFormatError(element);
            string leftside = element.Substring(0, arrow);
            string rightside = element.Substring(arrow + 2);
            
            string fullname = leftside;
            string attributes = "";
            int rbrack = leftside.LastIndexOf("]");
            if (rbrack > 0)
            {
                attributes = leftside.Substring(0, rbrack + 1);
                fullname = leftside.Substring(rbrack + 1);
            }

            string constraint = rightside;
            if (constraint.StartsWith("return "))
                constraint = constraint.Substring(7);
            if (constraint.StartsWith("new "))
                constraint = constraint.Substring(4);

            int dot = fullname.IndexOf('.');
            if (dot < 0) IssueFormatError(element);
            string name = fullname.Substring(dot + 1);

            this.typeName = constraint.Substring(0, constraint.IndexOf("("));
            this.genSpecs.Add(new GenSpec(
                "Gen: " + leftside + "=>" + rightside));
            this.genSpecs.Add(new GenSpec(
                "Gen: " + attributes + "ConstraintFactory." + name + "=>" + rightside));
            this.genSpecs.Add(new GenSpec(
                "Gen: " + attributes + "ConstraintExpression." + name + "=>(" + typeName + ")this.Append(" + rightside + ")"));
        }

        private void IssueFormatError(string line)
        {
            throw new ArgumentException("Invalid line in spec file" + Environment.NewLine + line);
        }

        public void Generate(IndentedTextWriter writer, string className, bool isStatic)
        {
            foreach (GenSpec spec in genSpecs)
            {
                if (spec.ClassName == className)
                {
                    if (currentRegion == null)
                    {
                        //currentRegion = spec.LeftPart;
                        //int dot = currentRegion.IndexOf('.');
                        //if (dot > 0) currentRegion = currentRegion.Substring(dot + 1);
                        currentRegion = spec.MethodName;
                        int lpar = currentRegion.IndexOf('(');
                        if (lpar > 0) currentRegion = currentRegion.Substring(0, lpar);

                        writer.WriteLine("#region " + currentRegion);
                        writer.WriteLine();
                    }

                    if (spec.IsGeneric)
                        writer.WriteLineNoTabs("#if NET_2_0");

                    if (spec.ClassName == "Assert")
                        GenerateAssertOverloads(writer, isStatic, spec);
                    else
                        GenerateMethod(writer, isStatic, spec);

                    if (spec.IsGeneric)
                        writer.WriteLineNoTabs("#endif");
                }
            }

            if (currentRegion != null)
            {
                writer.WriteLine("#endregion");
                writer.WriteLine();
                currentRegion = null;
            }
        }

        private void GenerateMethod(IndentedTextWriter writer, bool isStatic, GenSpec spec)
        {
            WriteComments(writer);
            WriteMethodDefinition(writer, isStatic, spec);
        }

        private void WriteMethodDefinition(IndentedTextWriter writer, bool isStatic, GenSpec spec)
        {
            if (spec.Attributes != null)
                writer.WriteLine(spec.Attributes);

            if (isStatic)
                writer.WriteLine("public static {0} {1}", typeName, spec.MethodName);
            else
                writer.WriteLine("public {0} {1}", typeName, spec.MethodName);
            writer.WriteLine("{");
            writer.Indent++;
            writer.WriteLine(spec.IsProperty
                    ? "get { return " + spec.RightPart + "; }"
                    : typeName == "void"
                        ? spec.RightPart + ";"
                        : "return " + spec.RightPart + ";" );
            writer.Indent--;
            writer.WriteLine("}");
            writer.WriteLine();
        }

        private void WriteComments(IndentedTextWriter writer)
        {
            foreach (string comment in comments)
                writer.WriteLine(comment);
        }

        private void GenerateAssertOverloads(IndentedTextWriter writer, bool isStatic, GenSpec spec)
        {
            if (!spec.LeftPart.EndsWith(")") || !spec.RightPart.EndsWith(")"))
                IssueFormatError(spec.ToString());
            string leftPart = spec.LeftPart.Substring(0, spec.LeftPart.Length - 1);
            string rightPart = spec.RightPart.Substring(0, spec.RightPart.Length - 1);

            GenSpec spec1 = new GenSpec(
                "Gen: " + leftPart + ", string message, params object[] args)=>" + rightPart + " ,message, args)");
            WriteComments(writer);
            writer.WriteLine("/// <param name=\"message\">The message to display in case of failure</param>");
            writer.WriteLine("/// <param name=\"args\">Array of objects to be used in formatting the message</param>");
            WriteMethodDefinition(writer, isStatic, spec1);

            GenSpec spec2 = new GenSpec(
                "Gen: " + leftPart + ", string message)=>" + rightPart + " ,message, null)");
            WriteComments(writer);
            writer.WriteLine("/// <param name=\"message\">The message to display in case of failure</param>");
            WriteMethodDefinition(writer, isStatic, spec2);

            GenSpec spec3 = new GenSpec(
                "Gen: " + leftPart + ")=>" + rightPart + " ,null, null)");
            WriteComments(writer);
            WriteMethodDefinition(writer, isStatic, spec3);
        }

        public List<string> Defaults
        {
            get { return defaults; }
        }
    }
}
