/*
    Problem: H-Index

    Time complexity: O(n log n) due to sorting
    Space complexity: O(1)

    Idea:
    - Sort the citations in descending order.
    - Traverse the sorted list and find the maximum `h` such that the researcher has at least `h` papers with `>= h` citations.
    - Stop the loop when this condition breaks.

    Example:
    citations = [3,0,6,1,5]
    After sorting: [6,5,3,1,0]
    h = 3 (because there are 3 papers with at least 3 citations)
*/

public class Solution {
    public int HIndex(int[] citations) {
        int h = 0;
        Array.Sort(citations, (a, b) => b.CompareTo(a)); // Sort descending

        for (int i = 0; i < citations.Length; i++) {
            if (citations[i] >= i + 1) {
                h++;
            } else {
                break;
            }
        }

        return h;
    }
}
