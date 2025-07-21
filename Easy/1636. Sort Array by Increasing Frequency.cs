public class Solution {
    // Sorts the array by frequency (ascending), and by value (descending) for ties
    public int[] FrequencySort(int[] nums) {
        // Count frequencies using a dictionary
        Dictionary<int, int> freq = new Dictionary<int, int>();
        foreach (int num in nums) {
            freq[num] = freq.GetValueOrDefault(num, 0) + 1;
        }

        // Sort using a custom comparator:
        // - First by frequency (ascending)
        // - Then by value (descending) if frequencies are equal
        Array.Sort(nums, (a, b) =>
            freq[a] != freq[b]
                ? freq[a].CompareTo(freq[b])
                : b.CompareTo(a)
        );

        return nums;
    }
}
