using System.Text;
using System.Text.RegularExpressions;

namespace Day07;

public class Solver {
    public int Part1 { get; }
    public int Part2 { get; }

    private static string GetPath(Stack<string> currentPath, string dir) {
        StringBuilder builder = new();
        bool inside = false;
        foreach (string path in currentPath) {
            if (path == dir) {
                inside = true;
            }

            if (inside) {
                builder.Insert(0, path + "/");
            }
        }

        return builder.ToString();
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
                            foreach (string dir in currentPath) {
                                string path = GetPath(currentPath, dir);
                                if (!directories.ContainsKey(path)) {
                                    directories[path] = size;
                                } else {
                                    directories[path] += size;
                                }
                            }
                        }
                        line = file.ReadLine();
                    }
                }
            }
        }

        int a = 0;
        foreach ((string name, int size) in directories) {
            if (size <= 100_000) {
                a += size;
                Console.WriteLine($"name: {name} | size: {size} | total: {a}");
            }
        }
        
        Part1 = directories.Where(x => x.Value <= 100_000).Sum(x => x.Value);;
        Part2 = 0;
    }
}
