var maggie = new Person { Name = "Maggie" };
var homer = new Person { Name = "Homer" };
var simpsonsBabies = new List<Person> { homer, maggie };
simpsonsBabies.ShouldHaveSingleItem();