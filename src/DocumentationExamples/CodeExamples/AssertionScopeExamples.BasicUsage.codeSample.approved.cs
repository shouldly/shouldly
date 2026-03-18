var homer = new Person { Name = "Homer", Salary = 30000 };
using var scope = new AssertionScope();
homer.Name.ShouldBe("Mr.Burns");
homer.Salary.ShouldBeGreaterThan(1000000);