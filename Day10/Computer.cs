namespace Day10; 

public class Computer {

    private List<List<bool>> screen;
    private List<bool> currentLine;
    private int cycle;
    private int X = 1;
    private int part1;
    private string input;
    
    public Computer(string input) {
        screen = new List<List<bool>>();
        GoToNextLine();
        this.input = input;
    }

    public void Execute() {
        using StreamReader file = new(input);
        while (file.ReadLine() is { } line) {
            ExecuteLine(line);
        }
    }

    private void ExecuteLine(string line) {
        Clock();
        if (line.StartsWith("addx")) {
            Clock();
            X += int.Parse(line[5..]);
        }
    }

    private void Clock() {
        currentLine.Add(Math.Abs(currentLine.Count - X) < 2);
        cycle++;
        if (cycle % 40 == 0) {
            GoToNextLine();
        }
        if (new[] {20, 60, 100, 140, 180, 220}.Contains(cycle)) {
            part1 += X * cycle;
        }
    }

    private void GoToNextLine() {
        currentLine = new List<bool>();
        screen.Add(currentLine);
    }
    
    public void DisplayScreen() {
        foreach (List<bool> line in screen) {
            foreach (bool pixel in line) {
                Console.Write(pixel ? "##" : "  ");
            }
            Console.WriteLine();
        }
    }

    public int GetPart1() {
        return part1;
    }
}
