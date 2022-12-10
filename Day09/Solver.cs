using System.Text.RegularExpressions;

namespace Day09;

public class Solver {

    public int Part1 { get; }
    public int Part2 { get; }

    private const int ROPE_LENGTH = 9;
    
    public Solver(string input) {

        Coord head = new(0, 0);
        List<Tail> rope = new();
        for (int i = 0; i < ROPE_LENGTH; i++) {
            rope.Add(new Tail(0, 0));
        }
        
        using StreamReader file = new(input);
        while (file.ReadLine() is { } line) {
            Regex regex = new("(?<direction>[\\w]) (?<n>[\\d]+)");
            Match match = regex.Match(line);
            string direction = match.Groups["direction"].Value;
            int n = int.Parse(match.Groups["n"].Value);
            for (int _ = 0; _ < n; _++) {
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

                Coord last = head;
                for (int i = 0; i < ROPE_LENGTH; i++) {
                    rope[i].Follow(last);
                    last = rope[i];
                }
            }
        }
        Part1 = rope[0].GetHistoryLength();
        Part2 = rope[ROPE_LENGTH - 1].GetHistoryLength();
    }
}

public class Coord {
    public int x;
    public int y;

    public Coord(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

public class Tail : Coord {

    private readonly HashSet<int> history;

    public Tail(int x, int y) : base(x, y) {
        history = new HashSet<int> { 0 };
    }

    public void Follow(Coord other) {
        if (Math.Abs(x - other.x) >= 2 || Math.Abs(y - other.y) >= 2) {
            if (x < other.x) {
                x++;
            } else if (x > other.x) {
                x--;
            }

            if (y < other.y) {
                y++;
            } else if (y > other.y) {
                y--;
            }
            history.Add(y * 10_000 + x);
        }
}
    
    public int GetHistoryLength() {
        return history.Count;
    }
}
