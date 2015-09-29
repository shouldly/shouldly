## Objects
`ShouldBe` works on all types and compares using `.Equals`.

``` csharp
var theSimpsonsCat = new Cat() { Name = "Santas little helper" };
theSimpsonsCat.Name.ShouldBe("Snowball 2");
```
> ```
theSimpsonsCat.Name
    should be
"Snowball 2"
    but was
"Santas little helper"
```

## Numeric
`ShouldBe` numeric overloads accept tolerances and has overloads for `float`, `double` and `decimal` types.

``` csharp
const decimal pi = (decimal)Math.PI;
pi.ShouldBe(3.24m, 0.01m);
```

> ```
pi
   should be within
0.01
    of
3.24
    but was
3.14159265358979
```

## DateTime(Offset)
DateTime overloads are similar to the numeric overloads and support tolerances.

``` csharp
var date = new DateTime(2000, 6, 1);
date.ShouldBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1));
```

> ```
date
   should be within
01:00:00
    of
1/06/2000 1:00:01 AM
    but was
1/06/2000 12:00:00 AM
```

## TimeSpan
TimeSpan also has tolerance overloads

``` csharp
var timeSpan = TimeSpan.FromHours(1);
timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1));
```

> ```
timeSpan
   should be within
01:00:00
    of
02:06:00
    but was
01:00:00
```

Want to improve shouldy? We have an open issue at [#303](https://github.com/shouldly/shouldly/issues/303) to improve this error message!

## Enumerables
Enumerable comparison is done on the elements in the enumerable, so you can compare an array to a list and have it pass.
``` csharp
var apu = new Person() { Name = "Apu" };
var homer = new Person() { Name = "Homer" };
var skinner = new Person() { Name = "Skinner" };
var barney = new Person() { Name = "Barney" };
var theBeSharps = new List<Person>() { homer, skinner, barney };

theBeSharps.ShouldBe(new[] {apu, homer, skinner, barney});
```

> ```
theBeSharps
    should be
[Apu, Homer, Skinner, Barney]
    but was
[Homer, Skinner, Barney]
    difference
[*Homer*, *Skinner*, *Barney*, *]
```

## Enumerables of Numerics
If you have enumerables of `float`, `decimal` or `double` types then you can use the tolerance overloads, similar to the value extensions.

``` csharp
var firstSet = new[] { 1.23m, 2.34m, 3.45001m };
var secondSet = new[] { 1.4301m, 2.34m, 3.45m };
firstSet.ShouldBe(secondSet, 0.1m);
```

> ```
firstSet
   should be within
0.1
    of
[1.4301, 2.34, 3.45]
    but was
[1.23, 2.34, 3.45001]
    difference
[*1.23*, 2.34, *3.45001*]
```
