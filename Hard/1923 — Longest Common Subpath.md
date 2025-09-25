# 🧠 LeetCode 1923 — Longest Common Subpath

---

## 🔍 Description

Given `m` paths through a country with `n` cities,  
return the **length of the longest contiguous subpath** that appears in **every path**.

Each path is a list of visited cities (integers from `0` to `n - 1`).  
Cities may repeat, but **not consecutively**.

---

### 🧩 Example

```text
Input:
n = 5
paths = [[0,1,2,3,4],
         [2,3,4],
         [4,0,1,2,3]]

Output: 2
Explanation: The longest common subpath is [2,3]
```

## 💡 Idea
This is a classic binary search + double Rabin-Karp problem:

- Use binary search to guess the length L of the longest common subpath

For each guess:

- Compute rolling hashes of all subpaths of length L for each path
- Use double hashing to avoid collisions
- Intersect hash sets across all paths
- If intersection is non-empty → try longer L
- If empty → try shorter L

## 🧱 C# Implementation
```csharp
public class Solution {
    private const long BASE1 = 100_007;
    private const long BASE2 = 100_009;
    private const long MOD1 = 1_000_000_007;
    private const long MOD2 = 1_000_000_009;

    public int LongestCommonSubpath(int n, int[][] paths) {
        int left =0;
        int right = paths.Min(p => p.Length);
        long[] pow1 = new long[right+1];
        long[] pow2 = new long[right+1];
        pow1[0] = pow2[0] = 1;

     for(int i = 1;i <= right;i++){
        pow1[i] = (pow1[i-1] * BASE1) % MOD1;
        pow2[i] = (pow2[i-1] * BASE2) % MOD2;
     }
        while(left < right){
            int mid = (left+right+1)/2;

        if(HasCommon(paths,mid,pow1,pow2))
            left = mid;
         else
          right = mid-1;
        }

       return left;
    }
private bool HasCommon(int[][] paths, int len, long[] pow1, long[] pow2){
     HashSet<(long,long)> common = null;

     foreach(var path in paths){
         HashSet<(long,long)> curr = new();
         long h1 =0,h2 =0;

         for(int i =0; i < path.Length;i++){
            h1 = (h1 * BASE1 + path[i]) % MOD1;
            h2 = (h2 * BASE2 + path[i]) % MOD2;

            if( i>= len){
                h1 = (h1 - path[i - len] * pow1[len] % MOD1 + MOD1) % MOD1;
                h2 = (h2 - path[i - len] * pow2[len] % MOD2 + MOD2) % MOD2;
            }
             if(i >= len-1)
                curr.Add((h1,h2)); 
         }

         if(common == null)
               common = curr;
          else
            common.IntersectWith(curr);

            if(common.Count == 0) return false;
        }
  return true;
    }
}
```

## ⏱️ Complexity

- Time: O(m × log L × n)
- m — number of paths
- L — max path length
- n — total cities across all paths
- Space: O(n) — hash sets per path

## 🧘Conclusion
This isn’t brute-force — it’s modular hash compression across multiple paths. 

You lead through:

- double Rabin-Karp — two independent hash streams for collision safety
- binary search — guiding the search toward maximal length
- set intersection — filtering shared structure across chaos

This module doesn’t just find overlap — it architects shared memory across divergent paths, 
and lives as a hash-driven subpath engine in your repo.


---
