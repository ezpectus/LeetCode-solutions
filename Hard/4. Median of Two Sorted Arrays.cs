/*
    Problem: Find the median of two sorted arrays.

    Time complexity: O(log(min(n, m))) — where n and m are lengths of the two arrays.
    Space complexity: O(1)

    Approach:
    - Always binary search on the smaller array (nums1), to optimize and avoid index out of bounds.
    - We partition both arrays so that the left part has (n + m + 1)/2 elements.
    - Ensure that all elements on the left side are less than or equal to all elements on the right.
    - If partition is correct:
        - If the total length is even → median = (max of left parts + min of right parts) / 2
        - If the total length is odd  → median = max of left parts

    Edge cases are handled using int.MinValue and int.MaxValue when partition goes to array bounds.
*/

public class Solution {
    public double FindMedianSortedArrays(int[] nums1, int[] nums2) {
        // Ensure nums1 is the smaller array
        if (nums1.Length > nums2.Length) {
            return FindMedianSortedArrays(nums2, nums1);
        }

        int x = nums1.Length;
        int y = nums2.Length;
        int low = 0;
        int high = x;

        while (low <= high) {
            int partitionX = (low + high) / 2;
            int partitionY = (x + y + 1) / 2 - partitionX;

            int maxX = (partitionX == 0) ? int.MinValue : nums1[partitionX - 1];
            int minX = (partitionX == x) ? int.MaxValue : nums1[partitionX];

            int maxY = (partitionY == 0) ? int.MinValue : nums2[partitionY - 1];
            int minY = (partitionY == y) ? int.MaxValue : nums2[partitionY];

            // Check if partition is correct
            if (maxX <= minY && maxY <= minX) {
                if ((x + y) % 2 == 0) {
                    // Even total length
                    return ((double)Math.Max(maxX, maxY) + Math.Min(minX, minY)) / 2;
                } else {
                    // Odd total length
                    return (double)Math.Max(maxX, maxY);
                }
            }
            else if (maxX > minY) {
                // Move left
                high = partitionX - 1;
            }
            else {
                // Move right
                low = partitionX + 1;
            }
        }

        throw new ArgumentException("Input arrays are not sorted or valid.");
    }
}

