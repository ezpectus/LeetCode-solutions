public class Solution { 
// 🎯 Hash parameters: module and base 
// MOD is a large prime to avoid collisions 
// BASE is a prime larger than the size of the alphabet, such as 131 
const long MOD = 1000000007; 
const long BASE = 131; 

public int DeleteString(string s) { 
int n = s.Length; 

//  DP array: dp[i] = max number of operations to delete s[i..n] 
int[] dp = new int[n + 1]; 
dp[n] = 0; // basic case: an empty string requires 0 operations 

//  Arrays for prefix hashes and powers of BASE 
long[] hash = new long[n + 1]; // hash[i] = hash s[0..i-1] 
long[] pow = new long[n + 1]; // pow[i] = BASE^i 
pow[0] = 1; 

//  Construction of BASE hashes and exponents 
for (int i = 0; i < n; i++) { 
hash[i + 1] = (hash[i] * BASE + s[i]) % MOD; 
pow[i + 1] = (pow[i] * BASE) % MOD; 
} 

//  The main DP cycle: we start from the end of the term 
for (int i = n - 1; i >= 0; i--) { 
dp[i] = 1; // minimal - just delete completely 

//  We compare the segment s[i..i+j-1] with s[i+j..i+2j-1] 
for (int j = 1; j <= (n - i) / 2; j++) { 
if (GetHash(hash, pow, i, i + j - 1) == GetHash(hash, pow, i + j, i + 2 * j - 1)) { 
dp[i] = Math.Max(dp[i], 1 + dp[i + j]); // it is possible to delete the repeated segment 
    } 
  } 
} 

return dp[0]; //  Answer: maximum operations for the entire term 
} 

//  Function for quickly obtaining a hash from a substring s[l..r] inclusive 
private long GetHash(long[] hash, long[] pow, int l, int r) { 
return (hash[r + 1] - hash[l] * pow[r - l + 1] % MOD + MOD) % MOD; 
  }
}
