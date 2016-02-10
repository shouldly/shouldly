using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Shouldly;
using Shouldly.Configuration;

namespace TestDiffTools
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("This utility makes it easy to test different difftools");
            var diffTools = typeof(KnownDiffTools)
                .GetFields()
                .Select((f, i) => new
                {
                    DiffTool = (DiffTool)f.GetValue(ShouldlyConfiguration.DiffTools.KnownDiffTools),
                    Index = i
                }).ToList();


            while (true)
            {
                var diffToolsCollection = (List<DiffTool>)ShouldlyConfiguration.DiffTools.GetType()
                    .GetField("_diffTools", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(ShouldlyConfiguration.DiffTools);
                diffToolsCollection.Clear();

                Console.WriteLine("Select difftool:");
                foreach (var diffTool in diffTools)
                {
                    Console.WriteLine($"{diffTool.Index}. {diffTool.DiffTool.Name}");
                }

                int selectedTool;
                if (!int.TryParse(Console.ReadLine(), out selectedTool))
                {
                    Console.WriteLine("You must choose a number");
                    continue;
                }

                var selectedDiffTool = diffTools.FirstOrDefault(d => d.Index == selectedTool);
                if (selectedDiffTool == null)
                {
                    Console.WriteLine($"{selectedTool} was not a known difftool");
                    continue;
                }

                Console.WriteLine($"What do you want to test with {selectedDiffTool.DiffTool.Name}");
                Console.WriteLine();
                Console.WriteLine("1. When there is no approved file");
                Console.WriteLine("2. When the approved file does not match");

                int selectedOption;
                if (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("You must choose a number");
                    continue;
                }

                ShouldlyConfiguration.DiffTools.RegisterDiffTool(selectedDiffTool.DiffTool);
                var stacktrace = new StackTrace(true);

                var approved = Path.Combine(Path.GetDirectoryName(stacktrace.GetFrame(0).GetFileName()), "Main.approved.txt");
                switch (selectedOption)
                {
                    case 1:
                        File.Delete(approved);
                        Should.Throw<ShouldAssertException>(() => "Foo".ShouldMatchApproved());
                        break;
                    case 2:
                        File.WriteAllText(approved, "Bar");
                        Should.Throw<ShouldAssertException>(() => "Foo".ShouldMatchApproved());
                        break;
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
