var homer = new Person { Name = "Homer", Salary = 30000 };
var moe = new Person { Name = "Moe", Salary = 20000 };
var barney = new Person { Name = "Barney", Salary = 0 };
var millionaires = new List<Person> { homer, moe, barney };
millionaires.ShouldContain(m => m.Salary > 1000000);