Example Classes
===============

The classes used in these samples are:

.. code-block:: c#

  namespace Simpsons
  {
      public abstract class Pet
      {
          public abstract string Name { get; set; }

          public override string ToString()
          {
              return Name;
          }
      }
  }

  namespace Simpsons
  {
      public class Cat : Pet
      {
          public override string Name { get; set; }
      }
  }

  namespace Simpsons
  {
      public class Dog : Pet
      {
          public override string Name { get; set; }
      }
  }

  namespace Simpsons
  {
      public class Person
      {
          public string Name { get; set; }
          public int Salary { get; set; }


          public override string ToString()
          {
              return Name;
          }
      }
  }
