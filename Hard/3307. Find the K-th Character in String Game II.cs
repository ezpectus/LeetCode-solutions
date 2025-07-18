/*
    Problem: Kth Character After Certain Operations (LeetCode #2866)

    Time complexity: O(n)
    Space complexity: O(n)

    Idea:
    - Start with the character 'a'.
    - For each operation, the current string doubles in length.
        → For example, from "a" to "aa", or to "bb" depending on op.
    - At each operation, if op = 1, shift the character by 1 (e.g., 'a' → 'b').
    - We're asked to find the character at position k after all operations, but without actually building the full string.

    Strategy:
    - Precompute the maximum possible length after each step (capped by k).
    - Traverse operations in reverse:
        - At each step, if k lies in the second half of the doubled string, then:
            → subtract half length from k (shift to equivalent first half index)
            → and if op = 1 → apply shift
    - At the end, compute final character using total shift.

    Example:
    operations = [0,1]
    → Step 0: "a" → Step 1: "aa" → Step 2: "bb"
    → K = 3 → result is 'b'
*/

public class Solution {
    public char KthCharacter(long k, int[] operations) {
        int n = operations.Length;
        long[] lengths = new long[n + 1];
        lengths[0] = 1; 
        for (int i = 0; i < n; i++) {
            lengths[i + 1] = lengths[i] * 2;
            if (lengths[i + 1] > k) {
                lengths[i + 1] = k; 
            }
        }

        int shift = 0;
        for (int i = n - 1; i >= 0; i--) {
            long half = lengths[i];
            if (k > half) {
                k -= half; 
                if (operations[i] == 1) {
                    shift++; 
                }
            }
        }
        char result = (char)('a' + (shift % 26));
        return result;
    }
}
