namespace SudokuProject.Services
{
    public static class ValidationService
    {
        public static bool IsValidMove(int[,] grid, int row, int col, int num)
        {
            //number validation
            if (num < 1 || num > 9) return false;

            //check row
            for (int i = 0; i < 9; i++)
            {
                if (grid[row, i] == num && i != col) return false;
            }

            //check column
            for (int i = 0; i < 9; i++)
            {
                if (grid[i, col] == num && i != row) return false;
            }

            //check 3x3 sub-grid
            int subGridStartRow = (row / 3) * 3;
            int subGridStartCol = (col / 3) * 3;
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int currentRow = subGridStartRow + i;
                    int currentCol = subGridStartCol + j;
                    
                    if (grid[currentRow, currentCol] == num && 
                        (currentRow != row || currentCol != col)) 
                        return false;
                }
            }

            return true;
        }

        public static bool IsGridComplete(int[,] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //check if cell is empty or breaks Sudoku rules
                    if (grid[i, j] == 0 || 
                        !IsValidMove(grid, i, j, grid[i, j]))
                        return false;
                }
            }
            return true;
        }
    }
}
