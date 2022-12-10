using System;
using System.Linq;
class SolutionLinq
{
  public static int PartOne()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    List<List<int>> grid = new List<List<int>>();

    var index = 0;
    foreach (var line in input.Split("\n"))
    {
      grid.Add(line.ToCharArray().Select(ch => ch - '0').ToList()) ;
      index++;
    }

    List<List<int>> transposedGrid = Transpose(grid);
    
    var maxX = grid[0].Count -1;
    var maxY = grid.Count -1 ;

    var visibleCount = 0;
    var rowIndex = 0;
    var colIndex = 0;
    
    foreach (var row in grid)
    {
      if (rowIndex == 0 || rowIndex == maxY)
      {
        rowIndex++;
        continue;
      }
      
      colIndex = 0;
      foreach (var cell in row)
      {
        var x = grid[rowIndex][colIndex];
        if (colIndex == 0 || colIndex == maxY)
        {
          colIndex++;
          continue;
        }
        if (isVisible(rowIndex, colIndex, grid, transposedGrid))
          visibleCount++;

        colIndex++;
      }
      rowIndex++;
    }

    return visibleCount + (99 * 2) + (99 * 2) - 4;
  }

  private static bool isVisible(int rowIndex, int colIndex, List<List<int>> grid, List<List<int>> transposedGrid)
  {
    var row = grid[rowIndex];
    var column = transposedGrid[colIndex];

    var cell = grid[rowIndex][colIndex];
    
    //Check row visibility
    var beforeRow = row.Take(colIndex).ToList();
    var afterRow = row.Skip(colIndex+1).ToList();

    var beforeRowMaxIndex = beforeRow.FindIndex(c => c >= cell);
    var afterRowMaxIndex = afterRow.FindIndex(c => c >= cell);

    if ((beforeRowMaxIndex == -1) || (afterRowMaxIndex == -1))
      return true;

    //Check col visibility
    var beforeCol = column.Take(rowIndex).ToList();
    var afterCol = column.Skip(rowIndex+1).ToList();

    var beforeColMaxIndex = beforeCol.FindIndex(c => c >= cell);
    var afterColMaxIndex = afterCol.FindIndex(c => c >= cell);

    if ((beforeColMaxIndex == -1) || (afterColMaxIndex == -1))
      return true;

    return false;
  }
  
  private static int getScenicScore(int rowIndex, int colIndex, List<List<int>> grid, List<List<int>> transposedGrid)
  {
    var row = grid[rowIndex];
    var column = transposedGrid[colIndex];

    var cell = grid[rowIndex][colIndex];
    
    //Check row scenic score
    var beforeRow = row.Take(colIndex).ToList();
    beforeRow.Reverse();
    var afterRow = row.Skip(colIndex+1).ToList();
    
    var beforeRowScenicScore_ = beforeRow.TakeWhile(c => c < cell);
    var beforeRowScenicScore = (beforeRowScenicScore_.Count() < beforeRow.Count) ? beforeRowScenicScore_.Count()+1 : beforeRowScenicScore_.Count();
    
    var afterRowScenicScore_ = afterRow.TakeWhile(c => c < cell);
    var afterRowScenicScore = (afterRowScenicScore_.Count() < afterRow.Count) ? afterRowScenicScore_.Count()+1 : afterRowScenicScore_.Count();

    //Check col scenic score
    var beforeCol = column.Take(rowIndex).ToList();
    beforeCol.Reverse();
    var afterCol = column.Skip(rowIndex+1).ToList();

    var beforeColScenicScore_ = beforeCol.TakeWhile(c => c < cell);
    var beforeColScenicScore = (beforeColScenicScore_.Count() < beforeCol.Count) ? beforeColScenicScore_.Count()+1 : beforeColScenicScore_.Count();
    
    var afterColScenicScore_ = afterCol.TakeWhile(c => c < cell);
    var afterColScenicScore = (afterColScenicScore_.Count() < afterCol.Count) ? afterColScenicScore_.Count()+1 : afterColScenicScore_.Count();

    return beforeRowScenicScore * afterRowScenicScore * beforeColScenicScore * afterColScenicScore;
  }

  public static List<List<T>> Transpose<T>(List<List<T>> lists)
  {
    var longest = lists.Any() ? lists.Max(l => l.Count) : 0;
    List<List<T>> outer = new List<List<T>>(longest);
    for (int i = 0; i < longest; i++)
      outer.Add(new List<T>(lists.Count));
    for (int j = 0; j < lists.Count; j++)
    for (int i = 0; i < longest; i++)
      outer[i].Add(lists[j].Count > i ? lists[j][i] : default(T));
    return outer;
  }
  public static int PartTwo()
  {
    string input = System.IO.File.ReadAllText(@"./input.txt");

    List<List<int>> grid = new List<List<int>>();

    var index = 0;
    foreach (var line in input.Split("\n"))
    {
      grid.Add(line.ToCharArray().Select(ch => ch - '0').ToList()) ;
      index++;
    }

    List<List<int>> transposedGrid = Transpose(grid);
    
    var maxX = grid[0].Count -1;
    var maxY = grid.Count -1 ;

    var rowIndex = 0;
    var colIndex = 0;

    var maxScenicScore = 0;
    var currScenicScore = 0;
    
    foreach (var row in grid)
    {
      if (rowIndex == 0 || rowIndex == maxY)
      {
        rowIndex++;
        continue;
      }
      
      colIndex = 0;
      foreach (var cell in row)
      {
        var x = grid[rowIndex][colIndex];
        if (colIndex == 0 || colIndex == maxX)
        {
          colIndex++;
          continue;
        }

        currScenicScore = getScenicScore(rowIndex,colIndex,grid,transposedGrid);

        if (currScenicScore > maxScenicScore)
          maxScenicScore = currScenicScore;

        colIndex++;
      }
      rowIndex++;
    }

    return maxScenicScore;
  }

  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {SolutionLinq.PartOne()}");
    Console.WriteLine($"Part 2: {SolutionLinq.PartTwo()}");
  }
}