var homer = new Person { Name = "Homer", Salary = 30000 };
var denominator = 0;
Should.NotThrow(() =>
                {
                    var task = Task.Factory.StartNew(() => { var y = homer.Salary / denominator; });
                    return task;
                });