# 🧩 LeetCode 1787 — Make the XOR of All Segments Equal to Zero

## 📜 Problem Statement

Given an array `nums` and an integer `k`, you can change elements of `nums`.  
Your goal is to make the XOR of every segment of length `k` equal to zero, using the **minimum number of changes**.

---

## 🧠 Optimized Strategy

### 🔹 Core Idea
- Group elements by index modulo `k`: `group[i] = nums[j]` where `j % k == i`.
- For each group, count frequency of values.
- Use **DP over XOR states**:
  - `dp[x]` — minimum changes to get XOR `x` using first `i` groups.
  - Transition: for each `x`, try all values `v` in group `i`, update `dp[x ^ v]`.

---

## ✅ Final Code (C#)

```csharp
public class Solution {
    public int MinChanges(int[] nums, int k) {
        int MAXX = 1 << 10;
        int INF = nums.Length + 1;
        int[] dp = new int[MAXX];
        Array.Fill(dp, INF);
        dp[0] = 0;

        for (int i = 0; i < k; i++) {
            Dictionary<int, int> freq = new();
            int count = 0;
            for (int j = i; j < nums.Length; j += k) {
                freq[nums[j]] = freq.GetValueOrDefault(nums[j], 0) + 1;
                count++;
            }

            int[] ndp = new int[MAXX];
            Array.Fill(ndp, INF);
            int minPrev = dp.Min();

            foreach (int x in Enumerable.Range(0, MAXX)) {
                foreach (var kv in freq) {
                    int v = kv.Key, f = kv.Value;
                    ndp[x] = Math.Min(ndp[x], dp[x ^ v] + count - f);
                }
                ndp[x] = Math.Min(ndp[x], minPrev + count);
            }

            dp = ndp;
        }

        return dp[0];
    }
}
```

## ⏱ Complexity

| Metric            | Value                                                              |
|-------------------|--------------------------------------------------------------------|
| **Time Complexity**   | `O(k × MAXX × freq)` — where `MAXX = 2¹⁰ = 1024`, and `freq` is the number of distinct values per group |
| **Space Complexity**  | `O(MAXX)` — for storing XOR state transitions in DP arrays |

### 🔍 Breakdown
- `k` groups are formed by `i % k` indexing.
- For each group, we iterate over all possible XOR states (`MAXX = 1024`) and all values in the group (`freq`).
- Each DP transition computes the minimum number of changes needed to reach a target XOR.
- The final answer is `dp[0]` — the minimum changes to make all segments XOR to zero.

---

## 🧘 Key Takeaways

- **Modular grouping (`i % k`)** transforms the global XOR constraint into local, manageable segments.
- **DP over XOR states** is a powerful pattern for problems involving bitwise constraints and segment normalization.
- You avoid brute-force segment enumeration by **preprocessing frequency maps** and optimizing transitions.
- The solution is scalable and efficient even for large arrays, thanks to the bounded XOR space (`2¹⁰`).
- This pattern generalizes to other problems involving:
  - Bitwise masks
  - Segment-wise constraints
  - Frequency-aware DP transitions

---

## 📦 Module Summary

| Component         | Description                                         |
|------------------|-----------------------------------------------------|
| **Pattern**       | Grouped DP over XOR states                         |
| **Reusability**   | High — applicable to bitwise segment normalization, XOR balancing, and frequency-aware DP |
| **Edge Cases**    | Handles repeated values, large `k`, sparse distributions, and uneven group sizes |
| **Performance**   | Efficient for `nums.length ≤ 3×10⁴`, `k ≤ 10³`, and bounded value space (`arr[i] < 2¹⁰`) |
| **Debuggability** | Easy to trace with printed `dp` arrays per group, and frequency maps for each modulo class |


---
