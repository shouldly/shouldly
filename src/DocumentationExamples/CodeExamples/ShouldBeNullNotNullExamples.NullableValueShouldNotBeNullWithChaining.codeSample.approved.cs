SomeStruct? nullableValue = new SomeStruct { IntProperty = 41 };
nullableValue.ShouldNotBeNull().IntProperty.ShouldBe(42);