/*# Intuition
The array contains numbers, each of which has a certain set of bits. The bitwise OR operation always results in saving or adding bits. So, the maximum OR is possible only when we take "many" bits. To find how many subsets give this maximum, it is enough to check all possible sets of elements (subsets) and count those in which the OR is equal to maxOR. Since length ≤ 16 → all 2^n options can be sorted.

# Approach
On the first step:

We calculate maxOr, which is equal to the OR of all elements: nums[0] | nums[1] | ...

This is the upper limit of what OR can achieve

Then we use DFS:

At each step, we either take the current element or skip it

We pass the index and the current pathOr

At the end of the path (when index == nums.Length): compare pathOr == maxOr → if yes, increase the counter

We return the total number of paths that gave maxOr

# Complexity
Metric Value Reason

Time complexity $$O(2^n)$$ Complete search of all subsets
Space complexity $$O(n)$$ Recursion stack depth up to n
# Code*/

```csharp []
public class Solution {
    public int CountMaxOrSubsets(int[] nums) {
        int n = nums.Length;
        int maxOr = 0;

        for(int i = 0; i < n;i++){
             maxOr |= nums[i];
        }
        
   return DFS(0,0, nums,maxOr);

    }

    public int DFS(int index, int pathOr, int[] nums, int maxOr){
       
         if(index == nums.Length){
             return pathOr == maxOr ? 1 : 0;
         }

     int count = 0;

   count += DFS(index+1, pathOr,nums,maxOr);
   count += DFS(index+1,pathOr | nums[index],nums,maxOr);

    return count;

    }
}
```
