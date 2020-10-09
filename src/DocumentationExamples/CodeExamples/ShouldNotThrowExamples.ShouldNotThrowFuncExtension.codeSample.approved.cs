string? name = null;
Func<Person> func = () => new Person(name!);
func.ShouldNotThrow();