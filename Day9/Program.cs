class Solution
{
  public static int PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    HashSet<Point> tailVisitedPoints = new HashSet<Point>();

    Point tailPos = new Point
    {
      X = 0,
      Y = 0
    };
    Point headPos = new Point
    {
      X = 0,
      Y = 0
    };
    tailVisitedPoints.Add(tailPos);

    foreach (var line in input.Split("\n"))
    {
      var holder = line.Split(' ');
      var direction = holder[0];
      var magnitude = int.Parse(holder[1]);

      for (int i = 0; i < magnitude; ++i)
      {
        var oldHeadPos = headPos;
        switch (direction)
        {
          case "L":
            headPos = headPos with { X = headPos.X - 1 };
            break;
          case "R":
            headPos = headPos with { X = headPos.X + 1 };
            break;
          case "U":
            headPos = headPos with { Y = headPos.Y + 1 };
            break;
          case "D":
            headPos = headPos with { Y = headPos.Y - 1 };
            break;
          default:
            throw new Exception("Undefined Direction");
        }

        if (!isTwoPointsTouching(headPos, tailPos))
        {
          tailPos = oldHeadPos;
        }

        tailVisitedPoints.Add(tailPos);
      }
    }

    return tailVisitedPoints.Count;
  }

  private static bool isTwoPointsTouching(Point headPos, Point tailPos)
  {
    return ((Math.Abs(headPos.X - tailPos.X) <= 1) && (Math.Abs(headPos.Y - tailPos.Y) <= 1));
  }

  public static int PartTwo()
  {
    string input = File.ReadAllText(@"./input.txt");

    HashSet<Point> tailVisitedPoints = new HashSet<Point>();

    var tailStack = new Dictionary<int, Point>();

    for (int i = 1; i <= 9; ++i)
    {
      tailStack[i] = new Point
      {
        X = 0,
        Y = 0
      };
    }

    Point headPos = new Point
    {
      X = 0,
      Y = 0
    };
    tailVisitedPoints.Add(tailStack[9]);

    foreach (var line in input.Split("\n"))
    {
      Console.WriteLine(line);
      var holder = line.Split(' ');
      var direction = holder[0];
      var magnitude = int.Parse(holder[1]);

      for (int i = 0; i < magnitude; ++i)
      {
        var oldHeadPos = headPos;
        switch (direction)
        {
          case "L":
            headPos = headPos with { X = headPos.X - 1 };
            break;
          case "R":
            headPos = headPos with { X = headPos.X + 1 };
            break;
          case "U":
            headPos = headPos with { Y = headPos.Y + 1 };
            break;
          case "D":
            headPos = headPos with { Y = headPos.Y - 1 };
            break;
          default:
            throw new Exception("Undefined Direction");
        }

        if (!isTwoPointsTouching(headPos, tailStack[1]))
        {
          tailStack = shiftTailStack(tailStack, oldHeadPos);
        }

        tailVisitedPoints.Add(tailStack[9]);
        // plotGrid(tailStack, headPos);
      }
    }

    return tailVisitedPoints.Count;
  }

  private static void plotGrid(Dictionary<int, Point> tailStack, Point headPos)
  {
    Console.WriteLine("***************** grid start *****************");
    for (int y = 10; y > -10; --y)
    {
      for (int x = -10; x < 10; ++x)
      {
        var printed = false;
        if ((x == 0) && (y == 0))
        {
          Console.Write("s");
          continue;
        }

        if ((headPos.X == x) && (headPos.Y == y))
        {
          Console.Write("H");
          printed = true;
        }
        else
        {
          for (int i = 1; i <= 9; ++i)
          {
            if ((tailStack[i].X == x) && (tailStack[i].Y == y))
            {
              Console.Write(i);
              printed = true;
              break;
            }
          }
        }

        if (!printed)
          Console.Write('.');
      }

      Console.WriteLine("");
    }
  }

  private static Dictionary<int, Point> shiftTailStack(Dictionary<int, Point> tailStack, Point OldHeadPos)
  {
    var newTailStack = tailStack;
    newTailStack[1] = OldHeadPos;


    for (int i = 2; i <= 9; ++i)
    {
      if (!isTwoPointsTouching(newTailStack[i], newTailStack[i - 1]))
      {
        var newPoint = new Point();
        if (Math.Abs(newTailStack[i].X - newTailStack[i - 1].X) >= 2)
        {
          newPoint.X = newTailStack[i].X + Math.Sign(newTailStack[i - 1].X - newTailStack[i].X);
          if (Math.Abs(newTailStack[i].Y - newTailStack[i - 1].Y) >= 1)
          {
            newPoint.Y = newTailStack[i].Y + Math.Sign(newTailStack[i - 1].Y - newTailStack[i].Y);
            newTailStack[i] = newPoint;
            continue;
          }
        }
        else
        {
          newPoint.X = newTailStack[i].X;
        }

        if (Math.Abs(newTailStack[i].Y - newTailStack[i - 1].Y) >= 2)
        {
          newPoint.Y = newTailStack[i].Y + Math.Sign(newTailStack[i - 1].Y - newTailStack[i].Y);
          if (Math.Abs(newTailStack[i].X - newTailStack[i - 1].X) >= 1)
          {
            newPoint.X = newTailStack[i].X + Math.Sign(newTailStack[i - 1].X - newTailStack[i].X);
            newTailStack[i] = newPoint;
            continue;
          }
        }
        else
        {
          newPoint.Y = newTailStack[i].Y;
        }

        newTailStack[i] = newPoint;
      }
      else
      {
        newTailStack[i] = tailStack[i];
      }
    }

    return newTailStack;
  }

  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {Solution.PartOne()}");
    Console.WriteLine($"Part 2: {Solution.PartTwo()}");
  }
}

public struct Point
{
  public int X { get; set; }
  public int Y { get; set; }
}