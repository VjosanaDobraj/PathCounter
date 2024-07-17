using System;
using System.Collections.Generic;

class PathCounter
{
    static void Main(string[] args)
    {
        bool continueRunning = true;
        int X = 0, Y = 0;
        while (continueRunning)
        {
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("Enter destination X coordinate (0-1000):");
                string inputX = Console.ReadLine();

                if (int.TryParse(inputX, out X) && X >= 0 && X <= 1000)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer between 0 and 1000.");
                }
            }

            validInput = false; 

            while (!validInput)
            {
                Console.WriteLine("Enter destination Y coordinate (0-1000):");
                string inputY = Console.ReadLine();

                if (int.TryParse(inputY, out Y) && Y >= 0 && Y <= 1000)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer between 0 and 1000.");
                }
            }

            var paths = new List<string>();
            var memo = new Dictionary<(int, int, int, int), HashSet<string>>();

            FindPaths(X, Y, 0, 0, 0, 0, "", paths, memo);

            Console.WriteLine($"Number of valid paths: {paths.Count}");

            if (!(X == 0 && Y == 0))
            {
                Console.WriteLine("Routes for each valid path:");
                foreach (var route in paths)
                {
                    Console.WriteLine(route);
                }
            }
            else
            {
                Console.WriteLine("Explanation: There's exactly one valid way to get from (0, 0) to (0, 0),\r\nTake no steps");
            }

            Console.WriteLine("Press 1 to run again, Press 2 to stop:");
            string choice = Console.ReadLine();

            if (choice != "1")
            {
                continueRunning = false;
            }
        }
    }

    private static void FindPaths(int X, int Y, int currentX, int currentY, int eastStreak, int northStreak, string currentPath, List<string> paths, Dictionary<(int, int, int, int), HashSet<string>> memo)
    {
        if (currentX == X && currentY == Y)
        {
            paths.Add(currentPath);
            return; 
        }

        if (memo.ContainsKey((currentX, currentY, eastStreak, northStreak)))
        {
            foreach (var path in memo[(currentX, currentY, eastStreak, northStreak)])
            {
                paths.Add(currentPath + path);
            }
            return;
        }

        var validPaths = new HashSet<string>();

        if (currentX < X)
        {
            FindPaths(X, Y, currentX + 1, currentY, eastStreak + 1, 0, currentPath + "E", paths, memo);
        }

        if (currentY < Y )
        {
            FindPaths(X, Y, currentX, currentY + 1, 0, northStreak + 1, currentPath + "N", paths, memo);
        }

        memo[(currentX, currentY, eastStreak, northStreak)] = validPaths;

        foreach (var path in validPaths)
        {
            paths.Add(currentPath + path);
        }
    }
}
