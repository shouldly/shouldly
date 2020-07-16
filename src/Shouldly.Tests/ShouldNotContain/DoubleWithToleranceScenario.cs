//namespace Shouldly.Tests.ShouldNotContain
//{
//    // TODO Add missing overloads to ShouldNotContain
//    public class DoubleWithToleranceScenario : ShouldlyShouldTestScenario
//    {
//        protected override void ShouldThrowAWobbly()
//        {
//            new[] { 1d, 2d, 3d }.ShouldNotContain(1.91, 0.1d);
//        }

//        protected override string ChuckedAWobblyErrorMessage
//        {
//            get
//            {
//                return "new[]{1d, 2d, 3d} should contain 1.91
//    but was
//[1, 2, 3]"; }
//        }

//        protected override void ShouldPass()
//        {
//            new[] { 1d, 2d, 3d }.ShouldNotContain(1.8d, 0.1d);
//        }
//    }
//}