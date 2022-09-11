var date = new DateTime(2000, 6, 1);
date.ShouldNotBe(new(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1.5));