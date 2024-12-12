using System;
using System.Linq;

class SudokuGame
{
    private int[,] puzzle;
    private int[,] solution;
    private bool[,] initialCells;
    private Random random;
    private string[] congratsMessages = {
        "Wow, you're a Sudoku master! 🏆",
        "Incredible problem-solving skills! You nailed it! 🌟",
        "Brilliant work! Your logical thinking is top-notch! 👏",
        "Congratulations! You've conquered the Sudoku challenge! 🎉",
        "Mathematical genius alert! Perfect solve! 🧠",
        "You just solved a puzzle that would make Einstein proud! 🤯",
        "Sudoku champion! Your mind is sharper than a ninja's sword! 🥷"
    };

    public SudokuGame()
    {
        random = new Random();
        GenerateRandomPuzzle();
    }


    private void GenerateRandomPuzzle()
    {
        // Create a solved Sudoku grid
        solution = new int[9, 9];
        puzzle = new int[9, 9];
        initialCells = new bool[9, 9];

        // Generate a fully solved Sudoku grid
        GenerateSolvedGrid(solution);

        // Create a puzzle by removing some numbers
        Array.Copy(solution, puzzle, solution.Length);
        RemoveNumbers();
    }

    private void GenerateSolvedGrid(int[,] grid)
    {
        // Basic backtracking algorithm to generate a solved Sudoku grid
        SolveGrid(grid, 0, 0);
    }

    private bool SolveGrid(int[,] grid, int row, int col)
    {
        if (row == 9)
            return true;

        if (col == 9)
            return SolveGrid(grid, row + 1, 0);

        // Create a randomized list of numbers
        int[] numbers = Enumerable.Range(1, 9).OrderBy(x => random.Next()).ToArray();

        foreach (int num in numbers)
        {
            if (IsValidMove(grid, row, col, num))
            {
                grid[row, col] = num;

                if (SolveGrid(grid, row, col + 1))
                    return true;

                grid[row, col] = 0;
            }
        }

        return false;
    }

    private void RemoveNumbers()
    {
        // Remove a certain number of cells to create the puzzle
        int cellsToRemove = 40 + random.Next(20); // 40-60 cells removed

        for (int i = 0; i < cellsToRemove; i++)
        {
            int row, col;
            do
            {
                row = random.Next(9);
                col = random.Next(9);
            } while (puzzle[row, col] == 0);

            puzzle[row, col] = 0;
        }

        // Mark initial cells
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                initialCells[i, j] = puzzle[i, j] != 0;
            }
        }
    }

    private bool IsValidMove(int[,] grid, int row, int col, int value)
    {
        // Check row
        for (int j = 0; j < 9; j++)
        {
            if (grid[row, j] == value)
                return false;
        }

        // Check column
        for (int i = 0; i < 9; i++)
        {
            if (grid[i, col] == value)
                return false;
        }

        // Check 3x3 box
        int boxRowStart = (row / 3) * 3;
        int boxColStart = (col / 3) * 3;
        for (int i = boxRowStart; i < boxRowStart + 3; i++)
        {
            for (int j = boxColStart; j < boxColStart + 3; j++)
            {
                if (grid[i, j] == value)
                    return false;
            }
        }

        return true;
    }

    public void DisplayPuzzle()
    {
        Console.WriteLine("  1 2 3   4 5 6   7 8 9");
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0 && i != 0)
                Console.WriteLine("  -------------------");

            Console.Write((i + 1) + " ");
            for (int j = 0; j < 9; j++)
            {
                if (j % 3 == 0 && j != 0)
                    Console.Write("| ");

                if (puzzle[i, j] == 0)
                    Console.Write(". ");
                else if (initialCells[i, j])
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(puzzle[i, j] + " ");
                    Console.ResetColor();
                }
                else if (puzzle[i, j] == solution[i, j])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(puzzle[i, j] + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(puzzle[i, j] + " ");
                }
            }
            Console.WriteLine();
        }
    }

    public bool PlaceNumber(int row, int col, int value)
    {
        // Check if the cell is initially populated
        if (initialCells[row, col])
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Cannot modify initial cells!");
            Console.ResetColor();
            return false;
        }

        // Check if the value is valid
        if (value < 1 || value > 9)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Value must be between 1 and 9!");
            Console.ResetColor();
            return false;
        }

        // Check if the move matches the solution
        if (value == solution[row, col])
        {
            puzzle[row, col] = value;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Correct number placed! 😁");
            Console.ResetColor();
            return true;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Incorrect number !! 🥲");
            Console.ResetColor();
            return false;
        }
    }

    public bool CheckWin()
    {
        // Check if all cells match the solution
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (puzzle[i, j] != solution[i, j])
                    return false;
            }
        }
        return true;
    }

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== SUDOKU GAME =====");
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Instructions");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlayGame();
                    break;
                case "2":
                    ShowInstructions();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void PlayGame()
    {
        SudokuGame game = new SudokuGame();
        Random random = new Random();

        while (true)
        {
            game.DisplayPuzzle();

            Console.Write("Enter row (1-9) or 'q' to quit: ");
            string rowInput = Console.ReadLine();
            if (rowInput.ToLower() == "q") break;

            int row;
            while (!int.TryParse(rowInput, out row) || row < 1 || row > 9)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid row. Please enter a number between 1 and 9.");
                Console.ResetColor();
                Console.Write("Enter row (1-9): ");
                rowInput = Console.ReadLine();
                if (rowInput.ToLower() == "q") break;
            }

            Console.Write("Enter column (1-9): ");
            int col;
            while (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 9)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid column. Please enter a number between 1 and 9.");
                Console.ResetColor();
                Console.Write("Enter column (1-9): ");
            }

            Console.Write("Enter value (1-9): ");
            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || value < 1 || value > 9)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid value. Please enter a number between 1 and 9.");
                Console.ResetColor();
                Console.Write("Enter value (1-9): ");
            }

            // Adjust for 0-based indexing
            game.PlaceNumber(row - 1, col - 1, value);

            if (game.CheckWin())
            {
                Console.Clear();
                game.DisplayPuzzle();

                // Get a random congratulatory message
                string congratsMessage = game.congratsMessages[random.Next(game.congratsMessages.Length)];

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You solved the Sudoku puzzle!");
                Console.WriteLine(congratsMessage);
                Console.ResetColor();

                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
                break;
            }
        }
    }

    static void ShowInstructions()
    {
        Console.Clear();
        Console.WriteLine("===== SUDOKU INSTRUCTIONS =====");
        Console.WriteLine("1. The goal is to fill the 9x9 grid so that each column, row, and 3x3 box contains the digits 1-9.");
        Console.WriteLine("2. Use the menu to start a new game.");
        Console.WriteLine("3. Enter the row (1-9), column (1-9), and value (1-9) you want to place.");
        Console.WriteLine("4. Correct numbers will be displayed in green.");
        Console.WriteLine("5. Press 'q' during the game to return to the main menu.");
        Console.WriteLine("\nPress any key to return to the main menu.");
        Console.ReadKey();
    }
}