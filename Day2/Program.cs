class Solution
{
  public static int PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    return input.Split("\n").Select(line => CalculateLineScorePart1(line)).Sum();
  }

  public static int PartTwo()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    return input.Split("\n").Select(line => CalculateLineScorePart2(line)).Sum();
  }

  private static int CalculateLineScorePart1(string line)
  {

    var score = 0;

    var opponentMove = line[0];
    var ourMove = line[2];

    var MoveScores = new Dictionary<char, int>()
   {
    {'X', 1},
    {'Y', 2},
    {'Z', 3},
   };

    Func<char, char, bool> isDraw = (opponentMove, ourMove) => ((opponentMove == 'A' && ourMove == 'X') || (opponentMove == 'B' && ourMove == 'Y') || (opponentMove == 'C' && ourMove == 'Z'));

    Func<char, char, bool> isWin = (opponentMove, ourMove) => ((opponentMove == 'A' && ourMove == 'Y') || (opponentMove == 'B' && ourMove == 'Z') || (opponentMove == 'C' && ourMove == 'X'));


    if (isDraw(opponentMove, ourMove))
      score += 3;
    else if (isWin(opponentMove, ourMove))
      score += 6;

    score += MoveScores[ourMove];
    return score;
  }

  private static int CalculateLineScorePart2(string line)
  {

    var score = 0;

    var opponentMove = line[0];
    var gameResult = line[2];

    var gameScore = new Dictionary<char, int>()
   {
    { 'X', 0},
    { 'Y', 3},
    { 'Z', 6},
   };

    score += ScoreOfNeededMove[opponentMove][gameResult];
    score += gameScore[gameResult];

    return score;
  }

  public static Dictionary<char, Dictionary<char, int>> ScoreOfNeededMove =
    new Dictionary<char, Dictionary<char, int>>()
    {
      { 'A',
      new Dictionary<char, int>()
      {
        { 'X', 3}, //lose, scissors
        { 'Y', 1}, //draw, rock
        { 'Z', 2}, //win, paper
      }},
      {'B',
      new Dictionary<char, int>()
      {
        { 'X', 1}, //lose, rock
        { 'Y', 2}, //draw, paper
        { 'Z', 3}, //win, scissors
      }},
      { 'C',
      new Dictionary<char, int>()
      {
        { 'X', 2}, //lose, paper
        { 'Y', 3}, //draw, scissors
        { 'Z', 1}, //win, rock
      }},
    };

  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {Solution.PartOne()}");
    Console.WriteLine($"Part 2: {Solution.PartTwo()}");
  }
}

// Notes: there are some clever solutions that use chars and arithmetic to generate numbers using the ASCII values.
// Another option is to write the solution to the first part using the dict of dicts
// Alternatively, just write out all of the combos, but that is boring.