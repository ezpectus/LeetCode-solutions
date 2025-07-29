/*# Intuition
To check if a number is divisible by 3, we only need to check if the **sum of its digits is divisible by 3**. So the idea is: try to build the largest possible number from the given digits that satisfies this condition.

# Approach
- Sort digits in descending order to build the largest number possible.
- Compute the sum of digits.
- If the sum is already divisible by 3, we can take all digits.
- Otherwise, we remove the minimal number of digits whose removal will make the sum divisible by 3:
    - If sum % 3 == 1: remove one digit with mod 1, or two digits with mod 2.
    - If sum % 3 == 2: remove one digit with mod 2, or two digits with mod 1.
- After removing, concatenate the digits and return the result as a string.
- If all digits are zero, return `"0"`. If no valid number can be formed, return `""`.

# Complexity
- Time complexity:  
  $$O(n \log n)$$ — for sorting the digits.

- Space complexity:  
  $$O(n)$$ — to store indices to remove and build the output string.

# Code
*/


public class Solution {
    public string LargestMultipleOfThree(int[] digits) {
        int n = digits.Length;

        // Sort digits in descending order to maximize the final number
        Array.Sort(digits, (a, b) => b.CompareTo(a));

        // Calculate the total sum of digits
        int sum = digits.Sum();

        // Lists to store indices of digits based on their remainder when divided by 3
        List<int> mod0 = new(); // Digits % 3 == 0
        List<int> mod1 = new(); // Digits % 3 == 1
        List<int> mod2 = new(); // Digits % 3 == 2

        // Classify digits by mod 3
        for (int i = 0; i < digits.Length; i++) {
            int mod = digits[i] % 3;
            if (mod == 0) mod0.Add(i);
            else if (mod == 1) mod1.Add(i);
            else mod2.Add(i);
        }

        // List to track indices we need to remove
        List<int> removeIdx = new();

        // Decide which digits to remove based on sum % 3
        if (sum % 3 == 1) {
            // Try to remove one mod1 digit, otherwise two mod2 digits
            if (mod1.Count >= 1)
                removeIdx.Add(mod1[^1]); // ^1 — last element
            else if (mod2.Count >= 2)
                removeIdx.AddRange(new[] { mod2[^1], mod2[^2] });
            else
                return "";
        }
        else if (sum % 3 == 2) {
            // Try to remove one mod2 digit, otherwise two mod1 digits
            if (mod2.Count >= 1)
                removeIdx.Add(mod2[^1]);
            else if (mod1.Count >= 2)
                removeIdx.AddRange(new[] { mod1[^1], mod1[^2] });
            else
                return "";
        }

        // Build the result from digits not removed
        StringBuilder sb = new();
        for (int i = 0; i < digits.Length; i++) {
            if (!removeIdx.Contains(i))
                sb.Append(digits[i]);
        }

        string result = sb.ToString();

        // Edge case: all digits are zeros
        if (result.Length == 0) return "";
        if (result.All(c => c == '0')) return "0";
        return result;
    }
}
