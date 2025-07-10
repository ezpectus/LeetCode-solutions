
/*
    Problem: Non-overlapping Intervals (LeetCode 435)

    Time complexity: O(n log n)
    Space complexity: O(1)

    Idea:
    - Sort intervals by their end time.
    - Iterate and count how many intervals overlap.
    - For each interval, if it overlaps with the previous one (i.e., start < end), we remove it.
    - Greedy: always keep the interval with the earlier end time to maximize room for others.

    Example:
    Input: [[1,2],[2,3],[3,4],[1,3]]
    Sorted: [[1,2],[1,3],[2,3],[3,4]]
    Removed: [1,3] → result = 1

    We return the number of removed intervals.
*/

public class Solution {
    public int EraseOverlapIntervals(int[][] intervals) {
        if (intervals.Length == 0) return 0;

        // Sort by end time
        Array.Sort(intervals, (a, b) => a[1].CompareTo(b[1]));

        int count = 0;
        int prevEnd = intervals[0][1];

        for (int i = 1; i < intervals.Length; i++) {
            if (intervals[i][0] < prevEnd) {
                // Overlapping → remove this one (increase count)
                count++;
            } else {
                // No overlap → update end
                prevEnd = intervals[i][1];
            }
        }

        return count;
    }
}


