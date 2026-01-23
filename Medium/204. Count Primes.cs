public class Solution {
    public int CountPrimes(int n) {
        /**
         * Base case:
         * No primes exist below 2, so return early for n <= 1.
         */
        if (n == 0 || n == 1) return 0;


        /**
         * Create a boolean array where index i represents whether i is prime.
         * Initialize all entries to 'true'. We'll mark non-primes as 'false'.
         */
        bool[] isprime = Enumerable.Repeat(true, n).ToArray();

        /**
         * Main sieve loop:
         * For each number i starting from 2 up to sqrt(n),
         * mark all its multiples as non-prime (false).
         * Optimization: start marking from i * i.
         */
        for (int i = 2; i * i < n; i++) {
            for (int j = i * i; j < n; j += i) {
                isprime[j] = false;
            }
        }

        /**
         * Count how many entries from 2 to n-1 remained marked 'true' — those are primes.
         */
        int count = 0;
        for (int i = 2; i < n; i++) {
            if (isprime[i]) {
                count++;
            }
        }

        /**
         * Return the total number of primes strictly less than n.
         */
        return count;
    }
}
