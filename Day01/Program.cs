using StreamReader file = new("input.txt");

List<int> elves = new();
int current = 0;
while (file.ReadLine() is { } ln) {
    if (ln == "") {
        elves.Add(current);
        current = 0;
    } else {
        current += int.Parse(ln);
    }
}  
file.Close();

elves.Sort();

Console.WriteLine($"Part 1: {elves.Last()}");
Console.WriteLine($"Part 2: {elves.TakeLast(3).Sum()}");