using System;
using System.Linq;
using SudokuProject.Services;
using SudokuProject.Utilities;

namespace SudokuProject.Models
{
    public class SudokuGrid
    {
        private Cell[,] grid = new Cell[9, 9];

        public void Initialize()
        {
            int[,] initialGrid = {
                { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
                { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
                { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
                { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
                { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
                { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
                { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
                { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
                { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
            };

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = new Cell
                    {
                        Value = initialGrid[i, j],
                        IsFixed = initialGrid[i, j] != 0
                    };
                }
            }
        }

        public void Display()
        {
            DisplayHelper.DisplayGrid(grid);
        }

        public bool PlaceNumber(int row, int col, int num)
        {
            row--;
            col--;

            if (grid[row, col].IsFixed) return false;
            
            if (ValidationService.IsValidMove(GetGridValues(), row, col, num))
            {
                grid[row, col].Value = num;
                return true;
            }
            return false;
        }

        public bool IsSolved()
        {
            return ValidationService.IsGridComplete(GetGridValues());
        }

        private int[,] GetGridValues()
        {
            int[,] values = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    values[i, j] = grid[i, j].Value;
                }
            }
            return values;
        }
    }
}
