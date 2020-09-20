var homer = new Person { Name = "Homer", Salary = 300000000 };
homer.Salary.ShouldBeInRange(30000, 40000);