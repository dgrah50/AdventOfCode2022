using System;
using System.Text.RegularExpressions;

class Solution
{
  static List<int> nodeSizes =  new List<int> ();
  
  public static int PartOne()
  {
    string input = File.ReadAllText(@"./input.txt");

    var currentNode = new FileNode("/", 0);
    foreach (var line in input.Split(Environment.NewLine))
    {
      if (line.StartsWith("$ ls"))
      {
        continue;
      }
      if (line.StartsWith("$ cd /"))
      {
        while (currentNode.Parent != null)
        {
          currentNode = currentNode.Parent;
        } 
        continue;
      }
      else if (line.StartsWith("$ cd"))
      {
        var directive = line.Split(" ")[2];
        
        if ((directive == "..") )
        {
          if (currentNode.Id == "/")
          {
            continue;
          }
          currentNode = currentNode.Parent;
        }
        else
        {
          if ((currentNode.Children.Any(kid => kid.Id == directive)))
          {
            currentNode = currentNode.Children.Find(kid=>kid.Id == directive);
          }
          else
          {
           // child does not exist yet 
           var newNode = new FileNode(directive, 0);
             currentNode.AddChild(
               newNode
             );
             currentNode = newNode;
          }
        }
      }
      else
      {
        var size = line.Split(" ")[0];
        var id = line.Split(" ")[1];
        if (size == "dir")
        {
          var newNode = new FileNode(id, 0);
            currentNode.AddChild(
              newNode
            );
        }
        else
        {
          currentNode.Size += Int32.Parse(size);
        }
      }
    }

    var total = 0;

    //get root node

    while (currentNode.Parent != null)
    {
      currentNode = currentNode.Parent;
    } 

    traverseTree(currentNode);

    return nodeSizes.Where(i => i <= 100000).Sum();
  }

  private static void traverseTree(FileNode node)
  {
    nodeSizes.Add(node.GetNodeSize());
    foreach (var child in node.Children)
    {
      traverseTree(child);
    }
  }

  public static int PartTwo()
  {
    string input = File.ReadAllText(@"./input.txt");

    var currentNode = new FileNode("/", 0);
    var total = 0;

    foreach (var line in input.Split(Environment.NewLine))
    {

      int number;
    
      bool success = int.TryParse(line.Split(" ")[0], out number);
      if (success)
      {
        total += number;
      }

      if (line.StartsWith("$ ls"))
      {
        continue;
      }
      if (line.StartsWith("$ cd /"))
      {
        while (currentNode.Parent != null)
        {
          currentNode = currentNode.Parent;
        } 
        continue;
      }
      else if (line.StartsWith("$ cd"))
      {
        var directive = line.Split(" ")[2];
        
        if ((directive == "..") )
        {
          if (currentNode.Id == "/")
          {
            continue;
          }
          currentNode = currentNode.Parent;
        }
        else
        {
          if ((currentNode.Children.Any(kid => kid.Id == directive)))
          {
            currentNode = currentNode.Children.Find(kid=>kid.Id == directive);
          }
          else
          {
            // child does not exist yet 
     
             
            var newNode = new FileNode(directive, 0);
            currentNode.AddChild(
              newNode
            );
            currentNode = newNode;
           
          }
        }
      }
      else
      {
        var size = line.Split(" ")[0];
        var id = line.Split(" ")[1];
        if (size == "dir")
        {
          var newNode = new FileNode(id, 0);
          currentNode.AddChild(
            newNode
          );
        }
        else
        {
          currentNode.Size += Int32.Parse(size);
        }
      }
    }
    
    //get root node

    while (currentNode.Parent != null)
    {
      currentNode = currentNode.Parent;
    } 

    traverseTree(currentNode);

    var currentSize = 70000000 - total;
    return nodeSizes.Where(i => (currentSize + i) > 30000000).Order().First();
  }
  static void Main(string[] args)
  {
    Console.WriteLine($"Part 1: {Solution.PartOne()}");
    Console.WriteLine($"Part 2: {Solution.PartTwo()}");
  }
}



class FileNode
{
  public string Id { get; set; }
  public int Size { get; set; }

  public FileNode Parent { get; set; }
  public List<FileNode> Children { get; set; }

  public FileNode(string id, int size)
  {
    this.Id = id;
    this.Size = size;
    this.Children = new List<FileNode>() { };
  }
  public int GetNodeSize()
  {
    return this.Size + this.GetChildSize();
  }
  public void AddChild(FileNode newChild)
  {
    newChild.Parent = this;
    this.Children.Add(newChild);
  }

  private int GetChildSize()
  {
    return this.Children.Select(child => child.GetNodeSize()).Sum();
  }
}