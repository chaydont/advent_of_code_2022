using Day10;

Computer computer = new("input.txt");

computer.Execute();

Console.WriteLine($"Part 1: {computer.GetPart1()}");
Console.WriteLine($"Part 2:");
computer.DisplayScreen();
