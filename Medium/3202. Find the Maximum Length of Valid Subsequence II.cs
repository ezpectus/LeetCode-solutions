/*
    Problem: Maximum Length of a Subarray With Modular Matching (LeetCode #2664)

    Time complexity: O(n * k)
    Space complexity: O(k^2)

    Idea:
    - Track transitions between remainders mod k using 2D dp.
    - For each number, calculate mod and update all (prev → mod) paths.
    - dp[prev, mod] stores the length of the longest sequence ending with (prev → mod).
    - Update result with max length across all valid transitions.

    Example:
    nums = [5,7,11], k = 3 → remainders = [2,1,2]
    → valid transitions: (2→1), (1→2) → dp updated accordingly.
*/

public class Solution {
    public int MaximumLength(int[] nums, int k) {
        int[,] dp = new int[k, k];
        int res = 0;

        foreach (int num in nums) {
            int mod = num % k;

            for (int prev = 0; prev < k; prev++) {
                dp[prev, mod] = dp[mod, prev] + 1;
                res = Math.Max(res, dp[prev, mod]);
            }
        }

        return res;
    }
}
