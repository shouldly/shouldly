# Example Classes

The classes used in these samples are:

<!-- snippet: DocumentationExamples/ExampleClasses.cs -->
<a id='9418171b'></a>
```cs
using System;

namespace Simpsons
{
    public abstract class Pet
    {
        public abstract string? Name { get; set; }

        public override string? ToString()
        {
            return Name;
        }
    }

    public class Cat : Pet
    {
        public override string? Name { get; set; }
    }

    public class Dog : Pet
    {
        public override string? Name { get; set; }
    }

    public class Person
    {
        public Person()
        {
        }

        public Person(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string? Name { get; set; }
        public int Salary { get; set; }


        public override string? ToString()
        {
            return Name;
        }
    }
}
```
<sup><a href='/src/DocumentationExamples/ExampleClasses.cs#L1-L45' title='Snippet source file'>snippet source</a> | <a href='#9418171b' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
