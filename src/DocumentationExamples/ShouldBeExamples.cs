using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Shouldly;
using Simpsons;
using Xunit;

namespace DocumentationExamples
{
    public class ShouldBeExamples
    {
        [Fact]
        public void Objects()
        {
            DocExampleWriter.Document(() =>
                {
                    var theSimpsonsCat = new Cat { Name = "Santas little helper" };
                    theSimpsonsCat.Name.ShouldBe("Snowball 2");
                });
        }

        [Fact]
        public void Numeric()
        {
            DocExampleWriter.Document(() =>
            {
                const decimal pi = (decimal)Math.PI;
                pi.ShouldBe(3.24m, 0.01m);
            });
        }

        [Fact]
        public void DateTime()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-AU");
            DocExampleWriter.Document(() =>
            {
                var date = new DateTime(2000, 6, 1);
                date.ShouldBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1));
            });
        }

        [Fact]
        public void TimeSpanExample()
        {
            DocExampleWriter.Document(() =>
            {
                var timeSpan = TimeSpan.FromHours(1);
                timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1));
            });
        }

        [Fact]
        public void Enumerables()
        {
            DocExampleWriter.Document(() =>
            {
                var apu = new Person { Name = "Apu" };
                var homer = new Person { Name = "Homer" };
                var skinner = new Person { Name = "Skinner" };
                var barney = new Person { Name = "Barney" };
                var theBeSharps = new List<Person> { homer, skinner, barney };

                theBeSharps.ShouldBe(new[] { apu, homer, skinner, barney });
            });
        }

        [Fact]
        public void EnumerablesOfNumerics()
        {
            DocExampleWriter.Document(() =>
            {
                var firstSet = new[] { 1.23m, 2.34m, 3.45001m };
                var secondSet = new[] { 1.4301m, 2.34m, 3.45m };
                firstSet.ShouldBe(secondSet, 0.1m);
            });
        }

        [Fact]
        public void BooleanExample()
        {
            DocExampleWriter.Document(() =>
            {
                const bool myValue = false;
                myValue.ShouldBe(true, "Some additional context");
            });
        }
    }
}
