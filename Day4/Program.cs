class Solution
{
  public static int PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var intersectingPairs = input
      .Split("\n")
      .Where((line) => checkIfPairIsIntersecting(line.Split(",")[0], line.Split(",")[1]));

    return intersectingPairs.Count();
  }

  private static bool checkIfPairIsIntersecting(string first, string second)
  {
    var firstStart = Int16.Parse(first.Split("-")[0]);
    var firstEnd = Int16.Parse(first.Split("-")[1]);
    var secondStart = Int16.Parse(second.Split("-")[0]);
    var secondEnd = Int16.Parse(second.Split("-")[1]);

    return (firstStart <= secondStart && secondEnd <= firstEnd) || (secondStart <= firstStart && firstEnd <= secondEnd);
  }

  public static int PartTwo()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var overlappingPairs = input
      .Split("\n")
      .Where((line) => checkIfOverlapping(line.Split(",")[0], line.Split(",")[1]));

    return overlappingPairs.Count();
  }

  private static bool checkIfOverlapping(string first, string second)
  {
    var firstStart = Int16.Parse(first.Split("-")[0]);
    var firstEnd = Int16.Parse(first.Split("-")[1]);
    var secondStart = Int16.Parse(second.Split("-")[0]);
    var secondEnd = Int16.Parse(second.Split("-")[1]);

    var firstCond = ((firstStart >= secondStart) && (firstStart <= secondEnd)) || ((firstEnd >= secondStart) && (firstEnd <= secondEnd));
    var secondCond = ((secondStart >= firstStart) && (secondStart <= firstEnd)) || ((secondEnd >= firstStart) && (secondEnd <= firstEnd));

    return firstCond || secondCond;
  }

  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {Solution.PartOne()}");
    Console.WriteLine($"Part 2: {Solution.PartTwo()}");
  }
}