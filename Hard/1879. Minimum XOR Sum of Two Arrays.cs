/*# Intuition
We are given two integer arrays, nums1 and nums2, of equal length. We need to assign each nums2[j] to a nums1[i] such that each element in nums2 is used exactly once, and the sum of XOR values for each pairing is minimized. This is essentially a variation of the assignment problem with XOR as the cost function. Since n ≤ 14, brute-force approaches with memoization and bitmasking are feasible.

# Approach
We use bitmask dynamic programming with memoization. The idea is to perform a recursive search (DFS) to try every possible assignment of nums2[j] to the current nums1[index], making sure we do not reuse any element from nums2.

index: the position we are currently assigning in nums1

mask: tracks which elements in nums2 have already been used

For each unused j, we compute the XOR cost nums1[index] ^ nums2[j] and recursively compute the cost of assigning the rest.

We use a dictionary memo[(index, mask)] to store and reuse results to avoid recomputation.

# Complexity
# Time complexity: $$O(n \cdot 2^n)$$

We explore all combinations of assignments using a bitmask, and for each, we do up to n work.

# Space complexity: $$O(2^n)$$

The memoization table stores entries for each bitmask and index combination.*/

# Code
```csharp []
public class Solution {
    public int MinimumXORSum(int[] nums1, int[] nums2) {
         int n = nums1.Length;
         var memo = new Dictionary<(int,int), int>();

         int DFS(int index ,int mask){
               if(index == n) return 0;
               if(memo.ContainsKey((index,mask))) return memo[(index,mask)];

           int minsum = int.MaxValue;
            for(int j = 0;j <n;j++){
                if((mask & ( 1 << j)) == 0){
                   int cost = nums1[index] ^ nums2[j];
                    int total = cost + DFS(index+1, mask | (1 << j));
                    minsum = Math.Min(minsum,total);
                }
            }
           memo [(index,mask)] = minsum;
           return minsum;

         }
      return DFS(0,0);

    }
}
```
