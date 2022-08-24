var homer = new Person { Name = "Homer", Salary = 30000 };
var denominator = 1;
var action = () =>
            {
                var y = homer.Salary / denominator;
            };
action.ShouldThrow<DivideByZeroException>();