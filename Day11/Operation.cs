namespace Day11;

public class Operation {

    private readonly int value;
    private readonly char type;

    public Operation(string operation) {
        if (operation.StartsWith("* old")) {
            type = '^';
            value = 2;
        } else if (operation.StartsWith('+')) {
            type = '+';
            value = int.Parse(operation[2..]);
        } else if (operation.StartsWith('*')) {
            type = '*';
            value = int.Parse(operation[2..]);
        }
    }

    public long Execute(long x) {
        return type switch {
            '+' => x + value,
            '*' => x * value,
            _ => x * x
        };
    }

    public override string ToString() {
        return type + value.ToString();
    }
}
