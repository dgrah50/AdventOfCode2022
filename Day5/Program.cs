using System;
using System.Text.RegularExpressions;

class Solution
{
  public static string PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var charStacks = new Dictionary<int, List<char>>()
    {
    };

    foreach (var line in input.Split("\n"))
    {
      if (line.Contains("["))
      {
        ProcessBracketLine(line, charStacks);
      }

      if (line.Contains("move"))
      {
        var shouldReverse = true;
        ProcessMoveLine(line, shouldReverse, charStacks);
      }
    }

    return String.Concat(Range(1, charStacks.Keys.Count(), 1).Select(key => charStacks[key].First()));
  }


  public static string PartTwo()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");
    var charStacks = new Dictionary<int, List<char>>()
    {
    };

    foreach (var line in input.Split("\n"))
    {
      if (line.Contains("["))
      {
        ProcessBracketLine(line, charStacks);
      }

      if (line.Contains("move"))
      {
        var shouldReverse = false;
        ProcessMoveLine(line, shouldReverse, charStacks);
      }
    }

    return String.Concat(Range(1, charStacks.Keys.Count(), 1).Select(key => charStacks[key].First()));
  }
  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {Solution.PartOne()}");
    Console.WriteLine($"Part 2: {Solution.PartTwo()}");
  }

  private static void ProcessBracketLine(string line, Dictionary<int, List<char>> charStacks)
  {
    var charIndices = Range(2, line.Length - 1, 4);
    for (int i = 0; i < charIndices.Count(); i++)
    {
      var charToBeAdded = line[charIndices.Skip(i).First() - 1];
      if (char.IsLetter(charToBeAdded))
      {
        if (!charStacks.ContainsKey(i + 1))
        {
          charStacks.Add(i + 1, new List<char>()
          {
            line[charIndices.Skip(i).First() - 1]
          });
        }
        else
        {
          charStacks[i + 1].Add(line[charIndices.Skip(i).First() - 1]);
        }
      }
    }
  }

  private static void ProcessMoveLine(string line, bool shouldReverse, Dictionary<int, List<char>> charStacks)
  {
    Regex regex = new Regex(@"[0-9]+");
    MatchCollection numbers = regex.Matches(line);

    var numberToMove = Int16.Parse(numbers[0].Value);
    var stackFrom = Int16.Parse(numbers[1].Value);
    var stackTo = Int16.Parse(numbers[2].Value);
    if (shouldReverse)
    {
      charStacks[stackTo] = charStacks[stackFrom].Take(numberToMove).Reverse().Concat(
        charStacks[stackTo]
      ).ToList();
    }
    else
    {
      charStacks[stackTo] = charStacks[stackFrom].Take(numberToMove).Concat(
        charStacks[stackTo]
      ).ToList();
    }

    charStacks[stackFrom].RemoveRange(0, numberToMove);
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