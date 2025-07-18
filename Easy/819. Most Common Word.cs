/*
    Problem: Most Common Word (LeetCode #819)

    Time complexity: O(n), where n is the number of characters in the paragraph
    Space complexity: O(m), where m is the number of unique non-banned words

    Idea:
    - Normalize the paragraph by removing punctuation and converting to lowercase.
    - Split the cleaned string into individual words.
    - Use a HashSet for quick lookup of banned words.
    - Count frequency of each non-banned word using a dictionary.
    - Return the word with the highest frequency.

    Example:
    paragraph = "Bob hit a ball, the hit BALL flew far after it was hit."
    banned = ["hit"]
    → cleaned = "bob hit a ball the hit ball flew far after it was hit"
    → frequencies: {"ball": 2, "bob": 1, ...}
    → result: "ball"
*/

using System.Text.RegularExpressions;

public class Solution {
    public string MostCommonWord(string paragraph, string[] banned) {
        var bannedSet = new HashSet<string>(banned);
        var freq = new Dictionary<string, int>();

        // Remove punctuation and convert to lowercase
        var cleaned = Regex.Replace(paragraph, @"[^\w\s]", " ").ToLower();

        // Split into words
        var words = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Count non-banned word frequencies
        foreach (var word in words) {
            if (!bannedSet.Contains(word)) {
                freq[word] = freq.GetValueOrDefault(word, 0) + 1;
            }
        }

        // Find the most frequent valid word
        string ans = "";
        int count = 0;
        foreach (var kvp in freq) {
            if (kvp.Value > count) {
                count = kvp.Value;
                ans = kvp.Key;
            }
        }

        return ans;
    }
}
