var mrBurns = new Person { Name = "Mr. Burns", Salary = 30000 };
mrBurns.Salary.ShouldNotBeInRange(30000, 40000);