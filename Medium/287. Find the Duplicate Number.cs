public class Solution {
    public int FindDuplicate(int[] nums) {
        /** Initialize pointers for cycle detection */
        int slow = nums[0];
        int fast = nums[0];

        /** Phase 1: Find intersection point inside the cycle */
        do {
            slow = nums[slow];
            fast = nums[nums[fast]];
        } while (slow != fast);

        /** Phase 2: Move finder and slow to find cycle entry point */
        int finder = nums[0];
        while (finder != slow) {
            finder = nums[finder];
            slow = nums[slow];
        }

        /** Return the duplicate number */
        return finder;
    }
}
