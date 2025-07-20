/* Problem: Bitwise Complement (LeetCode #1009)

Time complexity: O(log n) — each shift halves the range Space complexity: O(1)

Goal: - Given an integer n, return its binary complement. - 
The complement flips all bits in n, but only up to its most significant bit. -
For example: - n = 5 → binary: 101 → complement: 010 → result = 2

Strategy: - First, special case: if n = 0 → return 1 (binary: 000 → flipped: 111 → trimmed to 1) - 
Otherwise, we find the smallest power of 2 greater than n → this builds a bitmask of 1s - 
For n = 5 → mask = 8 → subtract 1 → mask = 7 → binary: 111 - 
Then XOR n with mask flips only the active bits - n = 5 = 101 → mask = 111 → 101 ^ 111 = 010 → result = 2 */

public class Solution {
    public int BitwiseComplement(int n) {
        if (n == 0) return 1;

        int mask  = 1;
        while( mask <= n){
         mask <<= 1; 
        }

        mask -=1;

     return n ^ mask;
    }
}
