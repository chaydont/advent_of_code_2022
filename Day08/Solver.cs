namespace Day08;

public class Solver {
    public int Part1 { get; }
    public int Part2 { get; }

    public Solver(string input) {
        int[,]? grid = null;
        int i = 0;
        using (StreamReader file = new(input)) {
            while (file.ReadLine() is { } line) {
                grid ??= new int[line.Length, line.Length];
                for (int j = 0; j < line.Length; j++) {
                    grid[i, j] = line[j] - '0';
                }
                i++;
            }
        }
        
        Part1 = 0;
        Part2 = 0;
        for (int y = 0; y < grid!.GetLength(0); y++) {
            for (int x = 0; x < grid.GetLength(1); x++) {
                if (treeIsVisible(grid, x, y)) {
                    Part1++;
                }
                Part2 = Math.Max(Part2, treeScore(grid, x, y));
            }
        }
    }

    private static Tuple<bool, int> treeVisibilityUp(int[,] grid, int x, int y) {
        int treeHeight = grid[y, x];
        bool visible = true;
        int treesVisible = 0;
        for (int up = y - 1; up >= 0; up--) {
            treesVisible++;
            if (grid[up, x] >= treeHeight) {
                visible = false;
                break;
            }
        }
        return new Tuple<bool, int>(visible, treesVisible);
    }
    
    private static Tuple<bool, int> treeVisibilityDown(int[,] grid, int x, int y) {
        int treeHeight = grid[y, x];
        bool visible = true;
        int treesVisible = 0;
        for (int down = y + 1; down < grid.GetLength(0); down++) {
            treesVisible++;
            if (grid[down, x] >= treeHeight) {
                visible = false;
                break;
            }
        }
        return new Tuple<bool, int>(visible, treesVisible);
    }
    
    private static Tuple<bool, int> treeVisibilityLeft(int[,] grid, int x, int y) {
        int treeHeight = grid[y, x];
        bool visible = true;
        int treesVisible = 0;
        for (int left = x - 1; left >= 0; left--) {
            treesVisible++;
            if (grid[y, left] >= treeHeight) {
                visible = false;
                break;
            }
        }
        return new Tuple<bool, int>(visible, treesVisible);
    }
    
    private static Tuple<bool, int> treeVisibilityRight(int[,] grid, int x, int y) {
        int treeHeight = grid[y, x];
        bool visible = true;
        int treesVisible = 0;
        for (int right = x + 1; right < grid.GetLength(1); right++) {
            treesVisible++;
            if (grid[y, right] >= treeHeight) {
                visible = false;
                break;
            }
        }
        return new Tuple<bool, int>(visible, treesVisible);
    }

    private static bool treeIsVisible(int[,] grid, int x, int y) {
        return treeVisibilityUp(grid, x, y).Item1
            || treeVisibilityDown(grid, x, y).Item1
            || treeVisibilityLeft(grid, x, y).Item1
            || treeVisibilityRight(grid, x, y).Item1;
    }

    private static int treeScore(int[,] grid, int x, int y) {
        return treeVisibilityUp(grid, x, y).Item2
               * treeVisibilityDown(grid, x, y).Item2
               * treeVisibilityLeft(grid, x, y).Item2
               * treeVisibilityRight(grid, x, y).Item2;
    }
    
    private static void printSquare(int[,] grid) {
        for (int y = 0; y < grid.GetLength(0); y++) {
            for (int x = 0; x < grid.GetLength(1); x++) {
                Console.Write(grid[y, x]);
            }
            Console.WriteLine();
        }
    }
}