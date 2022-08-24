var func = () => new Person("Homer");
func.ShouldThrow<ArgumentNullException>();