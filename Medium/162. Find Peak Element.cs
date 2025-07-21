public class Solution {
    // Binary search to find any peak element (greater than neighbors)
    public int FindPeakElement(int[] nums) {
        int left = 0;
        int right = nums.Length - 1;

        // Continue search until pointers converge
        while (left < right) {
            int mid = left + (right - left) / 2;

            // If mid is greater than next → move left
            if (nums[mid] > nums[mid + 1]) {
                right = mid; // mid could be the peak
            } else {
                left = mid + 1; // peak must be in the right half
            }
        }

        // Left and right have converged to peak index
        return left;
    }
}
