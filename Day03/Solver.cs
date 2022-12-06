namespace Day03;

public class Solver {

    public int Part1 { get; }
    public int Part2 { get; }

    public Solver(string input) {

        DateTime startingTime = DateTime.Now;
        Part1 = 0;
        Part2 = 0;
        List<HashSet<char>> group = new();
        using (StreamReader file = new(input)) {
            while (file.ReadLine() is { } line) {
                HashSet<char> start = CreateItemGroup(line[..(line.Length / 2)]);
                HashSet<char> end = CreateItemGroup(line[(line.Length / 2)..]);
                Part1 += start.Intersect(end).Sum(GetLetterScore);
                group.Add(new HashSet<char>(start.Union(end)));
                if (group.Count == 3) {
                    group[0].IntersectWith(group[1]);
                    group[0].IntersectWith(group[2]);
                    foreach (char letter in group[0]) {
                        Part2 += GetLetterScore(letter);
                    }
                    group = new List<HashSet<char>>();
                }
            }
        }
        
        Console.WriteLine($"Computing time: {(DateTime.Now - startingTime).TotalMilliseconds} ms");
    }

    private static HashSet<char> CreateItemGroup(string line) {
        HashSet<char> group = new();
        foreach (char letter in line) {
            group.Add(letter);
        }
        return group;
    }
    
    private static int GetLetterScore(char letter) {
        return letter is >= 'A' and <= 'Z' ? letter - 'A' + 27 : letter - 'a' + 1;
    }
}