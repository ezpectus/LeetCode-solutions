# 🧠 Module: Bitwise Reduction — Make `n` Equal to `k` via 1→0

## 📌 Problem Statement

Given two positive integers `n` and `k`, you can change any `1` bit in `n` to `0`.  
Return the **minimum number of changes** needed to make `n == k`.  
If it's **impossible**, return `-1`.

---

## 🧩 Architectural Insight

### ✅ Signals from the problem:
- Only allowed operation: `1 → 0` in `n`
- You **cannot create new 1s** → `k` must be a **bitwise subset** of `n`
- Changes = number of `1`s in `n` that must be flipped to match `k`

### ❗ Key condition:
- If `(n & k) != k` → `k` contains bits that `n` doesn’t → impossible

---

## 🔧 Strategy

1. **Feasibility check**:  
   - `if ((n & k) != k)` → return `-1`

2. **Count changes**:  
   - `n ^ k` highlights differing bits  
   - Count how many `1`s in `n` must be flipped → use bit counting

---

## ✅ C# Implementation

```csharp
public class Solution {
    public int MinChanges(int n, int k) {
        if ((n & k) != k) return -1;

        int xor = n ^ k;
        return CountBits(xor);
    }

    private int CountBits(int x) {
        int count = 0;
        while (x > 0) {
            count += x & 1;
            x >>= 1;
        }
        return count;
    }
}
```

## 📦 Complexity Analysis

| Aspect         | Value             | Explanation                                                                 |
|----------------|-------------------|------------------------------------------------------------------------------|
| Time           | `O(log n)`        | We count bits in `n ^ k`, which takes time proportional to the number of bits in `n`. Since `n ≤ 10⁶`, this is at most 20 iterations. |
| Space          | `O(1)`            | No arrays, maps, or dynamic structures are used. Just integer counters.     |
| Bitwise ops    | Constant time     | Bitwise `&`, `^`, and shifts are fixed-time operations on 32-bit integers.  |
| Edge cases     | Handled inline    | The condition `(n & k) != k` filters out impossible cases without extra logic. |
| Total runtime  | Ultra-fast        | The entire process is linear in bit length and runs comfortably within constraints (`n, k ≤ 10⁶`). |
| Scalability    | Excellent         | Can be extended to 64-bit integers or batched queries with minimal changes. |
| Failure modes  | Predictable       | Only fails when `k` contains bits not present in `n`, which is explicitly checked. |

---

### 🧠 Architectural Notes

- The XOR operation isolates the exact bits that differ between `n` and `k`, allowing precise control over what needs to change.
- The feasibility check `(n & k) != k` acts as a **bitwise subset validator**, ensuring that no illegal bit creation occurs.
- The solution is **stateless**, **branchless**, and **modular**, making it ideal for reuse in bitmask-heavy systems.


---
