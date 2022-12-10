class Solution
{
  public static int PartOne()
  {
    string[] lines = System.IO.File.ReadAllLines(@"./input.txt");
    int max = 0;
    int accumulator = 0;

    foreach (string line in lines)
    {
      int number;
      bool isParsable = Int32.TryParse(line, out number);
      if (isParsable)
      {
        accumulator += number;
        if (accumulator > max)
          max = accumulator;
      }
      else
        accumulator = 0;
    }
    return max;
  }

  public static int PartTwo()
  {
    string[] lines = System.IO.File.ReadAllLines(@"./input.txt");

    int accumulator = 0;

    List<int> elvWeights = new List<int>();

    foreach (string line in lines)
    {
      int number;
      bool isParsable = Int32.TryParse(line, out number);
      if (isParsable)
      {
        accumulator = accumulator + number;
      }
      else
      {
        elvWeights.Add(accumulator);
        accumulator = 0;
      }
    }

    elvWeights.Sort();
    elvWeights.Reverse();

    int top3 = elvWeights[0] + elvWeights[1] + elvWeights[2];

    return top3;
  }

  // static void Main(string[] args)
  // {
  //   Console.WriteLine($"Part 1: {Solution.PartOne()}");
  //   Console.WriteLine($"Part 2: {Solution.PartTwo()}");
  // }
}