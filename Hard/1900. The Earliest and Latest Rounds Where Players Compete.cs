/*
    Problem: Earliest and Latest Rounds Where Players Meet (LeetCode #1900)

    Time complexity: Roughly exponential in worst case, but manageable due to memoization
    Space complexity: O(n^3) for DP memoization

    Idea:
    - Players are arranged from 1 to n, and they face off in mirrored pairs (i vs n+1−i) each round.
    - We want to find the earliest and latest round in which two specific players could meet.
    - Use 3D memoization arrays F and G to store earliest and latest outcomes for state (n, f, s):
        - n = number of remaining players
        - f = position of firstPlayer
        - s = position of secondPlayer
    - Base case: if f + s == n + 1 → players face off this round → return {1,1}
    - Symmetry optimization: F(n, f, s) == F(n, n+1−s, n+1−f) → reduce redundant states
    - Recursively simulate matches for left/middle vs right position scenarios
    - Track min and max round outcomes across recursive branches.

    Example:
    EarliestAndLatest(11, 2, 4) → two players may face off as early as round 3 or as late as round 4
*/

public class Solution {
    private int[,,] F = new int[30, 30, 30];
    private int[,,] G = new int[30, 30, 30];

    private int[] Dp(int n, int f, int s) {
        if (F[n, f, s] != 0) {
            return new int[] { F[n, f, s], G[n, f, s] };
        }
        if (f + s == n + 1) {
            return new int[] { 1, 1 };
        }

        // F(n,f,s) = F(n,n+1-s,n+1-f)
        if (f + s > n + 1) {
            int[] res = Dp(n, n + 1 - s, n + 1 - f);
            F[n, f, s] = res[0];
            G[n, f, s] = res[1];
            return new int[] { F[n, f, s], G[n, f, s] };
        }

        int earliest = int.MaxValue, latest = int.MinValue;
        int n_half = (n + 1) / 2;
        if (s <= n_half) {
            // On the left or in the middle
            for (int i = 0; i < f; ++i) {
                for (int j = 0; j < s - f; ++j) {
                    int[] res = Dp(n_half, i + 1, i + j + 2);
                    earliest = Math.Min(earliest, res[0]);
                    latest = Math.Max(latest, res[1]);
                }
            }
        } else {
            // s on the right
            int s_prime = n + 1 - s;
            int mid = (n - 2 * s_prime + 1) / 2;
            for (int i = 0; i < f; ++i) {
                for (int j = 0; j < s_prime - f; ++j) {
                    int[] res = Dp(n_half, i + 1, i + j + mid + 2);
                    earliest = Math.Min(earliest, res[0]);
                    latest = Math.Max(latest, res[1]);
                }
            }
        }

        F[n, f, s] = earliest + 1;
        G[n, f, s] = latest + 1;
        return new int[] { F[n, f, s], G[n, f, s] };
    }

    public int[] EarliestAndLatest(int n, int firstPlayer, int secondPlayer) {
        // F(n,f,s) = F(n,s,f)
        if (firstPlayer > secondPlayer) {
            int temp = firstPlayer;
            firstPlayer = secondPlayer;
            secondPlayer = temp;
        }

        int[] res = Dp(n, firstPlayer, secondPlayer);
        return new int[] { res[0], res[1] };
    }
}
