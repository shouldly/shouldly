var timeSpan = TimeSpan.FromHours(1);
timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d));