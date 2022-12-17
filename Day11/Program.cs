using System.Text.RegularExpressions;

class Solution
{
  public static long PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var monkeyMap = new Dictionary<int, Monkey>();

    foreach (var monkeyBlock in input.Split("\n\n"))
    {
      var monkeyId = parseMonkey(monkeyBlock, out var result);
      monkeyMap[monkeyId] = result;
    }

    for (int i = 0; i < 20; ++i)
    {
      for (int j = 0; j < monkeyMap.Count; ++j)
      {
        monkeyMap[j].DoPass(monkeyMap);
      }
    }

    var topMonkeys = monkeyMap.Values.OrderByDescending(monkey => monkey.ItemsInspected);
    return topMonkeys.First().ItemsInspected * topMonkeys.Skip(1).First().ItemsInspected;
  }

  public static long PartTwo()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    var monkeyMap = new Dictionary<int, Monkey>();

    foreach (var monkeyBlock in input.Split("\n\n"))
    {
      var monkeyId = parseMonkey(monkeyBlock, out var result);
      monkeyMap[monkeyId] = result;
    }

    for (int i = 0; i < 10000; ++i)
    {
      for (int j = 0; j < monkeyMap.Count; ++j)
      {
        monkeyMap[j].DoPassV2(monkeyMap);
      }
    }

    var topMonkeys = monkeyMap.Values.OrderByDescending(monkey => monkey.ItemsInspected);
    return topMonkeys.First().ItemsInspected * topMonkeys.Skip(1).First().ItemsInspected;
  }

  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {Solution.PartOne()}");
    Console.WriteLine($"Part 2: {Solution.PartTwo()}");
  }

  private static int parseMonkey(string monkeyBlock, out Monkey result)
  {
    var monkeyId = 0;
    List<long> items = new List<long>();
    MonkeyOperation oper = new MonkeyOperation();
    var divisible = 0;

    var trueDest = 0;
    var falseDest = 0;

    foreach (var line in monkeyBlock.Split("\n"))
    {
      if (line.StartsWith("Monkey"))
        monkeyId = parseIntsFromLine(line).First();
      if (line.Contains("Starting"))
        items = parseIntsFromLine(line).Select(i => (long)(i)).ToList();
      if (line.Contains("Operation"))
        oper = parseOperation(line);
      if (line.Contains("If true"))
        trueDest = parseIntsFromLine(line).First();
      if (line.Contains("divisible"))
        divisible = parseIntsFromLine(line).First();

      if (line.Contains("If false"))
        falseDest = parseIntsFromLine(line).First();
    }

    result = new Monkey(monkeyId, items, oper, divisible, trueDest, falseDest);
    return monkeyId;
  }

  private static MonkeyOperation parseOperation(string line)
  {
    var op = line.Contains("*") ? MonkeyOperand.Multiply : MonkeyOperand.Add;
    if (line.Split(" ").Last() == "old")
    {
      return new MonkeyOperation
      {
        operand = MonkeyOperand.Self
      };
    }
    else
    {
      var magnitude = parseIntsFromLine(line).First();

      return new MonkeyOperation
      {
        magnitude = magnitude,
        operand = op
      };
    }
  }

  private static List<int> parseIntsFromLine(string line)
  {
    List<string> numbers = Regex.Split(line, @"\D+").Where(s => s.Length > 0).ToList();
    return numbers.Select(x => int.Parse(x)).ToList();
  }
}

enum MonkeyOperand
{
  Multiply,
  Add,
  Self
}

struct MonkeyOperation
{
  public MonkeyOperand operand;
  public int magnitude;
}

class Monkey
{
  public int Id { get; set; }

  public long ItemsInspected { get; set; }

  public List<long> Items { get; set; }

  public MonkeyOperation Operation { get; set; }

  public int Divisible { get; set; }

  public bool DivisibleBool { get; set; }

  public int FalseDest { get; set; }

  public int TrueDest { get; set; }

  public Monkey(int id, List<long> startingItems, MonkeyOperation operation, int divisible, int trueDest,
      int falseDest)
  {
    Id = id;
    Items = startingItems;
    Operation = operation;
    Divisible = divisible;
    TrueDest = trueDest;
    FalseDest = falseDest;
  }

  public void DoPass(Dictionary<int, Monkey> monkeyMap)
  {
    foreach (var item in Items)
    {
      try
      {
        checked
        {
          ItemsInspected++;
          var worryScore = item;

          if (Operation.operand == MonkeyOperand.Self)
            worryScore *= worryScore;
          if (Operation.operand == MonkeyOperand.Add)
            worryScore += Operation.magnitude;
          if (Operation.operand == MonkeyOperand.Multiply)
            worryScore *= Operation.magnitude;

          worryScore /= 3;

          if (worryScore % Divisible == 0)
          {
            monkeyMap[TrueDest].Items.Add(worryScore);
          }
          else
          {
            monkeyMap[FalseDest].Items.Add(worryScore);
          }

          Items = new List<long>();
        }
      }
      catch (OverflowException e)
      {
        Console.WriteLine(e.Message); // output: Arithmetic operation resulted in an overflow.
      }
    }
  }

  public void DoPassV2(Dictionary<int, Monkey> monkeyMap)
  {
    foreach (var item in Items)
    {
      try
      {
        checked
        {
          ItemsInspected++;
          var worryScore = item;

          var baseMod = monkeyMap.Aggregate(1, (a, kvp) => kvp.Value.Divisible * a);
          worryScore = worryScore %= baseMod;

          if (Operation.operand == MonkeyOperand.Self)
            worryScore *= worryScore;
          if (Operation.operand == MonkeyOperand.Add)
            worryScore += Operation.magnitude;
          if (Operation.operand == MonkeyOperand.Multiply)
            worryScore *= Operation.magnitude;

          if (worryScore % Divisible == 0)
          {
            monkeyMap[TrueDest].Items.Add(worryScore);
          }
          else
          {
            monkeyMap[FalseDest].Items.Add(worryScore);
          }

          Items = new List<long>();
        }
      }
      catch (OverflowException e)
      {
        Console.WriteLine(e.Message); // output: Arithmetic operation resulted in an overflow.
      }
    }
  }
}