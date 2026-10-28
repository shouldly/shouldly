var func = () => new Person(null!);
func.ShouldThrowExactly<ArgumentException>();