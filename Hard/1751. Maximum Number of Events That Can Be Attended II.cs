/*
    Problem: Maximum Value of K Events That Don't Overlap (LeetCode #1751)

    Time complexity: O(nk log n) due to sorting + binary search per DP cell
    Space complexity: O(nk)

    Idea:
    - We're given a list of events: [start, end, value]
    - Goal: select up to k non-overlapping events to maximize total value.
    - Sort events by end time for easier DP + binary search.
    - Use DP[i][j]: max value we can get using first i events and selecting j of them.

    Strategy:
    - For each event i and count j:
        - Option 1: skip event i → inherit DP[i-1][j]
        - Option 2: take event i → add its value to DP of last compatible event with j-1 selections
            → Use binary search to find latest event with end time < current start time

    Final result is stored in dp[n][k]
*/

public class Solution {
    public int MaxValue(int[][] events, int k) {
        // Sort events by end time for binary search compatibility
        Array.Sort(events, (a, b) => a[1].CompareTo(b[1]));
        
        // Extract all end times into a separate array for quick lookup
        int[] endTimes = events.Select(e => e[1]).ToArray();
        int n = events.Length;
        
        // DP table: dp[i][j] → max value using first i events and j picks
        int[,] dp = new int[n + 1, k + 1];

        // Loop through all events
        for (int i = 1; i <= n; i++) {
            for (int j = 1; j <= k; j++) {

                // Option 1: skip current event
                dp[i, j] = dp[i - 1, j];

                // Option 2: take current event if possible
                // Binary search for latest event that ends before current one starts
                int left = 0, right = i - 1, res = -1;
                while (left <= right) {
                    int mid = (left + right) / 2;
                    if (endTimes[mid] < events[i - 1][0]) {
                        res = mid;
                        left = mid + 1;
                    } else {
                        right = mid - 1;
                    }
                }

                // Add value of current event + best from compatible previous
                int take = (res != -1) 
                    ? dp[res + 1, j - 1] + events[i - 1][2] 
                    : events[i - 1][2];

                // Maximize between skip and take
                dp[i, j] = Math.Max(dp[i, j], take);
            }
        }

        // Final answer: max value using n events and k picks
        return dp[n, k];
    }
}
