var simpsons = new[] { "Homer", "marge", "Bart" };
simpsons.ShouldBe(["bart", "HOMER", "Lisa"], Case.Insensitive, ignoreOrder: true);