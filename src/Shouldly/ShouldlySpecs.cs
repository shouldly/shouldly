using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shouldly
{
    [ShouldlyMethods]
    public static class ShouldlySpecs
    {
        public static void Spec(this string spec, Action test)
        {
            string message = "";

            try
            {
                test();
                return;
            }
            catch (ChuckedAWobbly e)
            {
                message = e.Message.Replace(
                    string.Format("\"{0}\".Spec(() => ", spec),
                    "    ");
            }
            catch (Exception e)
            {
                message = string.Format(
@"{0}: {1}",
                    e.GetType().ToString(),
                    e.Message);
            }

            throw new ChuckedAWobbly(string.Format(
@"{0}:
{1}",
                spec,
                message));
        }
    }
}
