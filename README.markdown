Shouldly
========

### How asserting *Should* be

This is the old *Assert* way: 

    Assert.That(contestant.Points, Is.EqualTo(1337));

For your troubles, you get this message, when it fails:

    Expected 1337 but was 0

How it **Should** be:

    contestant.Points.ShouldBe(1337);

Which is just syntax, so far, but check out the message when it fails:

    contestant.Points should be 1337 but was 0

It might be easy to underestimate how useful this is. Another example, side by side:

    Assert.That(map.IndexOfValue("boo"), Is.EqualTo(2));    // -> Expected 2 but was 1
    map.IndexOfValue("boo").ShouldBe(2);                    // -> map.IndexOfValue("boo") should be 2 but was 1

**Shouldly** uses the variables within the *ShouldBe* statement to report on errors, which makes diagnosing easier.

Another example, if you compare two collections:
    
    (new[] { 1, 2, 3 }).ShouldBe(new[] { 1, 2, 4 });
 
and it fails because they're different, it'll show you the differences between the two collections:

        should be
    [1, 2, 4]
        but was
    [1, 2, 3]
        difference
    [1, 2, *3*]

If you want to check that a particular call does/does not throw an exception, it's as simple as:
    
    Should.Throw<ArgumentOutOfRangeException>(() => widget.Twist(-1));

Then if it chucks a wobbly, you have access to the exception to help debug what the underlying cause was.

Other *Shouldly* features:

####Equality

[ShouldBe] (#equalityShouldBeExample)

[ShouldBeOneOf] (#equalityShouldBeOneOfExample)

[ShouldNotBe] (#equalityShouldNotBeExample)

[ShouldNotBeOneOf] (#equalityShouldNotBeOneOfExample)

[ShouldBeGreaterThan(OrEqualTo)] (#equalityShouldBeGreaterThanExample)

[ShouldBeLessThan(OrEqualTo)] (#equalityShouldBeLessThanExample)

[ShouldBeOfType<T> - Exact type match] (#equalityShouldBeOfTypeExample)

[ShouldBeAssignableTo<T>] (#equalityShouldBeAssignableToExample)

[ShouldBeInRange] (#equalityShouldBeInRangeExample)

[ShouldNotBeInRange] (#equalityShouldNotBeInRangeExample)


####Enumerable

[ShouldBe(optional Tolerance/Ignore order)] (#enumerableShouldBeExample)

[ShouldAllBe(predicate)] (#enumerableShouldAllBeExample)

[ShouldContain] (#enumerableShouldContainExample)

[ShouldContain(predicate)] (#enumerableShouldContainExample)

[ShouldNotContain] (#enumerableShouldNotContainExample)

[ShouldNotContain(predicate)] (#enumerableShouldNotContainExample)

[ShouldBeEmpty] (#enumerableShouldBeEmptyExample)

[ShouldNotBeEmpty] (#enumerableShouldNotBeEmptyExample)

[ShouldBeOneOf(params)] (#enumerableShouldBeOneOfExample)

[ShouldBeSubsetOf] (#enumerableShouldBeSubsetOfExample)

####String

[ShouldStartWith] (#stringShouldStartWithExample)

[ShouldNotStartWith] (#stringShouldNotStartWithExample)

[ShouldEndWith] (#stringShouldEndWithExample)

[ShouldNotEndWith] (#stringShouldNotEndWithExample)

[ShouldContain] (#stringShouldContainExample)

[ShouldNotContain] (#stringShouldNotContainExample)

[ShouldContainWithoutWhitespace] (#stringShouldContainWithoutWhitespaceExample)

[ShouldMatch] (#stringShouldMatchExample)

[ShouldBeNullOrEmpty] (#stringShouldBeNullOrEmptyExample)

[ShouldNotBeNullOrEmpty] (#stringShouldNotBeNullOrEmptyExample)

####Dictionary

[ShouldContainKey] (#dictionaryShouldContainKeyExample)

[ShouldContainKeyAndValue] (#dictionaryShouldContainKeyAndValueExample)

[ShouldNotContainKey] (#dictionaryShouldNotContainKeyExample)

[ShouldNotContainValueForKey] (#dictionaryShouldNotContainValueForKeyExample)


####Exceptions

[Should.Throw<TException>(Action)] (#exceptionsShouldThrowActionExample)

[Should.NotThrow(Action)] (#exceptionsShouldNotThrowActionExample)

[Should.Throw<TException>(Func<Task>[, Timeout])] (#exceptionsShouldThrowFuncExample)

[Should.NotThrow(Func<Task>[, Timeout])] (#exceptionsShouldNotThrowFuncExample)


**Task overloads for Should.Throw are blocking and will automatically timeout after 10 seconds (specified in ShouldlyConfiguration.DefaultTaskTimeout)**

#### Tasks

[CompleteIn] (#tasksCompleteInExample)

#### Dynamic

[ShouldHaveProperty] (#dynamicShouldHaveProperty)

#### Other

[ShouldSatisfyAllConditions] (#shouldSatisfyAllConditions)


<a name="classesUsedInExamples"> </a>
### Examples
All the example use the following classes.

```c#
namespace Simpsons
{
    public abstract class Pet
    {
        public abstract string Name { get; set; }
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
    }
}
```
<a name="equalityShouldBeExample"> </a>
#### Equality - ShouldBe

Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldBe()
{
    var theSimpsonsCat = new Cat() { Name = "Santas little helper" };
    theSimpsonsCat.Name.ShouldBe("Snowball 2");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    theSimpsonsCat.Name
        should be
    "Snowball 2"
        but was
    "Santas little helper"
```

<a name="equalityShouldBeOneOfExample"> </a>
#### Equality - ShouldBeOneOf

Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldBeOneOf()
{
    var apu = new Person() { Name = "Apu" };
    var homer = new Person() { Name = "Homer" };
    var skinner = new Person() { Name = "Skinner" };
    var barney = new Person() { Name = "Barney" };
    var theBeSharps = new List<Person>() { homer, skinner, barney };

    apu.ShouldBeOneOf(theBeSharps.ToArray());
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    apu
        should be one of
    [Simpsons.Person, Simpsons.Person, Simpsons.Person]
        but was
    Simpsons.Person
```

<a name="equalityShouldNotBeExample"> </a>
#### Equality - ShouldNotBe 

Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotBe()
{
    var theSimpsonsCat = new Cat() { Name = "Santas little helper" };
    theSimpsonsCat.Name.ShouldNotBe("Santas little helper");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    theSimpsonsCat.Name
        should not be
    "Santas little helper"
        but was
    "Santas little helper"
```

<a name="equalityShouldNotBeOneOfExample"> </a>
#### Equality - ShouldNotBeOneOf

Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotBeOneOf()
{
    var apu = new Person() { Name = "Apu" };
    var homer = new Person() { Name = "Homer" };
    var skinner = new Person() { Name = "Skinner" };
    var barney = new Person() { Name = "Barney" };
    var wiggum = new Person() { Name = "Wiggum" };
    var theBeSharps = new List<Person>() { apu, homer, skinner, barney, wiggum };

    wiggum.ShouldNotBeOneOf(theBeSharps.ToArray());
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    wiggum
        should not be one of
    [Simpsons.Person, Simpsons.Person, Simpsons.Person, Simpsons.Person, Simpsons.Person]
        but was
    Simpsons.Person
```

<a name="equalityShouldBeGreaterThanExample"> </a>
#### Equality - ShouldBeGreaterThan


Using the classes defined [here](#classesUsedInExamples), the following tests ...

```c#
[Test]
public void ShouldBeGreaterThan_Example_1()
{
    1.ShouldBeGreaterThan(2);
}

[Test]
public void ShouldBeGreaterThan_Example_2()
{
    var mrBurns = new Person() { Name = "Mr. Burns", Salary = 30000 };
    mrBurns.Salary.ShouldBeGreaterThan(300000000);
}
```

... shows the following messages on failure ...

```
Shouldly.ChuckedAWobbly : 
    1
        should be greater than
    2
        but was
    1

Shouldly.ChuckedAWobbly : 
    mrBurns.Salary
        should be greater than
    300000000
        but was
    30000
```

<a name="equalityShouldBeLessThanExample"> </a>
#### Equality - ShouldBeLessThan


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldBeLessThan()
{
    var homer = new Person() { Name = "Homer", Salary = 300000000 };
    homer.Salary.ShouldBeLessThan(30000);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    homer.Salary
        should be less than
    30000
        but was
    300000000
```

<a name="equalityShouldBeOfTypeExample"> </a>
#### Equality - ShouldBeOfType


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldBeOfType()
{
    var theSimpsonsDog = new Cat() { Name = "Santas little helper" };
    theSimpsonsDog.ShouldBeOfType<Dog>();
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    theSimpsonsDog
        should be of type
    Simpsons.Dog
        but was
    Simpsons.Cat
```

<a name="equalityShouldBeAssignableToExample"> </a>
#### Equality - ShouldBeAssignableTo


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldBeAssignableTo()
{
    var theSimpsonsDog = new Person() { Name = "Santas little helper" };
    theSimpsonsDog.ShouldBeAssignableTo<Pet>();
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    theSimpsonsDog
        should be assignable to
    Simpsons.Pet
        but was
    Simpsons.Person
```

<a name="equalityShouldBeInRangeExample"> </a>
#### Equality - ShouldBeInRange


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldBeInRange()
{
    var homer = new Person() { Name = "Homer", Salary = 300000000 };
    homer.Salary.ShouldBeInRange(30000, 40000);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    homer.Salary
        should be in range
    { from = 30000, to = 40000 }
        but was
    300000000
```

<a name="equalityShouldNotBeInRangeExample"> </a>
#### Equality - ShouldNotBeInRange


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotBeInRange()
{
    var mrBurns = new Person() { Name = "Mr. Burns", Salary = 30000 };
    mrBurns.Salary.ShouldNotBeInRange(30000, 40000);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    mrBurns.Salary
        should not be in range
    { from = 30000, to = 40000 }
        but was
    30000
```

<a name="enumerableShouldBeExample"> </a>
#### Enumerable - ShouldBe


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void IEnumerable_ShouldBe()
{
    var apu = new Person() { Name = "Apu" };
    var homer = new Person() { Name = "Homer" };
    var skinner = new Person() { Name = "Skinner" };
    var barney = new Person() { Name = "Barney" };
    var theBeSharps = new List<Person>() { homer, skinner, barney };

    theBeSharps.ShouldBe(new[] {apu, homer, skinner, barney});
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    theBeSharps
        should be
    [Simpsons.Person, Simpsons.Person, Simpsons.Person, Simpsons.Person]
        but was
    [Simpsons.Person, Simpsons.Person, Simpsons.Person]
        difference
    [*Simpsons.Person*, *Simpsons.Person*, *Simpsons.Person*, *]
```

<a name="enumerableShouldAllBeExample"> </a>
#### Enumerable - ShouldAllBe


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void IEnumerable_ShouldAllBe_Predicate()
{
    var mrBurns = new Person() { Name = "Mr.Burns", Salary=3000000};
    var kentBrockman = new Person() { Name = "Homer", Salary = 3000000};
    var homer = new Person() { Name = "Homer", Salary = 30000};
    var millionares = new List<Person>() {mrBurns, kentBrockman, homer};

    millionares.ShouldAllBe(m => m.Salary > 1000000);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    millionares
        should all be an element satisfying the condition
    (m.Salary > 1000000)
        but does not
```

<a name="enumerableShouldContainExample"> </a>
#### Enumerable - ShouldContain


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#

[Test]
public void IEnumerable_ShouldContain()
{
    var mrBurns = new Person() { Name = "Mr.Burns", Salary=3000000};
    var kentBrockman = new Person() { Name = "Homer", Salary = 3000000};
    var homer = new Person() { Name = "Homer", Salary = 30000};
    var millionares = new List<Person>() {kentBrockman, homer};

    millionares.ShouldContain(mrBurns);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    millionares
        should contain
    Simpsons.Person
        but was
    [Simpsons.Person, Simpsons.Person]
```

<a name="enumerableShouldContainExample"> </a>
#### Enumerable - ShouldContain


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void IEnumerable_ShouldContain_Predicate()
{
    var homer = new Person() { Name = "Homer", Salary = 30000};
    var moe = new Person() { Name = "Moe", Salary=20000};
    var barney = new Person() { Name = "Barney", Salary = 0};
    var millionares = new List<Person>() {homer, moe, barney};

    // Check if at least one element in the IEnumerable satisfies the predicate
    millionares.ShouldContain(m => m.Salary > 1000000);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    millionares
        should contain an element satisfying the condition
    (m.Salary > 1000000)
        but does not
```

<a name="enumerableShouldNotContainExample"> </a>
#### Enumerable - ShouldNotContain


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void IEnumerable_ShouldNotContain()
{
    var homerSimpson = new Person() { Name = "Homer"};
    var homerGlumplich  = new Person() { Name = "Homer"};
    var lenny = new Person() { Name = "Lenny"};
    var carl = new Person() { Name = "carl"};
    var clubOfNoHomers = new List<Person>() {homerSimpson, homerGlumplich, lenny, carl };

    clubOfNoHomers.ShouldNotContain(homerSimpson);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    clubOfNoHomers
        should not contain
    Simpsons.Person
        but was
    [Simpsons.Person, Simpsons.Person, Simpsons.Person, Simpsons.Person]
```

<a name="enumerableShouldNotContainExample"> </a>
#### Enumerable - ShouldNotContain


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void IEnumerable_ShouldNotContain_Predicate()
{
    var mrBurns = new Person() { Name = "Mr.Burns", Salary=3000000};
    var kentBrockman = new Person() { Name = "Homer", Salary = 3000000};
    var homer = new Person() { Name = "Homer", Salary = 30000};
    var millionares = new List<Person>() {mrBurns, kentBrockman, homer};

    millionares.ShouldNotContain(m => m.Salary < 1000000);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    millionares
        should not contain an element satisfying the condition
    (m.Salary < 1000000)
        but does 
```

<a name="enumerableShouldBeEmptyExample"> </a>
#### Enumerable - ShouldBeEmpty


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void IEnumerable_ShouldBeEmpty()
{
    var homer = new Person() { Name = "Homer"};
    var powerPlantOnTheWeekend = new List<Person>() {homer};
    powerPlantOnTheWeekend.ShouldBeEmpty(); 
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    powerPlantOnTheWeekend
            should be empty
        but had 1 item and was [Simpsons.Person]
```

<a name="enumerableShouldNotBeEmptyExample"> </a>
#### Enumerable - ShouldNotBeEmpty


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void IEnumerable_ShouldNotBeEmpty()
{
    var moesTavernOnTheWeekend = new List<Person>() {};
    moesTavernOnTheWeekend.ShouldNotBeEmpty(); 
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    moesTavernOnTheWeekend
            should not be empty
        but was 
```

<a name="enumerableShouldBeOneOfExample"> </a>
#### Enumerable - ShouldBeOneOf


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void IEnumerable_ShouldBeOneOf()
{
    var moe  = new Person() { Name = "Moe"};
    var lenny = new Person() { Name = "Lenny"};
    var carl = new Person() { Name = "carl"};
    var stoneCutters = new List<Person>() { moe, lenny, carl };
    var clubOfNoHomers = new List<Person>() { moe, lenny, carl };

    stoneCutters.ShouldBeOneOf(clubOfNoHomers);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    stoneCutters
        should be one of
    [[Simpsons.Person, Simpsons.Person, Simpsons.Person]]
        but was
    [Simpsons.Person, Simpsons.Person, Simpsons.Person]
        difference
    [*Simpsons.Person*, *Simpsons.Person*, *Simpsons.Person*]
```

<a name="enumerableShouldBeSubsetOfExample"> </a>
#### Enumerable - ShouldBeSubsetOf


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void IEnumerable_ShouldBeSubsetOf()
{
    var lisa  = new Person() { Name = "Lisa"};
    var bart = new Person() { Name = "Bart"};
    var maggie = new Person() { Name = "Maggie"};
    var homer = new Person() { Name = "Homer"};
    var marge = new Person() { Name = "Marge"};
    var ralph = new Person() { Name = "Ralph"};
    var simpsonsKids = new List<Person>() { bart, lisa, maggie, ralph };
    var simpsonsFamily = new List<Person>() {lisa, bart, maggie, homer, marge};

    simpsonsKids.ShouldBeSubsetOf(simpsonsFamily);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    simpsonsKids
        should be subset of 
    [Simpsons.Person, Simpsons.Person, Simpsons.Person, Simpsons.Person, Simpsons.Person]
        but does not
```

<a name="stringShouldStartWithExample"> </a>
#### String - ShouldStartWith


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldStartWith()
{
    var lenny = new Person() { Name = "Carl"};
    lenny.Name.ShouldStartWith("Len");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    lenny.Name
        should start with
    "Len"
        but was
    "Carl"
```

<a name="stringShouldNotStartWithExample"> </a>
#### String - ShouldNotStartWith


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotStartWith()
{
    var lenny = new Person() { Name = "Carl"};
    lenny.Name.ShouldNotStartWith("Car");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    lenny.Name
        should not start with
    "Car"
        but was
    "Carl"
```

<a name="stringShouldEndWithExample"> </a>
#### String - ShouldEndWith


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldEndWith()
{
    var lenny = new Person() { Name = "Carl"};
    lenny.Name.ShouldEndWith("nny");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    lenny.Name
        should end with
    "nny"
        but was
    "Carl"
```

<a name="stringShouldNotEndWithExample"> </a>
#### String - ShouldNotEndWith


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotEndWith()
{
    var lenny = new Person() { Name = "Carl"};
    lenny.Name.ShouldNotEndWith("arl");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    lenny.Name
        should not end with
    "arl"
        but was
    "Carl"
```

<a name="stringShouldContainExample"> </a>
#### String - ShouldContain


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldContain()
{
    var lenny = new Person() { Name = "Carl"};
    lenny.Name.ShouldContain("enn");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    lenny.Name
        should contain
    "enn"
        but was
    "Carl"
```

<a name="stringShouldNotContainExample"> </a>
#### String - ShouldNotContain


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotContain()
{
    var lenny = new Person() { Name = "Carl"};
    lenny.Name.ShouldNotContain("ar");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    lenny.Name
        should not contain
    "ar"
        but was
    "Carl"
```

<a name="stringShouldContainWithoutWhitespaceExample"> </a>
#### String - ShouldContainWithoutWhitespace


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldContainWithoutWhitespace()
{
    var simpsonDog = new Dog() { Name = "SantasLittleHelperS"};
    simpsonDog.Name.ShouldContainWithoutWhitespace("Santas Little Helper");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    simpsonDog.Name
        should contain without whitespace
    "Santas Little Helper"
        but was
    "SantasLittleHelperS"
```

<a name="stringShouldMatchExample"> </a>
#### String - ShouldMatch


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldMatch()
{
    var simpsonDog = new Dog() { Name = "Santas little helper" };
    simpsonDog.Name.ShouldMatch("Santas Little Helper");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    simpsonDog.Name
        should match
    "Santas Little Helper"
        but was
    "Santas little helper"
```

<a name="stringShouldBeNullOrEmptyExample"> </a>
#### String - ShouldBeNullOrEmpty


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldBeNullOrEmpty()
{
    var anonymousClanOfSlackJawedTroglodytes = new Person() {Name = "The Simpsons"};
    anonymousClanOfSlackJawedTroglodytes.Name.ShouldBeNullOrEmpty();
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    anonymousClanOfSlackJawedTroglodytes.Name
            should be null or empty
```

<a name="stringShouldNotBeNullOrEmptyExample"> </a>
#### String - ShouldNotBeNullOrEmpty


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotBeNullOrEmpty()
{
    var troyMcClure = new Person() {};
    troyMcClure.Name.ShouldNotBeNullOrEmpty();
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    troyMcClure.Name
            should not be null or empty
```

<a name="dictionaryShouldContainKeyExample"> </a>
#### Dictionary - ShouldContainKey


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldContainKey()
{
    var websters = new Dictionary<string, string>();
    websters.Add("Embiggen", "To empower or embolden.");
    websters.ShouldContainKey("Cromulent");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
Dictionary
    "websters"
should contain key
            "Cromulent"
but does not
```

<a name="dictionaryShouldContainKeyAndValueExample"> </a>
#### Dictionary - ShouldContainKeyAndValue


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldContainKeyAndValue()
{
    var websters = new Dictionary<string, string>();
    websters.Add("Cromulent", "I never heard the word before moving to Springfield.");

    websters.ShouldContainKeyAndValue("Cromulent", "Fine, acceptable.");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
Dictionary
    "websters"
should contain key
"Cromulent"
with value
            "Fine, acceptable."
but value was "I never heard the word before moving to Springfield."
```

<a name="dictionaryShouldNotContainKeyExample"> </a>
#### Dictionary - ShouldNotContainKey


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotContainKey()
{
    var websters = new Dictionary<string, string>();
    websters.Add("Chazzwazzers", "What Australians would have called a bull frog.");

    websters.ShouldNotContainKey("Chazzwazzers");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
Dictionary
    "websters"
should not contain key
            "Chazzwazzers"
but does
```

<a name="dictionaryShouldNotContainValueForKeyExample"> </a>
#### Dictionary - ShouldNotContainValueForKey


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotContainValueForKey()
{
    var websters = new Dictionary<string, string>();
    websters.Add("Chazzwazzers", "What Australians would have called a bull frog.");

    websters.ShouldNotContainValueForKey("Chazzwazzers", "What Australians would have called a bull frog.");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
Dictionary
    "websters"
should not contain key
"Chazzwazzers"
with value
            "What Australians would have called a bull frog."
but does
```

<a name="exceptionsShouldThrowActionExample"> </a>
#### Exceptions - ShouldThrowAction


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldThrow()
{
    var homer = new Person() { Name = "Homer", Salary = 30000};
    var denominator = 1;
    Should.Throw<DivideByZeroException>(() => { 
                                                var y = homer.Salary/denominator;
                                              });
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    Should
        throw 
    System.DivideByZeroException
        but does not
```

<a name="exceptionsShouldNotThrowActionExample"> </a>
#### Exceptions - ShouldNotThrowAction


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotThrow()
{
    var homer = new Person() { Name = "Homer", Salary = 30000};
    var denominator = 0;
    Should.NotThrow(() => { 
                            var y = homer.Salary/denominator;
                          });
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    Should
        not throw 
    System.DivideByZeroException
        but does 
```

<a name="exceptionsShouldThrowFuncExample"> </a>
#### Exceptions - ShouldThrowFunc


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldThrowFuncOfTask()
{
    var homer = new Person() { Name = "Homer", Salary = 30000};
    var denominator = 1;
    Should.Throw<DivideByZeroException>(() =>
    {
        var task = Task.Factory.StartNew(() => { var y = homer.Salary/denominator; });
        return task;
    });
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    Should
        throw 
    System.DivideByZeroException
        but does not
```

<a name="exceptionsShouldNotThrowFuncExample"> </a>
#### Exceptions - ShouldNotThrowFunc


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void ShouldNotThrowFuncOfTask()
{
    var homer = new Person() { Name = "Homer", Salary = 30000};
    var denominator = 0;
    Should.NotThrow(() =>
    {
        var task = Task.Factory.StartNew(() => { var y = homer.Salary/denominator; });
        return task;
    });
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    Should
        not throw 
    System.DivideByZeroException
        but does 
```

<a name="tasksCompleteInExample"> </a>
#### Tasks - CompleteIn


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
// NOTE: Currently not in the latest Nuget version of Shouldly (version - 2.1.1 )
[Test]
public void CompleteIn()
{
    var homer = new Person() { Name = "Homer", Salary = 30000 };
    var denominator = 1;
    Should.CompleteIn(() =>
    {
        Thread.Sleep(2000);
        var y = homer.Salary / denominator;
    }, TimeSpan.FromSeconds(1));
}
```

... shows the following message on failure ...

```
System.TimeoutException : The operation has timed out.
```

<a name="dynamicShouldHaveProperty"> </a>
#### Dynamic - ShouldHaveProperty


Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void DynamicShouldHavePropertyTest()
{
    var homerThinkingLikeFlanders = new ExpandoObject();
    DynamicShould.HaveProperty(homerThinkingLikeFlanders, "IAmABigFourEyedLameO");
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    Dynamic Object
        "homerThinkingLikeFlanders"
    should contain property
                 "IAmABigFourEyedLameO"
        but does not.
```

<a name="shouldSatisfyAllConditions"> </a>
#### Other - ShouldSatisfyAllConditions

It is a good practice to have only one assertion per test. But like they say 'To every rule, there is an exception'.
Sometimes it might be neater to check for multiple things as part of a single test (null checking before asserting for example).
Normally, if the first assertion fails, the test is terminated and the subsequent assertions are not evaluated.
This might require multiple passes to fix each of the failing assertions. But by using the 'ShouldSatisfyAllConditions' method,
all the assertions are evaluated at once and all failures displayed in one message, leading to quicker debugging. For example ...

Using the classes defined [here](#classesUsedInExamples), the following test ...

```c#
[Test]
public void Without_ShouldSatisfyAllConditions()
{
    var mrBurns = new Person() { Name = "Homer", Salary = 30000 };
    mrBurns.Name.ShouldBe("Mr.Burns");
    mrBurns.Salary.ShouldBeGreaterThan(1000000);
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
    mrBurns.Name
        should be
    "Mr.Burns"
        but was
    "Homer"
```

... and stops further execution and assertion. But the following is a better test which ...

```c#
[Test]
public void ShouldSatisfyAllConditions()
{
    var millionaire = new Person() { Name = "Homer", Salary = 30000 };
    millionaire.ShouldSatisfyAllConditions
        (
            () => millionaire.Name.ShouldBe("Mr.Burns"),
            () => millionaire.Salary.ShouldBeGreaterThan(1000000)
        );
}
```

... shows the following message on failure ...

```
Shouldly.ChuckedAWobbly : 
        millionaire should satisfy all the conditions specified, but does not.
        The following errors were found ...
--------------- Error 1 ---------------

    millionaire.Name
        should be
    "Mr.Burns"
        but was
    "Homer"

--------------- Error 2 ---------------

    millionaire.Salary
        should be greater than
    1000000
        but was
    30000

-----------------------------------------
```
