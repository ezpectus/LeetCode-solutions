public class Solution {
    public int MinPathSum(int[][] grid) {
        int m = grid.Length;      // Number of rows
        int n = grid[0].Length;   // Number of columns

        /** 
         * Pre-fill the first row:
         * You can only move right, so just accumulate the sum from left to right.
         */
        for (int j = 1; j < n; j++) {
            grid[0][j] += grid[0][j - 1];
        }

        /** 
         * Pre-fill the first column:
         * You can only move down, so just accumulate the sum from top to bottom.
         */
        for (int i = 1; i < m; i++) {
            grid[i][0] += grid[i - 1][0];
        }

        /** 
         * For the rest of the grid, fill each cell with:
         * current value + min(path from top, path from left)
         * This ensures we always store the minimal path sum up to that cell.
         */
        for (int i = 1; i < m; i++) {
            for (int j = 1; j < n; j++) {
                grid[i][j] += Math.Min(grid[i - 1][j], grid[i][j - 1]);
            }
        }

        /** 
         * Bottom-right cell now contains the minimum path sum from top-left to bottom-right.
         */
        return grid[m - 1][n - 1];
    }
}
