using System.Text.RegularExpressions;

namespace Day11;

public class Solver {

    public long Part1 { get; }
    public long Part2 { get; }

    public Solver(string input) {

        using StreamReader file = new(input);
        string inputText = file.ReadToEnd();
        


        Part1 = DoPart1(inputText);
        Part2 = DoPart2(inputText);
    }

    private static int DoPart1(string input) {
        List<Monkey> monkeys = ParseInput(input);
        for (int i = 0; i < 20; i++) {
            foreach (Monkey monkey in monkeys) {
                monkey.PlayTurn(false);
            }
        }

        return monkeys.Select(m => m.InspectedItems).OrderByDescending(m => m).Take(2).Aggregate(1, (c, i) => c * i);
    }
    
    private static long DoPart2(string input) {
        List<Monkey> monkeys = ParseInput(input);
        int factor = monkeys.Aggregate(1, (c, m) => c * m.test);
        for (int i = 0; i < 10000; i++) {
            foreach (Monkey monkey in monkeys) {
                monkey.PlayTurn(false, factor);
            }
        }

        long result = 1;
        foreach (long i in monkeys.Select(m => m.InspectedItems).OrderByDescending(m => m).Take(2)) {
            result *= i;
        }
        return result;
    }

    private static List<Monkey> ParseInput(string input) {
        List<Monkey> monkeys = new();
        Regex regex = new("Monkey (?<id>\\d+):[\n\r]+" + "  Starting items: (?<items>.+)[\n\r]+" +
                          "  Operation: new = old (?<operation>.+)[\n\r]+" +
                          "  Test: divisible by (?<test>\\d+)[\n\r]+" +
                          "    If true: throw to monkey (?<indexTrue>\\d+)[\n\r]+" +
                          "    If false: throw to monkey (?<indexFalse>\\d+)");
        foreach (Match match in regex.Matches(input)) {
            int id = int.Parse(match.Groups["id"].Value);
            List<long> items = match.Groups["items"].Value.Split(", ").Select(long.Parse).ToList();
            Operation operation = new(match.Groups["operation"].Value);
            int test = int.Parse(match.Groups["test"].Value);
            int indexTrue = int.Parse(match.Groups["indexTrue"].Value);
            int indexFalse = int.Parse(match.Groups["indexFalse"].Value);
            monkeys.Add(new Monkey(id, items, operation, test, indexTrue, indexFalse));
        }
        foreach (Monkey monkey in monkeys) {
            monkey.SetNeighbours(monkeys);
        }
        return monkeys;
    }

}
