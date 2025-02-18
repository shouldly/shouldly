var lisa = new Person { Name = "Lisa" };
var bart = new Person { Name = "Bart" };
var maggie = new Person { Name = "Maggie" };
var homer = new Person { Name = "Homer" };
var marge = new Person { Name = "Marge" };
var ralph = new Person { Name = "Ralph" };
var simpsonsKids = new List<Person> { bart, lisa, maggie, ralph };
var simpsonsFamily = new List<Person> { lisa, bart, maggie, homer, marge };
simpsonsFamily.ShouldBeSupersetOf(simpsonsKids);