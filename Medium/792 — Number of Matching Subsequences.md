# 🧩 LeetCode 792 — Number of Matching Subsequences

## 📜 Problem Statement

Given a string `s` and an array of strings `words`, return the number of words that are subsequences of `s`.

A **subsequence** of a string is a new string formed by deleting some (or no) characters from the original string without changing the relative order of the remaining characters.

### Example 1:
- Input: `s = "abcde"`, `words = ["a","bb","acd","ace"]`
- Output: `3`
- Explanation: `"a"`, `"acd"`, and `"ace"` are subsequences of `"abcde"`.

### Example 2:
- Input: `s = "dsahjpjauf"`, `words = ["ahjpjau","ja","ahbwzgqnuk","tnmlanowax"]`
- Output: `2`

---

## 📐 Constraints

- `1 <= s.length <= 5 * 10⁴`
- `1 <= words.length <= 5000`
- `1 <= words[i].length <= 50`
- All strings consist of lowercase English letters.

---

## 🧠 Architectural Insight

### 🔹 Naive Approach (Rejected)
- For each word, scan through `s` to check if it’s a subsequence.
- Time complexity: `O(len(s) × len(word))` per word → up to `250 million` operations.
- ❌ Too slow for large inputs.

### 🔹 Optimized Strategy
- Preprocess `s` by mapping each character to a list of its indices.
- For each word:
  - Traverse its characters.
  - Use **binary search** to find the next valid index in `s` that maintains order.
- This reduces the search time from linear to logarithmic per character.

---
## 🔍 How It Works

1. **Preprocessing**:
   - Build a dictionary `map` where each character in `s` maps to a list of its indices.
   - Example: for `s = "abcab"`, `map['a'] = [0, 3]`, `map['b'] = [1, 4]`, `map['c'] = [2]`.

2. **Word Matching**:
   - For each word in `words`, initialize `prev = -1`.
   - For each character `c` in the word:
     - Use binary search to find the smallest index in `map[c]` that is greater than `prev`.
     - If such index doesn’t exist → word is not a subsequence.
     - Otherwise, update `prev` to that index and continue.
   - If all characters are matched in order → increment the result counter.

3. **Why Binary Search**:
   - Each character lookup is reduced from linear to logarithmic time.
   - This makes the solution scalable for large `s` and many words.

---

## ✅ Final Code

```csharp
public class Solution {
    public int NumMatchingSubseq(string s, string[] words) {
        var map = new Dictionary<char, List<int>>();
        for (int i = 0; i < s.Length; i++) {
            if (!map.ContainsKey(s[i]))
                map[s[i]] = new List<int>();
            map[s[i]].Add(i);
        }

        int count = 0;
        foreach (var word in words) {
            int prev = -1;
            bool isSubseq = true;

            foreach (var c in word) {
                if (!map.ContainsKey(c)) {
                    isSubseq = false;
                    break;
                }

                var list = map[c];
                int idx = list.BinarySearch(prev + 1);
                if (idx < 0) idx = ~idx;
                if (idx == list.Count) {
                    isSubseq = false;
                    break;
                }

                prev = list[idx];
            }

            if (isSubseq) count++;
        }

        return count;
    }
}
```

## ⏱ Complexity

| Metric            | Value                                                              |
|-------------------|--------------------------------------------------------------------|
| **Time Complexity**   | `O(s.length + words.length × word.length × log s.length)`          |
| **Space Complexity**  | `O(s.length)` — for storing index lists of each character in `s` |

- The preprocessing step builds a dictionary mapping each character to its positions in `s`, which takes linear time.
- For each word, we scan its characters and use binary search to find the next valid position in `s`, maintaining subsequence order.
- Binary search ensures each character lookup is `O(log n)` instead of `O(n)`, making the solution efficient even for large inputs.

---

## 🧘 Key Takeaways

- The core idea is to scan each word, check character order, and count if valid.
- HashSet wouldn’t work — order matters, not just presence.
- Binary search is the accelerator, not the core logic.
- The solution was manually reconstructed — this module simply formalizes the architecture.
- This approach balances clarity and performance, making it reusable across similar problems.

---

## 📦 Module Summary

| Component         | Description                                         |
|------------------|-----------------------------------------------------|
| **Pattern**       | Indexed character mapping + binary search          |
| **Reusability**   | High — works for any ordered subsequence check     |
| **Edge Cases**    | Words with characters not in `s`, repeated letters |
| **Performance**   | Efficient for large `s` and many short words       |



---
