using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class ShouldlyRhinoTests
    {
        public enum Direction
        {
            North, East, South, West
        }

        public interface IZombie
        {
            void EatHuman(Direction direction);
        }

        [Test]
        public void OnShouldHaveBeenCalled_WhenCalled_ShouldPass()
        {
            var steve = Create.Mock<IZombie>();

            steve.EatHuman(Direction.North);

            steve.ShouldHaveBeenCalled(s => s.EatHuman(Direction.North));
        }

        [Test]
        public void OnShouldHaveBeenCalled_WhenNotCalled_ShouldFailWithOtherCallsShown()
        {
            var steve = Create.Mock<IZombie>();

            steve.EatHuman(Direction.East);
            steve.EatHuman(Direction.South);
            steve.EatHuman(Direction.West);

            Should.Error(() =>
                steve.ShouldHaveBeenCalled(s => s.EatHuman(Direction.North)),
                @"*Expecting*
                      EatHuman(North)
                  *Recorded*
                   0: EatHuman(Direction.East)
                   1: EatHuman(Direction.South)
                   2: EatHuman(Direction.West)"); 
        }
    }
}