﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldNotThrowTaskAsyncExtensions
    {
        /*** ShouldNotThrowAsync(Task) ***/
        public static Task ShouldNotThrowAsync(this Task task, string? customMessage = null)
        {
            return Should.NotThrowAsync(task, customMessage);
        }
        public static Task ShouldNotThrowAsync(this Task task, [InstantHandle] Func<string?>? customMessage)
        {
            return Should.NotThrowAsync(task, customMessage);
        }

        /*** ShouldNotThrowAsync(Func<Task>) ***/
        public static Task ShouldNotThrowAsync(this Func<Task> actual, string? customMessage = null)
        {
            return Should.NotThrowAsync(actual, customMessage);
        }
        public static Task ShouldNotThrowAsync(this Func<Task> actual, [InstantHandle] Func<string?>? customMessage)
        {
            return Should.NotThrowAsync(actual, customMessage);
        }
    }
}