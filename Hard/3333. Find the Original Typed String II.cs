/*
    Problem: Count of Possible Strings Given Group Limits (LeetCode #2846)

    Time complexity: O(n + k^2), where n = word length and k = group constraint
    Space complexity: O(k)

    Idea:
    - Given a string of lowercase letters, split it into groups of consecutive identical characters.
    - Goal: count how many different strings can be formed by choosing at most k groups,
      and keeping their internal order.

    Step 1:
    - Split the input string into blocks of repeated letters → store their sizes in `freq`.
    - Multiply all group sizes together as `ans` → total combinations using full set of groups.

    Step 2:
    - If number of blocks ≥ k → return ans (no restriction).
    - Else → use dynamic programming to count how many combinations exceed the group limit `k`.

    DP:
    - f[j]: number of ways to pick j blocks without violating constraints
    - g[j]: prefix sums for quick computation of f
    - For each group size freq[i], update f and g iteratively.

    Final result:
    → Subtract invalid combinations from total: (ans - g[k - 1] + mod) % mod
*/

public class Solution {
    private const int mod = 1000000007;

    public int PossibleStringCount(string word, int k) {
        int n = word.Length;
        int cnt = 1;
        List<int> freq = new List<int>();

        for (int i = 1; i < n; ++i) {
            if (word[i] == word[i - 1]) {
                ++cnt;
            } else {
                freq.Add(cnt);
                cnt = 1;
            }
        }
        freq.Add(cnt);
        long ans = 1;
        foreach (int o in freq) {
            ans = ans * o % mod;
        }
        if (freq.Count >= k) {
            return (int)ans;
        }

        int[] f = new int[k];
        int[] g = new int[k];
        f[0] = 1;
        Array.Fill(g, 1);
        for (int i = 0; i < freq.Count; ++i) {
            int[] f_new = new int[k];
            for (int j = 1; j < k; ++j) {
                f_new[j] = g[j - 1];
                if (j - freq[i] - 1 >= 0) {
                    f_new[j] = (f_new[j] - g[j - freq[i] - 1] + mod) % mod;
                }
            }

            int[] g_new = new int[k];
            g_new[0] = f_new[0];
            for (int j = 1; j < k; ++j) {
                g_new[j] = (g_new[j - 1] + f_new[j]) % mod;
            }

            f = f_new;
            g = g_new;
        }

        return (int)((ans - g[k - 1] + mod) % mod);
    }
}

