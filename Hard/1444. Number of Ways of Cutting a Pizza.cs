public class Solution {
    /**  Problem Overview:
        Given a pizza represented as a grid of 'A' (apple) and '.' (empty),
        Cut it into k non-empty pieces such that each contains at least one apple.
        Return the total number of valid ways to do this.
    **/

    int rows, cols, MOD = 1_000_000_007;
    int[,,] memo; /** 3D DP cache: memo[r, c, remain] stores ways to cut from (r, c) with 'remain' cuts **/
    int[,] appleCount; /** Prefix sum matrix of apples in subgrid (r, c) to bottom-right **/
    string[] pizza; /** Original input grid **/

    public int Ways(string[] pizza, int k) {
        rows = pizza.Length;
        cols = pizza[0].Length;
        this.pizza = pizza;
        memo = new int[rows, cols, k];

        /** Initialize memoization array with -1 (unvisited states) **/
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                for (int rem = 0; rem < k; rem++)
                    memo[r, c, rem] = -1;

        appleCount = BuildPrefix(); /** Build prefix sum matrix **/
        return Dp(0, 0, k - 1); /** Start from top-left with k-1 remaining cuts **/
    }

    int[,] BuildPrefix() {
        /**  Reverse Prefix Sum Matrix
            Stores total apples in subrectangle starting at (r, c) to bottom-right corner.
            Enables fast apple checks for any subgrid.
        **/
        int[,] prefix = new int[rows + 1, cols + 1];

        for (int r = rows - 1; r >= 0; r--) {
            for (int c = cols - 1; c >= 0; c--) {
                int hasApple = pizza[r][c] == 'A' ? 1 : 0;
                prefix[r, c] = hasApple
                             + prefix[r + 1, c]
                             + prefix[r, c + 1]
                             - prefix[r + 1, c + 1];
            }
        }
        return prefix;
    }

    bool HasApple(int r1, int c1, int r2, int c2) {
        /**  Check if subgrid (r1, c1) to (r2, c2) contains at least one apple **/
        int totalApples = appleCount[r1, c1]
                        - appleCount[r2 + 1, c1]
                        - appleCount[r1, c2 + 1]
                        + appleCount[r2 + 1, c2 + 1];

        return totalApples > 0;
    }

    int Dp(int r, int c, int remain) {
        /**  Dp State:
            From cell (r, c), how many ways to cut the remaining pizza with 'remain' cuts
            ensuring each piece has at least one apple.
        **/

        if (remain == 0)
            return HasApple(r, c, rows - 1, cols - 1) ? 1 : 0; /** Base Case: final piece validity **/

        if (memo[r, c, remain] != -1)
            return memo[r, c, remain]; /** Memoized result **/

        int ans = 0;

        /**  Horizontal Cuts **/
        for (int nr = r + 1; nr < rows; nr++) {
            if (HasApple(r, c, nr - 1, cols - 1)) {
                ans = (ans + Dp(nr, c, remain - 1)) % MOD;
            }
        }

        /**  Vertical Cuts **/
        for (int nc = c + 1; nc < cols; nc++) {
            if (HasApple(r, c, rows - 1, nc - 1)) {
                ans = (ans + Dp(r, nc, remain - 1)) % MOD;
            }
        }

        memo[r, c, remain] = ans; /** Save result **/
        return ans;
    }
}
