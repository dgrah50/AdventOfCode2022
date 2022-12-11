using System;
class Solution
{
  public static int PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var cycles = 0;
    var x = 1;

    var xAtTwenties = new List<int>();
    
    foreach (var line in input.Split("\n"))
    {
      if (line.StartsWith("addx"))
      {
        var magnitude = int.Parse(line.Split(" ")[1]);
        cycles++;
        if ((cycles-20) % 40 == 0)
          xAtTwenties.Add(x * cycles);
        cycles++;
        if ((cycles-20) % 40 == 0)
          xAtTwenties.Add(x * cycles);
        x += magnitude;
      } else if (line.StartsWith("noop"))
      {
        cycles++;
        if ((cycles-20) % 40 == 0)
          xAtTwenties.Add(x * cycles);
      }
    }
    return xAtTwenties.Sum();
  }

  public static void PartTwo()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");
    
    var cycles = 0;
    var x = 1;


    
    foreach (var line in input.Split("\n"))
    {
      if (line.StartsWith("addx"))
      {
        var magnitude = int.Parse(line.Split(" ")[1]);
        cycles++;
        drawPoint(cycles, x);
        cycles++;
        drawPoint(cycles, x);
        x += magnitude;
      } else if (line.StartsWith("noop"))
      {
        cycles++;
        drawPoint(cycles, x);
      }
    }
  }

  private static void drawPoint(int cycles, int x)
  {
    
    if ((cycles % 40) == 0)
    {
      if ((x ==38) || (x ==39) || (x == 40))
      {
        Console.Write("#");
      }
      else
      {
        Console.Write(".");
      }
    }
    
    if (((cycles%40) == x) || ((cycles%40) == x+1) || ((cycles%40) == x+2))
    {
      Console.Write("#");
    }
    else
    {
      Console.Write(".");
    }

    if ((cycles % 40) == 0)
    {
      Console.WriteLine("");
    }
  }

  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {Solution.PartOne()}");
    Solution.PartTwo();
  }
}