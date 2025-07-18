/*
    Problem: Number of Incremovable Subarrays (LeetCode #2865)

    Time complexity: O(n^3) — triple nested loops
    Space complexity: O(n) — temporary list per iteration

    Idea:
    - Try removing every possible subarray [i...j] from the original array.
    - After removing, check if the remaining array is strictly increasing.
    - Increment count if the current removal leads to a strictly increasing result.
    - Use helper function to validate the increasing property.

    Example:
    nums = [1,2,3,4]
    → all subarray removals leave increasing array → count = 10
    nums = [1,3,2]
    → remove [3] → [1,2] → increasing → count = 1
*/

public class Solution {
    public int IncremovableSubarrayCount(int[] nums) {
        int n = nums.Length;
        int count = 0;

        for (int i = 0; i < n; i++) {
            for (int j = i; j < n; j++) {
                List<int> temp = new List<int>();

                for (int k = 0; k < i; k++) {
                    temp.Add(nums[k]);
                }

                for (int k = j + 1; k < n; k++) {
                    temp.Add(nums[k]);
                }

                if (IsStrictlyIncreasing(temp)) {
                    count++;
                }
            }
        }

        return count;
    }

    private bool IsStrictlyIncreasing(List<int> arr) {
        for (int i = 1; i < arr.Count; i++) {
            if (arr[i] <= arr[i - 1]) return false;
        }
        return true;
    }
}
