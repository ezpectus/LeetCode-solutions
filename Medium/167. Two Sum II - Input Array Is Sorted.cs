/*
    Problem: Two Sum II – Input Array Is Sorted (LeetCode #167)

    Time complexity: O(n)
    Space complexity: O(1)

    Idea:
    - Use two pointers: one at the beginning, one at the end.
    - Since the array is sorted, adjust pointers based on current sum:
        - If sum < target → move left pointer forward (need bigger number)
        - If sum > target → move right pointer backward (need smaller number)
        - If sum == target → return the 1-based indices
    - This works efficiently in one pass using sorted property.

    Example:
    numbers = [2,7,11,15], target = 9
    → 2 + 7 = 9 → return [1,2]
*/

public class Solution {
    public int[] TwoSum(int[] numbers, int target) {
        int left = 0;
        int right = numbers.Length - 1;

        while (left < right) {
            int sum = numbers[left] + numbers[right];
            
            if (sum == target) {
                return new int[] { left + 1, right + 1 }; // Return 1-based indices
            } else if (sum < target) {
                left++; // Need larger sum → move left forward
            } else {
                right--; // Need smaller sum → move right backward
            }
        }

        return new int[] {}; // Fallback (problem guarantees exactly one solution)
    }
}
