var bag = new IdBag(1, 1, 2, 3);
bag.ShouldBe(new IdBag(2, 1, 2, 3));