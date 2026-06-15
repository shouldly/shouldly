var mrBurns = new Person { Name = null };
var homer = new Person { Name = "Homer" };
Should.Satisfy(
                [
                    () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                    () => homer.Name.ShouldBe("Mr.Burns")
                ]);