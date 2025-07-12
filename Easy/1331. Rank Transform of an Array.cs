/*
Goal of the task:
- Transform the array so that each value is replaced by its "ring".
- Rank is the position of the element in the sorted array without duplicates, starting from 1.

Option 1:

1. Remove duplicates using Distinct() and convert to a list.
2. Sorted resulting list.
3. Create a dictionary rank, where the key is a number from the sorted list, and the value is its position +1.
4. Go through the original array and replace each element with its "ring" from the dictionary.
5. Return the transformed array.

Advantages:
- Lacon is coded.
- Uses LINQ to clean up duplicates (convenient for a quick solution).

Achievements:
- Distinct() and ToList() create an additional collection => a little more memory.
- May be slightly lower for very large arrays.
 
Option 2 (optimized):

1. We restore the original array to sort it without changing the original.
2. Sortable cosmetics.
3. We create a dictionary category in which we save the "bell" for each unique number.
- At the same time, we check not to specify the rank of the same thing for each case.
4. We create a new result array and replace each element of the original array with its rank.
5. Return the result.

Advantages:
- Faster and more memory efficient for large input data.
- Array.Copy and regular sorting work faster than LINQ for large volumes.

Achievements:
- Slightly more cumbersome in syntax.
- An additional result array is required (if you need to save the original obr).

General gist:
--------------------------------------------------
Both usages allow sorting and dictionary to be used to rank results.
Evaluation is in performance and most importantly code style.
*/

Algorithm 1(classic):

public class Solution {
    public int[] ArrayRankTransform(int[] arr) {
         var sorted = arr.Distinct().ToList();
         sorted.Sort();

         var rank = new Dictionary<int,int>();
         for(int i =0; i < sorted.Count;i++){
            rank[sorted[i]] = i+1;
         }
     
     for(int i =0; i < arr.Length;i++){
        arr[i] = rank[arr[i]];
     }

    return arr;


    }
}


 Algorithm 2(optimised):

public class Solution {
    public int[] ArrayRankTransform(int[] arr) {
        if (arr.Length == 0) return arr;

        int[] sorted = new int[arr.Length];
        Array.Copy(arr, sorted, arr.Length);
        Array.Sort(sorted);

        var rank = new Dictionary<int, int>();
        int currentRank = 1;

        foreach (int num in sorted) {
            if (!rank.ContainsKey(num)) {
                rank[num] = currentRank++;
            }
        }

        int[] result = new int[arr.Length];
        for (int i = 0; i < arr.Length; i++) {
            result[i] = rank[arr[i]];
        }

        return result;
    }
}
