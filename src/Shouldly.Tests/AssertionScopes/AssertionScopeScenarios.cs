namespace Shouldly.Tests.AssertionScopes;

public class AssertionScopeScenarios
{
    [Fact]
    public void NoFailures_ShouldNotThrow()
    {
        using (new AssertionScope())
        {
            1.ShouldBe(1);
            "hello".ShouldBe("hello");
        }
    }

    [Fact]
    public void SingleFailure_ShouldThrowWithMessage()
    {
        Verify.ShouldFail(() =>
        {
            using (new AssertionScope())
            {
                1.ShouldBe(2);
            }
        }
           );
    }

    [Fact]
    public void MultipleFailures_ShouldCollectAll()
    {
        Verify.ShouldFail(() =>
        {
            using (new AssertionScope())
            {
                1.ShouldBe(2);
                "actual".ShouldBe("expected");
            }
        }
          );
    }


    [Fact]
    public void MultipleFailures_ShouldCollectAll_WithoutEnclosingBraces()
    {
        Verify.ShouldFail(() =>
        {
            using var _ = new AssertionScope();
            1.ShouldBe(2);
            "actual".ShouldBe("expected");
        }
          );
    }


    [Fact]
    public void NestedScope_PropagatesFailuresToOuterScope()
    {
        Verify.ShouldFail(() =>
        {
            using (new AssertionScope())
            {
                1.ShouldBe(2);

                using (new AssertionScope())
                {
                    "inner".ShouldBe("outer");
                }

                true.ShouldBe(true);
            }
        }
         );
    }

    [Fact]
    public void NestedScope_InnerDoesNotThrow()
    {
        var ex = Assert.Throws<ShouldAssertException>(() =>
        {
            using (new AssertionScope())
            {
                using (new AssertionScope())
                {
                    1.ShouldBe(2);
                }

                // Execution continues after inner scope disposes
                3.ShouldBe(4);
            }
        });

        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
    }


    [Fact]
    public void MixedPassAndFail_OnlyCollectsFailures()
    {
        Verify.ShouldFail(() =>
        {
            using (new AssertionScope())
            {
                1.ShouldBe(1);
                2.ShouldBe(3);
                "hello".ShouldBe("hello");
                "a".ShouldBe("b");
            }
        }
         );
    }

    [Fact]
    public void ShouldSatisfyAllConditions_InsideScope_WorksCorrectly()
    {
        Verify.ShouldFail(() =>
        {
            using (new AssertionScope())
            {
                1.ShouldBe(2);

                var result = 4;
                result.ShouldSatisfyAllConditions(
                [
                    () => result.ShouldBeOfType<int>(),
                    () => result.ShouldBeGreaterThan(3)
                ]);
            }
        }
        );
    }

    [Fact]
    public void ShouldBeOfType_FailureInsideScope_DoesNotShortCircuitScope()
    {
        var ex = Assert.Throws<ShouldAssertException>(() =>
        {
            using (new AssertionScope())
            {
                object actual = "hello";
                actual.ShouldBeOfType<int>();
                1.ShouldBe(2);
            }
        });

        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
    }

    [Fact]
    public void ShouldBeAssignableTo_FailureInsideScope_DoesNotShortCircuitScope()
    {
        var ex = Assert.Throws<ShouldAssertException>(() =>
        {
            using (new AssertionScope())
            {
                object actual = "hello";
                actual.ShouldBeAssignableTo<int>();
                1.ShouldBe(2);
            }
        });

        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
    }

    [Fact]
    public void ShouldBeEquivalentTo_NullMismatchInsideScope_DoesNotShortCircuitScope()
    {
        var ex = Assert.Throws<ShouldAssertException>(() =>
        {
            using (new AssertionScope())
            {
                "actual".ShouldBeEquivalentTo(null);
                1.ShouldBe(2);
            }
        });

        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
    }

    [Fact]
    public void ShouldBeEquivalentTo_TypeMismatchInsideScope_DoesNotShortCircuitScope()
    {
        var ex = Assert.Throws<ShouldAssertException>(() =>
        {
            using (new AssertionScope())
            {
                "hello".ShouldBeEquivalentTo(5);
                1.ShouldBe(2);
            }
        });

        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
    }

    [Fact]
    public void ShouldBeEquivalentTo_EnumerableCountMismatchInsideScope_DoesNotShortCircuitScope()
    {
        var ex = Assert.Throws<ShouldAssertException>(() =>
        {
            using (new AssertionScope())
            {
                new[] { 1, 2 }.ShouldBeEquivalentTo(new[] { 1 });
                1.ShouldBe(2);
            }
        });

        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
    }

    [Fact]
    public void EmptyScope_DisposesCleanly()
    {
        using (new AssertionScope())
        {
        }
    }

    [Fact]
    public void DoubleDispose_DoesNotThrow()
    {
        var scope = new AssertionScope();
        scope.Dispose();
        scope.Dispose();
    }

    [Fact]
    public void DeeplyNestedScopes_PropagateToOutermost()
    {
        var ex = Assert.Throws<ShouldAssertException>(() =>
        {
            using (new AssertionScope())
            {
                using (new AssertionScope())
                {
                    using (new AssertionScope())
                    {
                        1.ShouldBe(2);
                    }

                    3.ShouldBe(4);
                }

                5.ShouldBe(6);
            }
        });

        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
        ex.Message.ShouldContain("Error 3");
    }

    // The following tests characterise nested-scope behaviour. The exact semantics are not
    // contractual - they simply pin down that nesting behaves reasonably and does not explode.

    [Fact]
    public void NestedScope_CleanInner_DoesNotAffectOuterReport()
    {
        // A nested scope with no failures contributes nothing; only the outer failure is reported.
        Verify.ShouldFail(() =>
        {
            using (new AssertionScope())
            {
                using (new AssertionScope())
                {
                    1.ShouldBe(1);
                }

                2.ShouldBe(3);
            }
        });
    }

    [Fact]
    public void NestedScope_CollectsInnerThenOuterFailuresInOrder()
    {
        // Failures from a disposed inner scope are reported before later outer failures.
        Verify.ShouldFail(() =>
        {
            using (new AssertionScope())
            {
                using (new AssertionScope())
                {
                    "inner".ShouldBe("inner-expected");
                }

                "outer".ShouldBe("outer-expected");
            }
        });
    }

    [Fact]
    public void NestedScope_InnerDoubleDispose_DoesNotDuplicateFailures()
    {
        // Disposing the inner scope twice must not propagate its failures to the outer scope twice.
        Verify.ShouldFail(() =>
        {
            using (new AssertionScope())
            {
                var inner = new AssertionScope();
                1.ShouldBe(2);
                inner.Dispose();
                inner.Dispose();
            }
        });
    }

    [Fact]
    public async Task NestedScope_InnerOnSeparateTask_PropagatesToOuter()
    {
        // An inner scope created on a pool thread still flows from the outer scope and merges
        // back into it without races.
        var ex = await Assert.ThrowsAsync<ShouldAssertException>(async () =>
        {
            using (new AssertionScope())
            {
                1.ShouldBe(2);

                await Task.Run(() =>
                {
                    using (new AssertionScope())
                    {
                        3.ShouldBe(4);
                    }
                }, TestContext.Current.CancellationToken);
            }
        });

        ex.Message.ShouldMatchApproved(c => c.NoDiff());
    }

    [Fact]
    public async Task NestedScope_DisposedOutOfOrder_DoesNotThrow()
    {
        // Disposing scopes out of nesting order is misuse. Capture that it fails safe (no
        // exception) rather than exploding; the inner failure is simply not reported. Run on a
        // forked execution context so the dangling AsyncLocal state cannot leak into other tests.
        await Task.Run(() =>
        {
            var outer = new AssertionScope();
            var inner = new AssertionScope();

            1.ShouldBe(2);

            Should.NotThrow(() =>
            {
                outer.Dispose();
                inner.Dispose();
            });
        }, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task AsyncScope_CapturesFailures()
    {
        var ex = await Assert.ThrowsAsync<ShouldAssertException>(async () =>
        {
            using (new AssertionScope())
            {
                await Task.Yield();
                1.ShouldBe(2);
                await Task.Yield();
                2.ShouldBe(3);
#pragma warning disable xUnit1051 // Calls to methods which accept CancellationToken should use TestContext.Current.CancellationToken
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
#pragma warning restore xUnit1051 // Calls to methods which accept CancellationToken should use TestContext.Current.CancellationToken
                3.ShouldBe(4);
            }
        });

        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
        ex.Message.ShouldContain("Error 3");
    }

    [Fact(Timeout = 5000)]
    public void ScopesOnDifferentThreads_AreIndependent()
    {
        var timeout = TimeSpan.FromSeconds(3);
        //use barrier to ensure both AssertionScopes are active at the same time, so we have a chance for any cross-thread interference to manifest
        var barrier = new Barrier(2);

        Exception? thread1Exception = null;
        Exception? thread2Exception = null;

        var t1 = new Thread(() =>
        {
            try
            {
                using (new AssertionScope())
                {
                    1.ShouldBe(2);
                    barrier.SignalAndWait(timeout);
                    barrier.SignalAndWait(timeout);
                }
            }
            catch (Exception ex)
            {
                thread1Exception = ex;
            }
        });

        var t2 = new Thread(() =>
        {
            try
            {
                using (new AssertionScope())
                {
                    barrier.SignalAndWait(timeout);
                    "a".ShouldBe("b");
                    barrier.SignalAndWait(timeout);
                }
            }
            catch (Exception ex)
            {
                thread2Exception = ex;
            }
        });

        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();

        thread1Exception.ShouldNotBeNull();
        thread1Exception.ShouldBeOfType<ShouldAssertException>();
        thread1Exception.Message.ShouldContain("Error 1");
        thread1Exception.Message.ShouldNotContain("Error 2");

        thread2Exception.ShouldNotBeNull();
        thread2Exception.ShouldBeOfType<ShouldAssertException>();
        thread2Exception.Message.ShouldContain("Error 1");
        thread2Exception.Message.ShouldNotContain("Error 2");
    }
}
