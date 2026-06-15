var mrBurns = new Person { Name = null };
mrBurns.ShouldSatisfy(
                [
                    p => p.Name.ShouldNotBeNullOrEmpty(),
                    p => p.Name.ShouldBe("Mr.Burns")
                ]);