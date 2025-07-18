/*
    Problem: Number of Unplaced Fruits (LeetCode #3046)

    Time complexity: O(n^2)
    Space complexity: O(n)

    Idea:
    - Try to place each fruit in any basket that can hold it and hasn't been used yet.
    - Mark a basket as used once a fruit is placed in it.
    - If a fruit cannot be placed, increment the result counter.
    - Return the total number of fruits that couldn't be placed.

    Example:
    fruits = [1,3,5], baskets = [2,4,2]
    → Place 1 → basket[0] (2)
    → Place 3 → basket[1] (4)
    → Place 5 → no basket ≥ 5 left → unplaced += 1
    → result: 1
*/


public class Solution {
    public int NumOfUnplacedFruits(int[] fruits, int[] baskets) {
        int n = fruits.Length;
        bool[] used  = new bool[n];
        int res = 0;

        for(int i = 0; i < n;i++){
            bool place = false;

            for(int j = 0; j < n;j++){
                if(!used[j] && baskets[j] >= fruits[i]){
                    used[j] = true;
                     place = true;
                     break;
                }
            }
            if(!place) res++;
        }
        
     return res;

    }
}
