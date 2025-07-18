/*
    Problem: Kth Smallest Product of Two Sorted Arrays (LeetCode #2040)

    Time complexity: O(n * log m * log range)
      - Binary search over product range: log(1e13)
      - For each mid, count how many products ≤ mid using O(n log m)
    Space complexity: O(1), aside from input arrays

    Idea:
    - We're given two sorted arrays: nums1 and nums2
    - Need to find the k-th smallest product formed by picking one element from each array
    - Products can be negative, zero, or positive → need global binary search over full range

    Strategy:
    - Use binary search over range [-1e10, 1e10]
    - For each midpoint, count how many products ≤ mid:
        → If count ≥ k → search left
        → Else → search right
    - Counting is done via modified binary search for each value in nums1:
        - If a == 0:
            → If x ≥ 0 → contributes full length (all products are ≤ x)
            → Else → contributes 0
        - If a > 0:
            → Find how many values in nums2 ≤ x / a → use UpperBound
        - If a < 0:
            → Find how many values in nums2 ≥ ceil(x / a) → use LowerBound

    Helpers:
    - UpperBound: first index > value
    - LowerBound: first index ≥ value
    - DivideFloor / DivideCeil: handle rounding properly with sign control

    Final answer: the lowest product value that has at least k smaller/equal products
*/


public class Solution {
    public long KthSmallestProduct(int[] nums1, int[] nums2, long k) {
      
        this.nums1 = nums1;
        this.nums2 = nums2;

        long left = -1_0000_0000_0000L;  
        long right = 1_0000_0000_0000L;  

        while (left < right) {
            long mid = left + (right - left) / 2;
            if (CountLessOrEqual(mid) >= k) {
                right = mid;
            } else {
                left = mid + 1;
            }
        }
        return left;

    }

   private int[] nums1, nums2;

 long CountLessOrEqual(long x){
    long count = 0;
    foreach(int a in nums1){
        if(a == 0){
            if(x >= 0) count += nums2.Length;
        } else if(a > 0){
            count += UpperBound(nums2, DivideFloor(x, a));
        } else {
            count += nums2.Length - LowerBound(nums2, DivideCeil(x, a));
        }
    }
    return count;
}


  int UpperBound(int[] nums, long value){
     int left = 0;
     int right = nums.Length;

     while(left < right){
        int mid = left + (right- left)/2;
        if(nums[mid] <= value){
            left = mid+1;
        } else{
            right = mid;
        }
     }
   return left;
}

int LowerBound(int[] nums, long value){
     int left = 0;
     int right = nums.Length;

     while(left < right){
        int mid = left + (right- left)/2;
        if(nums[mid] < value){
            left = mid+1;
        } else{
            right = mid;
        }
     }
   return left;
}

long DivideCeil(long a, long b) {
    if (b == 0) throw new DivideByZeroException();
    long div = a / b;
    if ((a ^ b) >= 0 && a % b != 0) div++; 
    return div;
}
  long DivideFloor(long a, long b) {
    if (b == 0) throw new DivideByZeroException();
    long div = a / b;
    if ((a ^ b) < 0 && a % b != 0) div--;
    return div;
   }
}
