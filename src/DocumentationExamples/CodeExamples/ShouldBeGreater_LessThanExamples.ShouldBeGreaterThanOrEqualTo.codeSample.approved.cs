var mrBurns = new Person { Name = "Mr. Burns", Salary = 299999999 };
mrBurns.Salary.ShouldBeGreaterThanOrEqualTo(300000000);