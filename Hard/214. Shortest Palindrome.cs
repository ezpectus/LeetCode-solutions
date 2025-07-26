public class Solution {
    // Main function to construct the shortest palindrome by adding characters in front
    public string ShortestPalindrome(string s) {
        // Step 1: Reverse the input string
        string rev = new string(s.Reverse().ToArray());

        // Step 2: Concatenate original + separator + reversed
        // This helps us find the longest palindromic prefix using KMP
        string combined = s + "#" + rev;

        // Step 3: Build the LPS table for the combined string
        int[] lps = BuildLPS(combined);

        // Step 4: LPS last value = length of longest palindromic prefix in original string
        int palPrefixLen = lps[lps.Length - 1];

        // Step 5: Extract the suffix that needs to be mirrored to make the whole string a palindrome
        string suffix = s.Substring(palPrefixLen);

        // Step 6: Reverse the suffix and add it in front
        string reversedSuffix = new string(suffix.Reverse().ToArray());

        // Final result: reversedSuffix + original string = shortest palindrome
        return reversedSuffix + s;
    }

    // Builds the LPS (Longest Prefix Suffix) table for the string
    private int[] BuildLPS(string s) {
        int n = s.Length;
        int[] lps = new int[n];

        // len keeps track of the current matching prefix-suffix length
        int len = 0;

        for (int i = 1; i < n; i++) {
            // If mismatch occurs, fall back to shorter previous prefix
            while (len > 0 && s[i] != s[len])
                len = lps[len - 1];

            // If characters match, extend the prefix-suffix length
            if (s[i] == s[len])
                len++;

            // Set LPS for current index
            lps[i] = len;
        }

        return lps;
    }
}

