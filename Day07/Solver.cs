using System.Text.RegularExpressions;

namespace Day07;

public class Solver {
    public int Part1 { get; }
    public int Part2 { get; }

    private static List<string> GetDirectories(Stack<string> path) {
        List<string> result = new();
        string current = "";
        foreach (string dir in path.Reverse()) {
            current += dir;
            result.Add(current);
        }
        return result;
    }

    public Solver(string input) {

        Stack<string> currentPath = new();
        Dictionary<string, int> directories = new();
        
        using (StreamReader file = new(input)) {
            string? line = file.ReadLine();
            while (line is not null) {
                if (line.StartsWith("$ cd ")) {
                    string dir = line[5..];
                    switch (dir) {
                        case "..":
                            currentPath.Pop();
                            break;
                        case "/":
                            currentPath = new Stack<string>();
                            currentPath.Push("/");
                            break;
                        default:
                            currentPath.Push(dir);
                            break;
                    }

                    line = file.ReadLine();
                } else if (line.StartsWith("$ ls")) {
                    line = file.ReadLine();
                    while (line is not null && !line.StartsWith("$")) {
                        if (!line.StartsWith("dir")) {
                            Regex regex = new("(?<size>[\\d]+) .*");
                            int size = int.Parse(regex.Match(line).Groups["size"].Value);
                            foreach (string directory in GetDirectories(currentPath)) {
                                if (!directories.ContainsKey(directory)) {
                                    directories[directory] = size;
                                } else {
                                    directories[directory] += size;
                                }
                            }
                        }
                        line = file.ReadLine();
                    }
                }
            }
        }

        Part1 = directories.Where(x => x.Value <= 100_000).Sum(x => x.Value);

        int a = int.MaxValue;
        int requiredSpace = 30_000_000 - (70_000_000 - directories["/"]);
        foreach ((_, int size) in directories) {
            if (size > requiredSpace) {
                a = Math.Min(a, size);
            }
        }
        Part2 = a;
    }
}
