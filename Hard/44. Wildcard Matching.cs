/*
    Problem: Wildcard Matching (LeetCode #44)

    Time complexity: O(m * n)
    Space complexity: O(m * n)

    Idea:
    - Use dynamic programming to check if string `s` matches pattern `p`.
    - Pattern `p` may include:
        → `?` that matches any single character
        → `*` that matches any sequence of characters (including empty)
    
    Strategy:
    - Define dp[i][j] as: whether `s[0..i-1]` matches `p[0..j-1]`
    - dp[0][0] = true (empty string matches empty pattern)
    - For first row (i=0), allow `*` to represent empty string: dp[0][j] = dp[0][j-1] if p[j-1] == '*'
    - Transition logic:
        → If p[j−1] == '*' → match zero (`dp[i][j-1]`) or match one+ characters (`dp[i-1][j]`)
        → If p[j−1] == '?' or s[i−1] == p[j−1] → carry over dp[i−1][j−1]
        → Else → no match

    Final result: dp[m][n], whether full `s` matches full `p`

    Example:
    s = "abc", p = "a*"
    → dp[3][2] = true → because '*' can absorb 'bc'
*/


public class Solution {
    public bool IsMatch(string s, string p) {
        int n = p.Length;
        int m = s.Length;
        bool[,] dp = new bool[m+1, n+1];
         dp[0,0] = true;

         for(int t = 1; t <= n;t++){
            if(p[t-1] == '*'){
                dp[0,t] = dp[0,t-1];
            }
         }


  for(int i =1;i <= m;i++){
    for(int j = 1; j <= n;j++){
        if(p[j-1] == '*'){
            dp[i,j] = dp[i-1,j] || dp[i,j-1];
        } else if(p[j-1] == '?' || s[i-1] == p[j-1]){
            dp[i,j] = dp[i-1,j-1];
        }
     }
  }
   return dp[m,n];

    }
}
