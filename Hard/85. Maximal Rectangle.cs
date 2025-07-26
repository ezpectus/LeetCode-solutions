/**
 * Problem: Maximal Rectangle in a Binary Matrix
 * Given a 2D binary matrix filled with '0's and '1's,
 * find the largest rectangle containing only '1's and return its area.
 *
 * Approach:
 * - Treat each row as a layer of a histogram.
 * - For each row, build up a `height[]` array that reflects how many continuous '1's stack vertically.
 * - After updating `height[]`, apply the "Largest Rectangle in Histogram" algorithm using a monotonic stack.
 * - Track and return the maximum area found across all rows.
 *
 * Time Complexity: O(rows × cols)
 * Space Complexity: O(cols)
 */
public class Solution {
    public int MaximalRectangle(char[][] matrix) {
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int[] height = new int[cols]; // Tracks histogram heights per column
        int area = 0; // Stores maximal rectangle area across all rows

        for(int i = 0; i < rows; i++) {
            for(int j = 0; j < cols; j++) {
                // Build vertical histogram by increasing height at '1', resetting at '0'
                if(matrix[i][j] == '1') {
                    height[j] += 1;
                } else {
                    height[j] = 0;
                }
            }

            // For the current histogram (row), compute largest rectangle
            area = Math.Max(area, LargestRectangle(height));
        }

        return area;
    }

    // Computes largest rectangle area in a histogram using monotonic stack
    private int LargestRectangle(int[] heights) {
        Stack<int> stack = new Stack<int>(); // Stores indices of bars in increasing order
        int maxArea = 0;

        // Add sentinel 0-height bars at both ends to flush stack completely
        int[] extended = new int[heights.Length + 2];    
        Array.Copy(heights, 0, extended, 1, heights.Length);

        for(int i = 0; i < extended.Length; i++) {
            // If current bar is shorter than stack top → process previous taller bars
            while(stack.Count > 0 && extended[i] < extended[stack.Peek()]) {
                int height = extended[stack.Pop()]; // Height of the popped bar
                int width = i - stack.Peek() - 1; // Width between current and new stack top
                maxArea = Math.Max(maxArea, height * width); // Update max area
            }

            // Push current index to stack for future consideration
            stack.Push(i);
        }

        return maxArea;
    }
}
