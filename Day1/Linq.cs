using System;
using System.Linq;
class SolutionLinq
{
  public static int PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var caloriesPerElf =
    from elf in input.Split("\n\n")
    let calories = elf.Split("\n").Select(int.Parse).Sum()
    orderby calories descending
    select calories;

    return caloriesPerElf.First();
  }

  public static int PartTwo()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var caloriesPerElf =
    from elf in input.Split("\n\n")
    let calories = elf.Split("\n").Select(int.Parse).Sum()
    orderby calories descending
    select calories;

    return caloriesPerElf
      .Take(3)
      .Sum();
  }

  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {SolutionLinq.PartOne()}");
    Console.WriteLine($"Part 2: {SolutionLinq.PartTwo()}");
  }
}