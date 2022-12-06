
int GetScore(string line, bool part2=false) {
    string[] plan = line.Split(' ');
    return (plan[0], plan[1], part2) switch {
        ("A", "Y", false) => 8,
        ("B", "Z", false) => 9,
        ("C", "X", false) => 7,
        ("A", "X", false) => 4,
        ("B", "Y", false) => 5,
        ("C", "Z", false) => 6,
        ("A", "Z", false) => 3,
        ("B", "X", false) => 1,
        ("C", "Y", false) => 2,
        
        ("A", "Y", true) => 4,
        ("B", "Z", true) => 9,
        ("C", "X", true) => 2,
        ("A", "X", true) => 3,
        ("B", "Y", true) => 5,
        ("C", "Z", true) => 7,
        ("A", "Z", true) => 8,
        ("B", "X", true) => 1,
        ("C", "Y", true) => 6,
        _ => 0
    };
}

int Part1() {
    using StreamReader file = new("input.txt");
    int score = 0;
    while (file.ReadLine() is { } line) {
        score += GetScore(line);
    }  
    file.Close();
    return score;
}

int Part2() {
    using StreamReader file = new("input.txt");
    int score = 0;
    while (file.ReadLine() is { } line) {
        score += GetScore(line, true);
    }  
    file.Close();
    return score;
}

Console.WriteLine($"Part 1: {Part1()}");
Console.WriteLine($"Part 2: {Part2()}");