/*
    Problem: Maximum XOR of Two Numbers in an Array (LeetCode #421)
    
    Time complexity: O(32 * n) = O(n)
    Space complexity: O(n) due to prefix HashSet

    Task:
    - Given array nums[], find the max XOR of any two numbers.

    Strategy:
    - XOR is maximized when the bits differ in higher positions.
    - We greedily try to build the maximum possible XOR bit-by-bit from the most significant bit.
    - At each bit level:
        - We mask the numbers to only consider top i bits.
        - We store those prefixes in a HashSet.
        - Then we assume that current bit can be "1" in final XOR (candidate = max | (1 << i))
        - For this candidate to be valid, we need two prefixes a and b where a ^ b == candidate.
        - If such pair exists in the HashSet, we keep that candidate as part of max.
        - Repeat until all bits processed.
*/
public class Solution {
    public int FindMaximumXOR(int[] nums) {
        int max = 0;  /* final result — the largest XOR found so far */
        int mask = 0; /* current bitmask used to isolate top i bits */

        for (int i = 31; i >= 0; i--) {
            mask |= (1 << i);  
            /* progressively include higher bits in the mask from left to right (MSB to LSB) */

            HashSet<int> prefixes = new HashSet<int>();
            foreach (int num in nums) {
                prefixes.Add(num & mask);  
                /* collect all prefixes using current mask — only upper i bits are kept */
            }

            int candidate = max | (1 << i);  
            /* greedy assumption: maybe we can set current i-th bit in max */

            foreach (int prefix in prefixes) {
                /* check if there exists another prefix such that prefix1 ^ prefix2 = candidate */
                if (prefixes.Contains(prefix ^ candidate)) {
                    max = candidate;  /* if yes — we can build larger XOR */
                    break;
                }
            }
        }

        return max;  /* after trying all bits, return the largest XOR we managed to build */
    }
}
