/*
    Problem: Count the Number of Good Arrays (LeetCode #3405)

    Time complexity: O(n)
      - Precomputation of factorials and inverse factorials up to MX
      - Fast exponentiation: O(log n)
    Space complexity: O(MX) for factorial tables

    Idea:
    - We're forming arrays of length n using numbers from 1 to m.
    - A "good" array is one where exactly k indices are chosen to begin new segments,
      and each segment starts with any value (from m), rest filled independently using m - 1 choices
    - The number of ways to:
        → Choose k breakpoints among n−1 positions: comb(n−1, k)
        → Pick first element of each segment: m choices
        → Fill remaining (n−k) elements: (m−1) choices each → (m−1)^(n−k−1)

    Key methods:
    - qpow(x, n): modular fast exponentiation
    - init(): precomputes factorials and inverse factorials for nCr
    - comb(n, k): calculates C(n, k) modulo MOD using precomputed values

    Final formula:
    result = comb(n - 1, k) * m * (m - 1)^(n - k - 1) % MOD
*/


public class Solution {
    const int MOD = 1_000_000_007;
    const int MX = 100000;
    static long[] fact = new long[MX];
    static long[] invFact = new long[MX];

    long qpow(long x, int n) {
        long res = 1;
        while (n > 0) {
            if ((n & 1) == 1) {
                res = res * x % MOD;
            }
            x = x * x % MOD;
            n >>= 1;
        }
        return res;
    }

    void init() {
        if (fact[0] != 0) {
            return;
        }
        fact[0] = 1;
        for (int i = 1; i < MX; i++) {
            fact[i] = fact[i - 1] * i % MOD;
        }
        invFact[MX - 1] = qpow(fact[MX - 1], MOD - 2);
        for (int i = MX - 1; i > 0; i--) {
            invFact[i - 1] = invFact[i] * i % MOD;
        }
    }

    long comb(int n, int m) {
        return fact[n] * invFact[m] % MOD * invFact[n - m] % MOD;
    }

    public int CountGoodArrays(int n, int m, int k) {
        init();
        return (int)(comb(n - 1, k) * m % MOD * qpow(m - 1, n - k - 1) % MOD);
    }
}
