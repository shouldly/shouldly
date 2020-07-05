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
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This utility makes it easy to test different difftools");
            var diffTools = typeof(KnownDiffTools)
                .GetFields()
                .Select((f, i) => new
                {
                    DiffTool = (DiffTool) f.GetValue(ShouldlyConfiguration.DiffTools.KnownDiffTools)!,
                    Index = i
                }).ToList();


            while (true)
            {
                var diffToolsCollection = (List<DiffTool>) ShouldlyConfiguration.DiffTools.GetType()
                    .GetField("_diffTools", BindingFlags.Instance | BindingFlags.NonPublic)!
                    .GetValue(ShouldlyConfiguration.DiffTools)!;
                diffToolsCollection.Clear();

                Console.WriteLine("Select difftool:");
                foreach (var diffTool in diffTools)
                {
                    Console.WriteLine($"{diffTool.Index}. {diffTool.DiffTool.Name}");
                }

                if (!int.TryParse(Console.ReadLine(), out var selectedTool))
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

                if (!int.TryParse(Console.ReadLine(), out var selectedOption))
                {
                    Console.WriteLine("You must choose a number");
                    continue;
                }

                ShouldlyConfiguration.DiffTools.RegisterDiffTool(selectedDiffTool.DiffTool);
                var stacktrace = new StackTrace(true);

                var currentFrame = stacktrace.GetFrame(0)!;
                var approved = Path.Combine(
                    Path.GetDirectoryName(currentFrame.GetFileName())!,
                    $"{Path.GetFileNameWithoutExtension(currentFrame.GetFileName())}.{currentFrame.GetMethod()!.Name}.approved.txt");

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
