var mrBurns = new Person { Name = null };
mrBurns.ShouldSatisfyAllConditions(
                        () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                        () => mrBurns.Name.ShouldBe("Mr.Burns")
                    );