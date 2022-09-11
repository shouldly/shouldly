var mrBurns = new Person { Name = null };
mrBurns.ShouldSatisfyAllConditions(
                    p => p.Name.ShouldNotBeNullOrEmpty(),
                    p => p.Name.ShouldBe("Mr.Burns"));