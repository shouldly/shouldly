using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests
{
    public static partial class StackTraceTests
    {
        [Theory]
        [MemberData(nameof(ExceptionThrowers))]
        public static void Top_stack_frame_is_user_code(ExceptionThrower exceptionThrower)
        {
            var exception = exceptionThrower.Catch();

            var stackTraceLines = exception.StackTrace.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            stackTraceLines.First().ShouldContain(exceptionThrower.ThrowingAction.Method.Name);
        }

        public static IEnumerable<object[]> ExceptionThrowers()
        {
            return new ExceptionThrowerCollectionBuilder()
#pragma warning disable 618
                .Add<ChuckedAWobbly>(
                    throwDirectly: () => throw new ChuckedAWobbly(null),
                    reasonNotThrowingFromShouldlyAssembly: "Exact type not thrown in Shouldly assembly")
#pragma warning restore 618

                .Add<ShouldAssertException>(
                    throwDirectly: () => throw new ShouldAssertException(null),
                    throwInShouldlyAssembly: new Action[]
                    {
                        FailingUserCode_ShouldBeTrue,
                        FailingUserCode_ShouldContain
                    })

                .Add<ShouldlyTimeoutException>(
                    throwDirectly: () => throw new ShouldlyTimeoutException(null, null),
                    reasonNotThrowingFromShouldlyAssembly: "Exact type not thrown in Shouldly assembly")

                .Add<ShouldCompleteInException>(
                    throwDirectly: () => throw new ShouldCompleteInException(null, null),
                    throwInShouldlyAssembly: FailingUserCode_CompleteIn)

                .Add<ShouldMatchApprovedException>(
                    throwDirectly: () => throw new ShouldMatchApprovedException(null, null, null),
                    reasonNotThrowingFromShouldlyAssembly: "Don’t want to actually create a file on disk")

                .Build()
                .Select(exceptionThrower => new object[] { exceptionThrower });
        }

        private static void FailingUserCode_ShouldBeTrue()
        {
            false.ShouldBeTrue();
        }

        private static void FailingUserCode_ShouldContain()
        {
            // Causes a few more frames that need to be filtered
            "".ShouldContain("42");
        }

        private static void FailingUserCode_CompleteIn()
        {
            // Throws a different exception type
            Should.CompleteIn(Task.Delay(15), TimeSpan.Zero);
        }
    }
}
