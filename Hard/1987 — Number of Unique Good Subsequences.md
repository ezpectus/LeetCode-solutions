
# 🧩 LeetCode 1987 — Number of Unique Good Subsequences

## 📜 Problem Statement

Given a binary string `binary`, return the number of **unique good subsequences**.  
A good subsequence is:
- Non-empty
- Does not start with `'0'` unless it is exactly `"0"`

---

## 🧠 Optimized Strategy

### 🔹 Core Idea
- Use DP to track:
  - `endWith0`: number of subsequences ending with `'0'`
  - `endWith1`: number of subsequences ending with `'1'`
- For each character:
  - If `'1'`: can start new or extend existing → update both
  - If `'0'`: can only extend `endWith1`
- Track if `"0"` exists — it’s a valid subsequence on its own

---

## ✅ Final Code (C#)

```csharp
public class Solution {
    public int NumberOfUniqueGoodSubsequences(string binary) {
        int MOD = 1_000_000_007;
        long endWith0 = 0, endWith1 = 0;
        bool hasZero = false;

        foreach (char c in binary) {
            if (c == '0') {
                hasZero = true;
                endWith0 = (endWith0 + endWith1) % MOD;
            } else {
                endWith1 = (endWith0 + endWith1 + 1) % MOD;
            }
        }

        return (int)((endWith0 + endWith1 + (hasZero ? 1 : 0)) % MOD);
    }
}
```

## ⏱ Complexity

| Metric            | Value                                                              |
|-------------------|--------------------------------------------------------------------|
| **Time Complexity**   | `O(n)` — single pass over the binary string, updating two counters |
| **Space Complexity**  | `O(1)` — constant space for `endWith0`, `endWith1`, and `hasZero` flag |

### 🔍 Breakdown
- Each character is processed once.
- No auxiliary arrays or sets are used — only scalar counters.
- The `"0"` case is handled separately via a boolean flag, avoiding edge-case branching in the main loop.

---

## 🧘 Key Takeaways

- **Leading zero filter** is the core constraint: `"0"` is valid, but `"01"` is not. This requires careful handling of subsequence starts.
- **Dynamic programming without storage** — the solution tracks contributions using just two counters:
  - `endWith0`: number of subsequences ending in `'0'`
  - `endWith1`: number of subsequences ending in `'1'`
- **Elegant O(1) space** — no need to store or enumerate actual subsequences.
- This pattern generalizes to problems involving:
  - Prefix constraints
  - Subsequence uniqueness
  - Character-based contribution tracking

---

## 📦 Module Summary

| Component         | Description                                         |
|------------------|-----------------------------------------------------|
| **Pattern**       | Binary DP with prefix filtering                    |
| **Reusability**   | High — applicable to constrained subsequence counting, prefix-sensitive DP, and uniqueness tracking |
| **Edge Cases**    | Handles multiple zeros, `"0"` only, alternating patterns, and mixed sequences like `"101010"` |
| **Performance**   | Linear time, constant space — ideal for large input sizes (`n ≤ 10⁵`) |
| **Debuggability** | Easy to trace with printed counters per step; `"hasZero"` flag isolates special case cleanly |


---
