string? name = null;
var func = () => new Person(name!);
func.ShouldNotThrow();