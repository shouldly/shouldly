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
