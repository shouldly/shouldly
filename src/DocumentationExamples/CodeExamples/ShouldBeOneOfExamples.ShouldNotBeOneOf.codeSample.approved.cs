var apu = new Person { Name = "Apu" };
var homer = new Person { Name = "Homer" };
var skinner = new Person { Name = "Skinner" };
var barney = new Person { Name = "Barney" };
var wiggum = new Person { Name = "Wiggum" };
var theBeSharps = new List<Person> { apu, homer, skinner, barney, wiggum };
wiggum.ShouldNotBeOneOf(theBeSharps.ToArray());