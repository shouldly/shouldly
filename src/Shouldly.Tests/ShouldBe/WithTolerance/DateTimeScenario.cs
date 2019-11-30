using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DateTimeScenario
    {
        [Fact]
        public void DateTimeScenarioShouldFail()
        {
            var date = new DateTime(2000, 6, 1);
            var dateString = date.ToString("o");
            var expected = new DateTime(2000, 6, 1, 1, 0, 1);
            var expectedString = expected.ToString("o");
        }
         


public class DateTimeUTC
        {
            public static void DateTimeUTC1()
            {

                DateTime saveNow = DateTime.Now;
                Console.WriteLine(saveNow);


                DateTime saveUtcNow = DateTime.UtcNow;
                DateTime myDt;


                DisplayNow("UtcNow: ..........", saveUtcNow);
                DisplayNow("Now: .............", saveNow);
                Console.WriteLine();



                myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Utc);
                Display("Utc: .............", myDt);



                myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Local);
                Display("Local: ...........", myDt);



                myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Unspecified);
                Display("Unspecified: .....", myDt);
            }



            public static string datePatt = @"M/d/yyyy hh:mm:ss tt";
            public static void Display(string title, DateTime inputDt)
            {

                DateTime dispDt = inputDt;
                string dtString;



                dtString = dispDt.ToString(datePatt);
                Console.WriteLine("{0} {1}, Kind = {2}",
                                  title, dtString, dispDt.Kind);



                dispDt = inputDt.ToLocalTime();
                dtString = dispDt.ToString(datePatt);
                Console.WriteLine("  ToLocalTime:     {0}, Kind = {1}",
                                  dtString, dispDt.Kind);



                dispDt = inputDt.ToUniversalTime();
                dtString = dispDt.ToString(datePatt);
                Console.WriteLine("  ToUniversalTime: {0}, Kind = {1}",
                                  dtString, dispDt.Kind);
                Console.WriteLine();

            }

            // Display the value and Kind property for DateTime.Now and DateTime.UtcNow.

            public static void DisplayNow(string title, DateTime inputDt)
            {




                string dtString = inputDt.ToString(datePatt);
                Console.WriteLine("{0} {1}, Kind = {2}",
                                  title, dtString, inputDt.Kind);

            }
        }


    }
}

