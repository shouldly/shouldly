using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeUnique
{
	public class InegerArrayScenario : ShouldlyShouldTestScenario
	{
		protected override string ChuckedAWobblyErrorMessage {
			get 
			{
				return "new [] { 1, 2, 3 } should not be unique [ 1, 2, 3 ] but does";
			}
		}

		protected override void ShouldThrowAWobbly ()
		{
			new [] { 1, 2, 3 }.ShouldNotBeUnique();
		}

		protected override void ShouldPass()
		{
			new [] { 1, 2, 2 }.ShouldNotBeUnique();
		}
	}
}

