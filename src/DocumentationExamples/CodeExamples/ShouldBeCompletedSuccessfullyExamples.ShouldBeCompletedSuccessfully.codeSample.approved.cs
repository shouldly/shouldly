var lookup = Task.FromException<int>(new InvalidOperationException("Cache miss"));
var value = lookup.ShouldBeCompletedSuccessfully();