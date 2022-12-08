namespace Day06;

public class Solver {

    public int Part1 { get; }
    public int Part2 { get; }

    public Solver(string input) {

        string message;
        using (StreamReader file = new(input)) {
            message = file.ReadToEnd();
        }

        Part1 = GetStartOfSignal(message, 4);
        Part2 = GetStartOfSignal(message, 14);
    }

    private static int GetStartOfSignal(string message, int n) {
        LinkedList<char> lastChars = new();
        int i = 0;
        foreach (char c in message) {
            i++;
            lastChars.AddLast(c);
            if (lastChars.Count > n) {
                lastChars.RemoveFirst();
            }
            if (lastChars.Count == n && AllElementsAreDifferent(lastChars)) {
                break;
            }
        }
        return i;
    }

    private static bool AllElementsAreDifferent(IReadOnlyCollection<char> list) {
        return list.All(c => list.Count(current => current == c) <= 1);
    }
}
