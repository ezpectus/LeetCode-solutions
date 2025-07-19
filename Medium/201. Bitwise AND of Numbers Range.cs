public class Solution {
    public int RangeBitwiseAnd(int left, int right) {
        /**
         * Goal: Compute bitwise AND of all integers between 'left' and 'right' (inclusive).
         * Naive approach (iterating from left to right) is too slow for large ranges.
         * Key Insight: Any differing bits in the range will collapse to 0 in AND operation.
         * So, we need to find the common bit prefix of 'left' and 'right'.
         */

        int shift = 0;

        while (left != right) {
            /**
             * Shift both numbers right until they become equal.
             * Each shift strips off the differing least significant bits.
             * We count how many shifts we make so we can recover the shared bits later.
             */
            left >>= 1;
            right >>= 1;
            shift++;
        }

        /**
         * Now 'left' holds the shared bit prefix.
         * We return it shifted back to its original position.
         * This result represents the bitwise AND of the whole range.
         */
        return left << shift;
    }
}
