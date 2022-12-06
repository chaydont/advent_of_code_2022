using System.Text.RegularExpressions;

namespace Day05;

public class Solver {
    public string Part1 { get; }
    public string Part2 { get; }

    public Solver(string input) {
        List<LinkedList<char>> crates = new();
        List<Cargo.Action> actions = new();
        using (StreamReader file = new(input)) {
            int length = -1;
            bool initialized = false;
            while (file.ReadLine() is { } line) {
                if (length < 0) {
                    length = (line.Length + 1) / 4;
                    for (int i = 0; i < length; i++) {
                        crates.Add(new LinkedList<char>());
                    }
                }

                if (!initialized) {
                    for (int i = 0; i < length; i++) {
                        char crate = line[1 + i * 4];
                        if (crate == '1') {
                            initialized = true;
                            break;
                        } else if (crate != ' ') {
                            crates[i].AddFirst(crate);
                        }
                    }
                } else {
                    Regex regex = new("move ([\\d]+) from ([\\d]+) to ([\\d]+)");
                    if (regex.IsMatch(line)) {
                        GroupCollection groups = regex.Match(line).Groups;
                        actions.Add(new Cargo.Action(
                            int.Parse(groups[1].Value), 
                            int.Parse(groups[2].Value), 
                            int.Parse(groups[3].Value
                            )));
                    }
                }
            }
        }

        Cargo cargo9000 = new(crates);
        Cargo cargo9001 = new(crates);
        
        foreach (Cargo.Action action in actions) {
            cargo9000.ExecuteAction9000(action);
            cargo9001.ExecuteAction9001(action);
        }
        Part1 = cargo9000.GetTopCrates();
        Part2 = cargo9001.GetTopCrates();
    }
}
