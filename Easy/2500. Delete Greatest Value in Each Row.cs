public class Solution {
    public int DeleteGreatestValue(int[][] grid) {
        int m = grid.Length;         // Number of rows
        int n = grid[0].Length;      // Number of columns
        
        // Step 1: Sort each row in ascending order
        foreach (var row in grid) {
            Array.Sort(row);
        }

        int result = 0;

        // Step 2: Loop from last column to first (right to left)
        for (int col = n - 1; col >= 0; col--) {
            int maxInCol = 0;
            
            // Step 3: Find the max value in this column across all rows
            for (int row = 0; row < m; row++) {
                maxInCol = Math.Max(maxInCol, grid[row][col]);
            }

            // Step 4: Add the max value to the result
            result += maxInCol;
        }

        return result;
    }
}
