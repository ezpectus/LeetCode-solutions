public class Solution {
    public string RemoveKdigits(string num, int k) {
        Stack<char> stack = new Stack<char>();

        foreach (char digit in num) {
            // While the current digit is smaller than the top of the stack,
            // and we still have digits to remove, pop the top (greedy).
            while (stack.Count > 0 && k > 0 && stack.Peek() > digit) {
                stack.Pop();
                k--;
            }
            stack.Push(digit);
        }

        // If we still have digits to remove, remove them from the end.
        while (k > 0) {
            stack.Pop();
            k--;
        }

        // Build the result by reversing the stack.
        var arr = stack.Reverse().ToArray();

        // Remove leading zeros.
        int i = 0;
        while (i < arr.Length && arr[i] == '0') i++;

        // If result is empty, return "0", else the substring.
        string result = new string(arr, i, arr.Length - i);
        return result == "" ? "0" : result;
    }
}
