public class Solution {
    // Determines if the array can be rearranged into an arithmetic progression
    public bool CanMakeArithmeticProgression(int[] arr) {
        Array.Sort(arr); // Sort the array to bring potential progression order

        // Calculate the common difference based on the first two elements
        int step = arr[1] - arr[0];

        // Iterate through the array to verify the difference holds throughout
        for (int i = 2; i < arr.Length; i++) {
            if (arr[i] - arr[i - 1] != step) {
                return false; // If any step deviates, it's not a valid progression
            }
        }

        return true; // All steps are consistent
    }
}
