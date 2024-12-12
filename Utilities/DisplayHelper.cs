using System;
using SudokuProject.Models;


namespace SudokuProject.Utilities
{
    public static class DisplayHelper
    {
        public static void ShowInstructions()
        {
            Console.WriteLine("=== SUDOKU GAME ===");
            Console.WriteLine("Rules:");
            Console.WriteLine("- Fill the grid so that each column, row, and 3x3 sub-grid contains all digits 1-9");
            Console.WriteLine("- Each number can appear only once in each row, column, and sub-grid");
            Console.WriteLine("\nHow to play:");
            Console.WriteLine("Enter moves as: row,column,number (e.g., 1,1,1)");
            Console.WriteLine("Type 'quit' to exit the game\n");
        }

        public static void DisplayGrid(Cell[,] grid)
        {
            Console.WriteLine("Current Grid:");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0) Console.Write("| ");
                    
                    //highlight fixed cells differently
                    if (grid[i, j].IsFixed)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(grid[i, j].Value + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(grid[i, j].Value == 0 ? "_ " : grid[i, j].Value + " ");
                    }
                }
                
                if (i % 3 == 2 && i != 8) Console.WriteLine("\n------+-------+------");
                else Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: {message}");
            Console.ResetColor();
        }
    }
}
