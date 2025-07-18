/*
    Problem: Uncommon Words from Two Sentences (LeetCode #884)

    Time complexity: O(n), where n is the total number of words in both sentences
    Space complexity: O(m), where m is the number of unique words

    Idea:
    - Split both sentences into words.
    - Count frequency of each word across both sentences.
    - A word is considered "uncommon" if it appears exactly once overall.
    - Return a list of all such uncommon words.

    Example:
    s1 = "this apple is sweet"
    s2 = "this apple is sour"
    → freq: {"this":2, "apple":2, "is":2, "sweet":1, "sour":1}
    → result: ["sweet", "sour"]
*/

public class Solution {
    public string[] UncommonFromSentences(string s1, string s2) {
        var freq = new Dictionary<string, int>();

        foreach (var word in s1.Split(' ')) {
            freq[word] = freq.GetValueOrDefault(word, 0) + 1;
        }

        foreach (var word in s2.Split(' ')) {
            freq[word] = freq.GetValueOrDefault(word, 0) + 1;
        }

        var result = new List<string>();
        foreach (var kvp in freq) {
            if (kvp.Value == 1) {
                result.Add(kvp.Key);
            }
        }

        return result.ToArray();
    }
}
