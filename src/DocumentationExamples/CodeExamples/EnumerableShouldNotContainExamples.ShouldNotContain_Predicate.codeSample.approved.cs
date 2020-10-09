var mrBurns = new Person { Name = "Mr.Burns", Salary = 3000000 };
var kentBrockman = new Person { Name = "Homer", Salary = 3000000 };
var homer = new Person { Name = "Homer", Salary = 30000 };
var millionaires = new List<Person> { mrBurns, kentBrockman, homer };
millionaires.ShouldNotContain(m => m.Salary < 1000000);