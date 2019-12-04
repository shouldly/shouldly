using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DateTimeScenario
    {
         public class DateTimeUTC
        
        {

            public static void DateTimeUTC1()
            {
                //Get the date and time for the current moment, adjusted to the local time zone.
                DateTime dateNow = DateTime.Now;
                Console.WriteLine(dateNow);

                // Get  The current date and time as coordinated universal time (UTC).
                DateTime timeUtcNow = DateTime.UtcNow;
                DateTime datetimeNow;

                // Display the value and Kind property of the current moment expressed as UTC and local time. 
                DisplayNow("currentUtctime: ..........", timeUtcNow);
                DisplayNow("Now: .............", dateNow);
                Console.WriteLine();


                // Change the Kind property of the current moment to DateTimeKind.Utc and display the result.
                datetimeNow = DateTime.SpecifyKind(dateNow, DateTimeKind.Utc);
                datetimeDisplay("Utc: .............", datetimeNow);


                // Change the Kind property of the current moment to DateTimeKind.Local and display the result
                datetimeNow = DateTime.SpecifyKind(dateNow, DateTimeKind.Local);
                datetimeDisplay("Local: ...........", datetimeNow);


                // Change the Kind property of the current moment to DateTimeKind.Unspecified and display the result
                datetimeNow = DateTime.SpecifyKind(dateNow, DateTimeKind.Unspecified);
                datetimeDisplay("Unspecified: .....", datetimeNow);
            }



            // Display the value and Kind property of a DateTime structure, the DateTime structure converted to local time, and structure converted to universal time.
            public static string datetimePattern = @"d/m/yyyy hh:mm:ss tt";

            public static void datetimeDisplay(string title, DateTime inputDatetime)
            {

                DateTime displayDt = inputDatetime;
                string datetimeString;
                // Display the original DateTime.



                datetimeString = displayDt.ToString(datetimePattern);
                Console.WriteLine("{0} {1}, Kind = {2}", title, datetimeString, displayDt.Kind);
                // Convert inputDateTime to local time and display the result. 
                // If inputDateTime.Kind is DateTimeKind.Utc, the conversion is performed.
                // If inputDateTime.Kind is DateTimeKind.Local, the conversion is not performed.
                // If inputDateTime.Kind is DateTimeKind.Unspecified, the conversion is performed as if inputDateTime was universal time.



                displayDt = inputDatetime.ToLocalTime();
                datetimeString = displayDt.ToString(datetimePattern);
                Console.WriteLine("  ToLocalTime:     {0}, Kind = {1}", datetimeString, displayDt.Kind);
                // Convert inputDateTime to universal time and display the result. 
                // If inputDateTime.Kind is DateTimeKind.Utc, the conversion is not performed.
                // If inputDateTime.Kind is DateTimeKind.Local, the conversion is performed.
                // If inputDateTime.Kind is DateTimeKind.Unspecified, the conversion is performed as if inputDateTime was local time.




                displayDt = inputDatetime.ToUniversalTime();
                datetimeString = displayDt.ToString(datetimePattern);
                Console.WriteLine("  ToUniversalTime: {0}, Kind = {1}", datetimeString, displayDt.Kind);
                Console.WriteLine();

            }

            // Display the value and Kind property for DateTime.Now and DateTime.UtcNow.

            public static void DisplayNow(string title, DateTime inputDt)
            {

                string datetimeString = inputDt.ToString(datetimePattern);
                Console.WriteLine("{0} {1}, Kind = {2}", title, datetimeString, inputDt.Kind);

            }

        
        

            }
        }
    }

