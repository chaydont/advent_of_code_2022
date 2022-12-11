namespace Day11; 

public class Monkey {

    public readonly int Id;
    private readonly Operation operation;
    private readonly int indexTrue;
    private readonly int indexFalse;
    public readonly int test;
    
    private Monkey? neighbourTrue;
    private Monkey? neighbourFalse;

    private List<long> items;

    public int InspectedItems;

    public Monkey(int id, List<long> items, Operation operation, int test, int indexTrue, int indexFalse) {
        Id = id;
        this.items = items;
        this.test = test;
        this.operation = operation;
        this.indexTrue = indexTrue;
        this.indexFalse = indexFalse;
        InspectedItems = 0;
    }

    public void SetNeighbours(List<Monkey> monkeys) {
        neighbourTrue = monkeys[indexTrue];
        neighbourFalse = monkeys[indexFalse];
    }

    public void PlayTurn(bool verbose, int factor=-1) {
        if (verbose) Console.WriteLine($"Monkey {Id}:");
        foreach (long item in items) {
            if (verbose) Console.WriteLine($"  Monkey inspects an item with a worry level of {item}");
            long newItem = operation.Execute(item);
            newItem = factor == -1 ? newItem / 3 : newItem % factor;
            if (verbose) Console.WriteLine($"   Worry level is [{operation}] to {newItem}.");
            InspectedItems++;
            if (newItem % test == 0) {
                if (verbose) Console.WriteLine($"   Current worry level is divisible by {test}.");
                neighbourTrue!.Receive(newItem);
                if (verbose) Console.WriteLine($"   Item with worry level {newItem} is thrown to monkey {neighbourTrue.Id}.");
            } else {
                if (verbose) Console.WriteLine($"   Current worry level is not divisible by {test}.");
                neighbourFalse!.Receive(newItem);
                if (verbose) Console.WriteLine($"   Item with worry level {newItem} is thrown to monkey {neighbourFalse.Id}.");
            }
        }
        items = new List<long>();
    }

    private void Receive(long item) {
        items.Add(item);
    }

    public void PrintItems() {
        Console.Write($"Monkey {Id}: ");
        foreach (long item in items) {
            Console.Write(item + ", ");
        }
        Console.WriteLine();
    }
}
