using System;
using System.Text.RegularExpressions;

class Solution
{
  public static int PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    for (int i = 0; i < input.Length - 3; i++)
    {
      var subs = input.Substring(i, 4).ToCharArray();
      if (subs.Distinct().Count() == subs.Count())
      {
        Console.WriteLine($"{input.Substring(i, 4)}");
        return i + 4;
      }
    }
    return 0;
  }


  public static int PartTwo()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    for (int i = 0; i < input.Length - 13; i++)
    {
      var subs = input.Substring(i, 14).ToCharArray();
      if (subs.Distinct().Count() == subs.Count())
      {
        Console.WriteLine($"{input.Substring(i, 14)}");
        return i + 14;
      }
    }
    return 0;
  }
  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {Solution.PartOne()}");
    Console.WriteLine($"Part 2: {Solution.PartTwo()}");
  }



  private static IEnumerable<int> Range(int min, int max, int step)
  {
    int result = min;
    for (int i = 0; result < max; i++)
    {
      result = min + (step * i);
      yield return result;
    }
  }
}