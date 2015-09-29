`ShouldNotBe` is the inverse of ShouldBe.

## Objects
`ShouldNotBe` works on all types and compares using `.Equals`.

``` csharp
var theSimpsonsCat = new Cat() { Name = "Santas little helper" };
theSimpsonsCat.Name.ShouldNotBe("Santas little helper");
```
> ```
theSimpsonsCat.Name
    should not be
"Santas little helper"
    but was
"Santas little helper"
```

Want to contribute to Shouldly? [#304](https://github.com/shouldly/shouldly/issues/304) makes this error message better!

## Numeric, DateTime(Offset), TimeSpan
Shouldly does not have tolerance overloads for numbers, datetime or timespans for ShouldNotBe.

If you would like to add them, the issues are at:

 - [Numeric Overload Missing](https://github.com/shouldly/shouldly/issues/305)
 - [DateTime Overload Missing](https://github.com/shouldly/shouldly/issues/306)
 - [TimeSpan Overload Missing](https://github.com/shouldly/shouldly/issues/307)

## Enumerables
Please contribute a sample for `ShouldNotBe` and enumerables!
