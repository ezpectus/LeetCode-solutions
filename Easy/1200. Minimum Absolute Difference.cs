public class Solution {
    public IList<IList<int>> MinimumAbsDifference(int[] arr) {
        /**
         * Step 1: Sort the array so that we can easily find the minimum absolute difference
         * between consecutive elements. In a sorted array, the closest numbers are neighbors.
         */
        Array.Sort(arr);

        /**
         * Step 2: Initialize variables:
         * - mindiff: to track the smallest absolute difference found
         * - result: to store all pairs [a, b] that have this minimum difference
         */
        int mindiff = int.MaxValue;
        List<IList<int>> result = new List<IList<int>>();

        /**
         * Step 3: Loop through the array and calculate the difference between each pair of
         * consecutive elements.
         * If we find a smaller difference, we clear the result and add the new pair.
         * If we find an equal difference, we just add the pair to the result.
         */
        for (int i = 1; i < arr.Length; i++) {
            int diff = arr[i] - arr[i - 1];

            if (diff < mindiff) {
                mindiff = diff;
                result.Clear();
                result.Add(new List<int> { arr[i - 1], arr[i] });
            } else if (diff == mindiff) {
                result.Add(new List<int> { arr[i - 1], arr[i] });
            }
        }

        /**
         * Step 4: Return the final list of pairs with the minimum absolute difference.
         */
        return result;
    }
}
