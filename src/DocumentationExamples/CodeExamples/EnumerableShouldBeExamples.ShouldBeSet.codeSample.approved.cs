var bowlingTeam = new HashSet<string> { "Homer", "Moe", "Otto" };
bowlingTeam.ShouldBe(new HashSet<string> { "Apu", "Homer", "Moe" });