using System.Text.RegularExpressions;

namespace Day04;

public class Solver {
    public int Part1 { get; }
    public int Part2 { get; }

    public Solver(string input) {
        using StreamReader file = new(input);
        while (file.ReadLine() is { } line) {
            Regex regex = new("([\\d]+)-([\\d]+),([\\d]+)-([\\d]+)");
            Match match = regex.Match(line);
            int a1 = int.Parse(match.Groups[1].Value);
            int a2 = int.Parse(match.Groups[2].Value);
            int b1 = int.Parse(match.Groups[3].Value);
            int b2 = int.Parse(match.Groups[4].Value);
            if (a1 >= b1 && a2 <= b2) {
                Part1++;
            } else if (b1>= a1 && b2 <= a2) {
                Part1++;
            }

            if (a2 >= b1 && b2 >= a1) {
                Part2++;
            }
        }
    }
}
