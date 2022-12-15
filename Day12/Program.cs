class Solution
{
    public static int PartOne()
    {
        string input = System.IO.File.ReadAllText(@"./input.txt");

        List<List<Char>> grid = parseGrid(input);

        Point startingPoint = getStartingPoint(grid);

        Point endPoint = getEndPoint(grid);

        var path = getPath(grid, startingPoint, endPoint);

        Console.WriteLine(startingPoint);
        Console.WriteLine(endPoint);

        return path.Count -1;
    }

    private static List<Point> getPath(List<List<char>> grid, Point startingPoint, Point endPoint)
    {
        Dictionary<Point,int> dist = new Dictionary<Point,int>();


        
        dist[startingPoint] = 0;

        List<Point> QContents = new List<Point>();
        PriorityQueue<Point, int> Q = new PriorityQueue<Point, int>();
        Q.Enqueue(startingPoint, dist[startingPoint]);
        QContents.Add(startingPoint);
        
        Dictionary<Point, Point> prev = new();

        while (Q.Count > 0)
        {
            Point currentPoint = Q.Dequeue();
            QContents.Remove(currentPoint);
            
            if (currentPoint.X == endPoint.X && currentPoint.Y == endPoint.Y)
            {
                break;
            }
            else
            {
                var ValidMoves = getValidMoves(currentPoint, grid);
                foreach (var validMove in ValidMoves)
                {
                    int proposedDist = dist[currentPoint] + 1;
                    
                    if (proposedDist < (dist.ContainsKey(validMove) ? dist[validMove] : int.MaxValue))
                    {
                        prev[validMove] = currentPoint;
                        dist[validMove] = proposedDist;
                        prev[validMove] = currentPoint;
                        if (!QContents.Contains(validMove))
                        {
                            Q.Enqueue(validMove,proposedDist);
                            QContents.Add(validMove);
                        }
                    }
                }
            }
        }

        var path = new List<Point>();

        Nullable<Point> holder = endPoint;
        if (prev.ContainsKey(endPoint) || (startingPoint.X == endPoint.X && startingPoint.Y == endPoint.Y))
        {
            while (holder is not null)
            {
                path.Add((Point)holder);
                holder = prev.ContainsKey((Point)holder) ? prev[(Point)holder] : null;
            }
        }
        
        return path;
    }


    private static List<Point> getValidMoves(Point basePoint, List<List<char>> grid)
    {
        List<Point> validMoves = new List<Point>();

        // Get up point
        Point upCoords = basePoint with { Y = basePoint.Y - 1 };
        if (isValidPoint(upCoords, grid,basePoint)) validMoves.Add(upCoords);

        // Get down point
        Point downCoords = basePoint with { Y = basePoint.Y + 1 };
        if (isValidPoint(downCoords, grid, basePoint)) validMoves.Add(downCoords);

        // Get left point
        Point leftCoords = basePoint with { X = basePoint.X - 1 };
        if (isValidPoint(leftCoords, grid, basePoint)) validMoves.Add(leftCoords);

        // Get right point
        Point rightCoords = basePoint with { X = basePoint.X + 1 };
        if (isValidPoint(rightCoords, grid, basePoint)) validMoves.Add(rightCoords);

        return validMoves;
    }

    private static bool isValidPoint(Point pt, List<List<char>> grid, Point basePoint)
    {
        var maxX = grid[0].Count - 1;
        var maxY = grid.Count - 1;


        if (pt.X < 0 || pt.Y < 0 || pt.X > maxX || pt.Y > maxY) return false;
        var ptChar = grid[pt.Y][pt.X];
        if (ptChar == 'S')
            ptChar = 'a';
        if (ptChar == 'E')
            ptChar = 'z';
        var basePointChar = grid[basePoint.Y][basePoint.X] + 0;
        if (basePointChar == 'S')
            basePointChar = 'a';
        if (basePointChar == 'E')
            basePointChar = 'z';

        
        return (ptChar + 0 - basePointChar + 0) <=1;
    }

    private static Point getStartingPoint(List<List<char>> grid)
    {
        return findLetterInGrid(grid, 'S');
    }

    private static Point getEndPoint(List<List<char>> grid)
    {
        return findLetterInGrid(grid, 'E');
    }

    private static Point findLetterInGrid(List<List<char>> grid, char needle)
    {
        int rowIndex = 0;
        foreach (var row in grid)
        {
            int colIndex = 0;
            foreach (var cell in row)
            {
                if (grid[rowIndex][colIndex] == needle)
                {
                    return new Point { X = colIndex, Y = rowIndex };
                }

                colIndex++;
            }

            rowIndex++;
        }

        return default;
    }

    private static List<Point> findAllLettersInGrid(List<List<char>> grid, char needle)
    {
        var result = new List<Point>();
        int rowIndex = 0;
        foreach (var row in grid)
        {
            int colIndex = 0;
            foreach (var cell in row)
            {
                if (grid[rowIndex][colIndex] == needle)
                {
                    result.Add(new Point { X = colIndex, Y = rowIndex });
                }

                colIndex++;
            }

            rowIndex++;
        }

        return result;
    }

    private static List<List<char>> parseGrid(string input)
    {
        List<List<char>> grid = new List<List<char>>();
        foreach (var line in input.Split("\n"))
        {
            grid.Add(line.ToCharArray().ToList());
        }

        return grid;
    }
    

    public static int PartTwo()
    {
        string input = System.IO.File.ReadAllText(@"./input.txt");
        List<List<Char>> grid = parseGrid(input);

        var aElevation = findAllLettersInGrid(grid,'a');
        var SElevation = findLetterInGrid(grid, 'S');
        
        aElevation.Add(SElevation);

        var min = int.MaxValue;
        
        Point endPoint = getEndPoint(grid);

        foreach (var startingPoint in aElevation)
        {
            var path = getPath(grid, startingPoint, endPoint);

            var pathLength = path.Count - 1;

            if (path.Count > 0)
                if (min > pathLength)
                    min = pathLength;
        }
            
        return min;
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