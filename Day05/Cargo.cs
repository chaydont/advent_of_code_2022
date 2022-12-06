using System.Text;

namespace Day05; 

public class Cargo {

    public class Action {

        public int start;
        public int end;
        public int n;
        
        public Action(int n, int start, int end) {
            this.n = n;
            this.start = start;
            this.end = end;
        }
    }

    private readonly List<Stack<char>> crates;

    public Cargo(List<LinkedList<char>> crates) {
        this.crates = new List<Stack<char>>();
        foreach (LinkedList<char> list in crates) {
            this.crates.Add(new Stack<char>(list));
        }
    }

    public void ExecuteAction9000(Action action) {
        for (int i = 0; i < action.n; i++) {
            MoveCrate(action.start, action.end);
        }
    }

    private void MoveCrate(int from, int to) {
        char crate = crates[from - 1].Pop();
        crates[to - 1].Push(crate);
    }
    
    public void ExecuteAction9001(Action action) {
        Stack<char> removedCrates = new();
        for (int i = 0; i < action.n; i++) {
            removedCrates.Push(crates[action.start - 1].Pop());
        }

        foreach (char crate in removedCrates) {
            crates[action.end - 1].Push(crate);
        }
    }

    public string GetTopCrates() {
        StringBuilder builder = new();
        foreach (Stack<char> stack in crates) {
            builder.Append(stack.Pop());
        }
        return builder.ToString();
    }

    public override string ToString() {
        StringBuilder builder = new();
        for (int h = crates.Select(stack => stack.Count).Max() - 1; h >= 0; h--) {
            foreach (List<char> stack in crates.Select(t => t.ToList())) {
                stack.Reverse();
                if (stack.Count > h) {
                    builder.Append($"[{stack[h]}] ");
                } else {
                    builder.Append("    ");
                }
            }

            builder.Append('\n');
        }

        for (int i = 1; i <= crates.Count; i++) {
            builder.Append($" {i}  ");
        }

        return builder.ToString();
    }
}
