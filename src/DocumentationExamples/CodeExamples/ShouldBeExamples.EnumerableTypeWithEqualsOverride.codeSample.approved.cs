var bag = new IdBag(1, 1, 2, 3);
bag.ShouldBe<IdBag>(new IdBag(2, 1, 2, 3));