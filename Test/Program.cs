// See https://aka.ms/new-console-template for more information

using Shouldly;

class Program
{
    static void Main()
    {
        var array = new List<int>{1, 2, 3, 4, 5, 6, 7, 8};
        var res1 = array.ShouldContain(x => x > 90);
        var res2 = array.ShouldContain(x => x > 3, 10);
        var res3 = array.ShouldContainSingle(x => x > 7);
    }
}