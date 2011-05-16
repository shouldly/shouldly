using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.CodeDom.Compiler;

namespace GenSyntax
{
    /// <summary>
    /// Generates code for a single class
    /// </summary>
    class CodeGenerator
    {
        private string className;
        private string fileName;
        private StreamReader template;
        private bool isStatic;

        public CodeGenerator(string option)
        {
            int eq = option.IndexOf('=');
            if (eq > 0)
            {
                this.className = option.Substring(0, eq);
                this.fileName = option.Substring(eq + 1);
            }
            else
            {
                this.className = option;
                this.fileName = className + ".cs";
            }

            Assembly assembly = GetType().Assembly;
            Stream stream = assembly.GetManifestResourceStream("GenSyntax.Templates." + className + ".template.cs");
            if (stream == null)
                stream = assembly.GetManifestResourceStream("GenSyntax.Templates.Default.template.cs");

            this.template = new StreamReader(stream);
        }

        public void GenerateClass()
        {
            IndentedTextWriter writer = new IndentedTextWriter(new StreamWriter(this.fileName));

            Console.WriteLine("Generating " + this.fileName);

            WriteFileHeader(writer);

            foreach (Stanza stanza in SyntaxInfo.Instance)
            {
                stanza.Generate(writer, this.className, isStatic);
            }

            WriteFileTrailer(writer);

            writer.Close();
        }

        private void WriteFileHeader(IndentedTextWriter writer)
        {
            string[] argList = Environment.GetCommandLineArgs();
            argList[0] = Path.GetFileName(argList[0]);
            string commandLine = string.Join(" ", argList);

            string line = template.ReadLine();
            while (line != null && line.IndexOf("$$GENERATE$$") < 0)
            {
                line = line.Replace("__CLASSNAME__", this.className)
                           .Replace("__COMMANDLINE__", commandLine);
                writer.WriteLine(line);
                line = template.ReadLine();
            }

            if( line != null && line.IndexOf("$$STATIC$$") >= 0)
                this.isStatic = true;

            writer.Indent += 2;
        }

        private void WriteFileTrailer(IndentedTextWriter writer)
        {
            writer.Indent -= 2;

            while (!template.EndOfStream)
                writer.WriteLine(template.ReadLine());
        }
    }
}
