var bart = new Person { Name = "Bart" };
var detentionOnTheLastDayOfSchool = new List<Person> { bart };
detentionOnTheLastDayOfSchool.ShouldBeNullOrEmpty();