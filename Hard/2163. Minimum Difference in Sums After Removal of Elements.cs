/*
    Problem: Minimum Difference in Sums After Removal of Elements (LeetCode #2163)

    Time complexity: O(n log n)
    Space complexity: O(n)

    Idea:
    - We're given 3n elements and must remove exactly n of them.
    - The remaining 2n elements are split into two equal parts (n elements each).
    - Goal: minimize the difference between the sums of these two parts (sumFirst - sumSecond).

    Approach:
    - Use two heaps to track the optimal elements to keep on each side:
        - Left side (prefix): keep n smallest elements → maxHeap
        - Right side (suffix): keep n largest elements → minHeap
    - Build prefix sums using maxHeap to retain n smallest values seen so far.
    - Build suffix sums using minHeap to retain n largest values seen so far.
    - Iterate over possible split points (between left and right parts)
      and calculate the difference in sums → keep track of minimum.

    Example:
    nums = [7,9,5,8,1,3] → n = 2
    Remove 9 and 1 → left: [7,5], right: [8,3] → diff = 12 - 11 = 1
*/


public class Solution {
    public long MinimumDifference(int[] nums) {
        int n = nums.Length / 3;
        int totalLength = nums.Length;

        var leftHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));
         long leftSum = 0;
         long[] leftSums = new long[totalLength]; 

       for(int i = 0; i < 2 * n; i++) {
             leftHeap.Enqueue(nums[i], nums[i]);
             leftSum += nums[i];
           if (leftHeap.Count > n) {
               leftSum -= leftHeap.Dequeue();
                 }
              leftSums[i] = leftSum;
            }

            var rightHeap = new PriorityQueue<int, int>();
            long rightSum = 0;
              long[] rightSums = new long[totalLength];

         for (int i = totalLength - 1; i >= n; i--) {
           rightHeap.Enqueue(nums[i], nums[i]);
              rightSum += nums[i];
          if (rightHeap.Count > n) {
                rightSum -= rightHeap.Dequeue();
              }
            rightSums[i] = rightSum;
         }

        long minDiff = long.MaxValue;
         for (int i = n - 1; i < 2 * n; i++) {
             minDiff = Math.Min(minDiff, leftSums[i] - rightSums[i + 1]);
         }

   return minDiff;


    }
}
