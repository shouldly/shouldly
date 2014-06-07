#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldThrow
{
    public class TaskTimeoutTests
    {
        [Test]
        public void CanSpecifyTimeout()
        {
            Should.Throw<TimeoutException>(()=>
                Should.Throw<Exception>(() => 
                    Task.Factory.StartNew(() => Thread.Sleep(TimeSpan.FromSeconds(2)), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default), 
                    TimeSpan.FromSeconds(1)));
        }
    }
}
#endif