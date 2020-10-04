Func<Person> func = () => new Person("Homer");
func.ShouldThrow<ArgumentNullException>();