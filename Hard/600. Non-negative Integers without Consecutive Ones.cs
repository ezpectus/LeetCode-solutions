/*
    Problem: Non-negative Integers Without Consecutive Ones (LeetCode #600)

    Time complexity: O(1) — constant time due to fixed 32-bit traversal
    Space complexity: O(1)

    Idea:
    - We're counting how many integers in [0, n] have no consecutive 1s in their binary representation.
    - Use DP array f[i] = number of valid binary strings of length i without consecutive 1s.
        → f[i] = f[i-1] + f[i-2] (same as Fibonacci)
    - Traverse bits of n from highest (30th) to lowest:
        → If current bit is 1, add f[j] to sum (counts all numbers with 0 at j and valid below)
        → If previous bit was also 1, we’ve found consecutive ones → subtract 1 and exit loop
    - At the end, return sum + 1 to include n itself if it’s valid.

    Example:
    n = 5 → binary: 101
    → valid numbers: 0,1,2,4,5 → result: 5
*/

public class Solution {
    public int FindIntegers(int n) {
        int[] f = new int[32];
        f[0] = 1;
        f[1] = 2;

        for(int i = 2; i < f.Length;i++){
            f[i] = f[i-1] + f[i-2];

        }
        int j = 30;
        int sum = 0;
        int prev = 0;

 while( j >= 0){
    if((n & ( 1 << j)) != 0){
        sum += f[j];
        if(prev == 1){
            sum--;
            break;
        }
        prev = 1;
    } else{
        prev = 0;
      
    }
      j--;
 }

      return sum+1;  
    }
}
