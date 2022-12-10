class Solution
{
  public static int PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var priority =
    from backpack in input.Split("\n")
    let duplicatedLetter = backpack[0..(backpack.Length / 2)].Intersect(backpack[(backpack.Length / 2)..^0]).First()
    select (duplicatedLetter + 0 < 91) ? (duplicatedLetter - 38) : (duplicatedLetter - 96);

    return priority.Sum();
  }

  public static int PartTwo()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var elfGroupScore = input
      .Split("\n")
      .Chunk(3)
      .Select(chunk => (chunk[0].Intersect(chunk[1]).Intersect(chunk[2])).First())
      .Select(duplicatedLetter => (duplicatedLetter + 0 < 91) ? (duplicatedLetter - 38) : (duplicatedLetter - 96));

    return elfGroupScore.Sum();
  }
  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {Solution.PartOne()}");
    Console.WriteLine($"Part 2: {Solution.PartTwo()}");
  }
}