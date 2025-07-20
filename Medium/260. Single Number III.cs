/* Problem: Single Number III (LeetCode #260)

Time complexity: O(n) Space complexity: O(1)

Goal: - Given an array where every number appears twice except for two, find those two unique numbers.

Strategy: - If all duplicates cancel via XOR, then the XOR of entire array is equal to a ^ b, where a and b are the two unique numbers. - 
This result gives us information about the bits where a and b differ (i.e., at least one bit is '1' in a ^ b). - 
Choose one such bit (the lowest set bit) to partition the array into two groups: - 
Group 1: numbers with that bit set - Group 2: numbers without it - 
In each group, the duplicate numbers still cancel, leaving us with one of the unique numbers per group.

Beautiful use of bitmask to separate the two distinct values. */

public class Solution {
    public int[] SingleNumber(int[] nums) {
        int xor = 0;
        foreach(int num in nums){
             xor ^= num; 
             }
    
      int diff = xor & -xor;

      int a = 0;
      foreach(int num in nums){
        if((num & diff) != 0) a ^= num;

      }
     int b = xor ^ a;
    return new int[] {a,b};

    }
}
