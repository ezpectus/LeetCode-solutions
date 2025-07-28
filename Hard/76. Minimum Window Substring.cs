public class Solution {
    public string MinWindow(string s, string t) {
        if (s.Length < t.Length) return "";

        var need = new Dictionary<char, int>(); // /** frequency of chars in t **/
        foreach (var c in t) {
            if (!need.ContainsKey(c)) need[c] = 0;
            need[c]++;
        }

        int left = 0, right = 0;
        int minLen = int.MaxValue;
        int start = 0;
        int match = 0;
        var window = new Dictionary<char, int>(); // /** frequency of current window **/

        while (right < s.Length) {
            char c = s[right];
            if (need.ContainsKey(c)) {
                if (!window.ContainsKey(c)) window[c] = 0;
                window[c]++;
                if (window[c] == need[c]) match++; // /** char meets requirement **/
            }

            // shrink window when all required chars matched
            while (match == need.Count) {
                if (right - left + 1 < minLen) {
                    minLen = right - left + 1;
                    start = left;
                }
                char lc = s[left];
                if (need.ContainsKey(lc)) {
                    window[lc]--;
                    if (window[lc] < need[lc]) match--; // /** char no longer satisfies **/
                }
                left++; // /** shrink **/
            }
            right++; // /** expand **/
        }

        return minLen == int.MaxValue ? "" : s.Substring(start, minLen);
    }
}
