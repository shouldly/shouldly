var homerSimpson = new Person { Name = "Homer" };
var homerGlumplich = new Person { Name = "Homer" };
var lenny = new Person { Name = "Lenny" };
var carl = new Person { Name = "carl" };
var clubOfNoHomers = new List<Person> { homerSimpson, homerGlumplich, lenny, carl };
clubOfNoHomers.ShouldNotContain(homerSimpson);