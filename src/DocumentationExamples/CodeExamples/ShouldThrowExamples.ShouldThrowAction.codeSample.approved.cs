var homer = new Person { Name = "Homer", Salary = 30000 };
var denominator = 1;
Should.Throw<DivideByZeroException>(() =>
                {
                    var y = homer.Salary / denominator;
                });