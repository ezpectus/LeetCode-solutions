/* Problem: Prefixes Divisible By 5 (LeetCode #1018)

Time complexity: O(n) Space complexity: O(n) — storing n boolean results

Goal: - You're given a binary array nums[] (containing only 0 and 1). - 
At each step i, nums[0..i] forms a binary number. - 
Return a boolean list: true if this number is divisible by 5.

Strategy: - Build the number incrementally using bits: - num = num * 2 + nums[i] → classic left-shift and append bit - 
But we don't need the full number — just its remainder modulo 5. -
Using mod properties, we maintain num % 5 at each step to avoid overflow. - 
If num % 5 == 0 → current prefix is divisible by 5 → append true. */

public class Solution {
    public IList<bool> PrefixesDivBy5(int[] nums) {
        List<bool> ans  = new List<bool>();
        int num = 0;

        for(int i = 0; i < nums.Length;i++){
               num = (num * 2 + nums[i]) % 5;

               if(num == 0){
                ans.Add(true);
               } else{
                ans.Add(false);
               }
        }
      return ans;

    }
}
