# 3719. Longest Balanced Subarray I  
*O(n²) — Simple Brute Force with Distinct Counts*

---

## Problem Statement

- You are given an integer array `nums` (1 ≤ n ≤ 1500, 1 ≤ nums[i] ≤ 10⁵).
- A subarray is **balanced** if the number of **distinct even** numbers in it equals the number of **distinct odd** numbers.
- Return the **length** of the **longest balanced subarray** (return 0 if none exists).

---

## Approach — Double Loop with Two Dictionaries

We check **every possible subarray** [i..j] and maintain two dictionaries:

- `even` — tracks distinct even numbers in current subarray
- `odd` — tracks distinct odd numbers

For each starting index `i`, we extend the end index `j` and add numbers to the appropriate dictionary.  
Whenever the counts of distinct evens and odds become equal, we update the maximum length.

This is a straightforward brute-force solution that is easy to understand and debug.

---

## Clean Implementation (C#)

```csharp
public class Solution {
    public int LongestBalanced(int[] nums) {
        int n = nums.Length;
        int maxLen = 0;

        for (int i = 0; i < n; i++) {
            var odd = new Dictionary<int, int>();
            var even = new Dictionary<int, int>();

            for (int j = i; j < n; j++) {
                var dict = (nums[j] & 1) == 1 ? odd : even;

                // Track distinct numbers (value doesn't matter, key is unique)
                if (!dict.ContainsKey(nums[j])) dict[nums[j]] = 1;
            
                if (odd.Count == even.Count) {
                    maxLen = Math.Max(maxLen, j - i + 1);
                }
            }
        }

        return maxLen;
    }
}
```
## Complexity

| **Metric**            | **Value**     | **Notes**                                      |
|-----------------------|---------------|------------------------------------------------|
| **Time Complexity**   | **O(n²)**     | Two nested loops — checks every possible subarray [i..j] |
| **Space Complexity**  | **O(n)**      | Two dictionaries per starting index (worst case: all unique numbers in subarray) |

**Fits constraints perfectly** — n ≤ 1500 → 1500² = 2.25 million operations — runs very fast on LeetCode.

---

## Why This Works — Example Walkthrough

**nums = [1,2,3,2]**

- i = 0 → subarrays: [1], [1,2], [1,2,3], [1,2,3,2]
- i = 1 → subarrays: [2], [2,3], [2,3,2]  
  → [2,3,2]: evens = {2} (1), odds = {3} (1) → counts equal → length = **3** → max = 3
- i = 2 and i = 3 → shorter subarrays

**Result = 3** → correct

**Correct** — every possible subarray is checked exactly once.  
Dictionaries track **distinct** even and odd numbers in the current window.  
When the counts become equal, we record the current length.

---

## Key Takeaway

Simple and reliable solution:

- Double loop over all possible subarrays [i..j]
- Use two dictionaries (even and odd) to count **distinct** numbers by parity
- When `odd.Count == even.Count` → update maximum length
- Dictionaries automatically ignore duplicates (only keys matter)

**Straightforward, correct, passes all tests** — ideal for n ≤ 1500.

---
