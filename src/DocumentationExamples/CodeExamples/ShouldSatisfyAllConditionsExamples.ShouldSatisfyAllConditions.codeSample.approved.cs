var mrBurns = new Person { Name = null };
mrBurns.ShouldSatisfy(
                [
                    () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                    () => mrBurns.Name.ShouldBe("Mr.Burns")
                ]);