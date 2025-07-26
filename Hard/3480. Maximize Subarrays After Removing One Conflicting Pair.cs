public class Solution {
    public long MaxSubarrays(int n, int[][] conflictingPairs) {
        /**
         * Calculates the maximum number of non-empty subarrays of [1..n]
         * that do not contain both a and b for any remaining conflicting pair.
         * We are allowed to remove exactly one conflicting pair to maximize the count.
         */

        // Total number of non-empty subarrays in an array of length n
        long total = (long)n * (n + 1) / 2;

        long maxValid = 0;
        int m = conflictingPairs.Length;

        // Try removing each conflicting pair one at a time
        for (int skip = 0; skip < m; skip++) {
            long bad = 0;

            // Count subarrays that violate remaining conflict rules
            for (int i = 0; i < m; i++) {
                if (i == skip) continue; // Skip the removed pair

                int a = conflictingPairs[i][0];
                int b = conflictingPairs[i][1];

                /**
                 * Count how many subarrays contain both a and b.
                 * For a subarray to contain both:
                 * - Left bound (L) must be ≤ min(a,b)
                 * - Right bound (R) must be ≥ max(a,b)
                 * So number of such subarrays is: min(a,b) * (n - max(a,b) + 1)
                 */
                int min = Math.Min(a, b);
                int max = Math.Max(a, b);
                bad += (long)min * (n - max + 1);
            }

            // Valid subarrays = total - those containing conflicting pairs
            long valid = total - bad;
            maxValid = Math.Max(maxValid, valid);
        }

        return maxValid;
    }
}
