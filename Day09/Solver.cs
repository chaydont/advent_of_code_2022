using System.Text.RegularExpressions;

namespace Day09;

public class Solver {

    public int Part1 { get; }
    public int Part2 { get; }

    public Solver(string input) {

        Coord head = new(0, 0);
        Coord tail = new(0, 0);

        HashSet<Coord> a = new();
        
        using StreamReader file = new(input);
        while (file.ReadLine() is { } line) {
            Regex regex = new("(?<direction>[\\w]) (?<n>[\\d]+)");
            Match match = regex.Match(line);
        }
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
