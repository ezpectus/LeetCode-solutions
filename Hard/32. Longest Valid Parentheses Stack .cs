# Intuition
The task is to find the length of the longest substring of well-formed parentheses. A valid sequence always starts with `'('` and closes with `')'`. So, we need a way to track unmatched parentheses and measure distances between matching pairs.

We can use a **stack to store indices**, which helps us compute valid segment lengths efficiently. Initially, we push `-1` as a sentinel to handle edge cases cleanly.

# Approach
- Initialize a stack and push `-1` as the base index.
- Traverse each character in the string:
  - If it's `'('`, push its index to the stack.
  - If it's `')'`:
    - Pop the stack (attempting to match the most recent `'('`).
    - If the stack is empty after popping, push the current index (this marks the start of the next possible valid substring).
    - Otherwise, calculate the length between the current index and the top of the stack, and update `maxlen`.

This way, we maintain boundaries of valid substrings and dynamically update the max length.

# Complexity
- Time complexity: $$O(n)$$ — each character is processed once.
- Space complexity: $$O(n)$$ — in the worst case, we store all opening parentheses in the stack.

# Code
```csharp
public class Solution {
    public int LongestValidParentheses(string s) {
        Stack<int> stack = new Stack<int>();
        stack.Push(-1); // Sentinel index
        int maxlen = 0;

        for (int i = 0; i < s.Length; i++) {
            if (s[i] == '(') {
                stack.Push(i); // Track opening
            } else {
                stack.Pop(); // Try to match a '('
                if (stack.Count == 0) {
                    stack.Push(i); // Reset base index
                } else {
                    maxlen = Math.Max(maxlen, i - stack.Peek()); // Valid substring length
                }
            }
        }
        return maxlen;
    }
}
