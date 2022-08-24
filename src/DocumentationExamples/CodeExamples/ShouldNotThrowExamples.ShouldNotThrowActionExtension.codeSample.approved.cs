var homer = new Person { Name = "Homer", Salary = 30000 };
var denominator = 0;
var action = () =>
            {
                var y = homer.Salary / denominator;
            };
action.ShouldNotThrow();