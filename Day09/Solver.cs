using System.Text.RegularExpressions;

namespace Day09;

public class Solver {

    public int Part1 { get; }
    public int Part2 { get; }

    public Solver(string input) {

        Coord head = new(0, 0);
        Tail tail = new(0, 0);

        HashSet<Coord> a = new();
        
        using StreamReader file = new(input);
        while (file.ReadLine() is { } line) {
            Regex regex = new("(?<direction>[\\w]) (?<n>[\\d]+)");
            Match match = regex.Match(line);
            string direction = match.Groups["direction"].Value;
            int n = int.Parse(match.Groups["n"].Value);
            
            Console.WriteLine(line);
            for (int i = 0; i < n; i++) {
                switch (direction) {
                    case "U":
                        head.y++;
                        break;
                    case "D":
                        head.y--;
                        break;
                    case "L":
                        head.x--;
                        break;
                    case "R":
                        head.x++;
                        break;
                }
                tail.Follow(head);
                Console.WriteLine($"{head.x} - {head.y}");
                Console.WriteLine($"{tail.x} - {tail.y}");
                Console.WriteLine();
            }
        }
        Part1 = tail.GetHistoryLength();
    }
}

public class Coord {
    public int x;
    public int y;

    public Coord(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public int Distance(Coord other) {
        return Math.Max(Math.Abs(other.x - x), Math.Abs(other.y - y));
    }
}

public class Tail : Coord {

    private readonly HashSet<int> history;

    public Tail(int x, int y) : base(x, y) {
        history = new HashSet<int>();
    }

    public void Follow(Coord other) {
        int distance = Distance(other);
        if (distance > 1) {
            if (x - other.x == distance) {
                x--;
            } else if (other.x - x == distance) {
                x++;
            }
            if (y - other.y == distance) {
                y--;
            } else if (other.y - y == distance) {
                y++;
            }
            history.Add(y * 10_000 + x);
        }
    }
    
    public int GetHistoryLength() {
        return history.Count;
    }
}
