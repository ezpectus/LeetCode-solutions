/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */public class Solution {
    // Main function that starts the traversal and collects valid paths
    public IList<IList<int>> PathSum(TreeNode root, int targetSum){
        var res = new List<IList<int>>();   // Final list of all valid paths
        var path  = new List<int>();        // Temporary list tracking current path
        DFS(root, targetSum, path, res);    // Start depth-first traversal
        return res;
    }

    // Helper function for DFS + backtracking
    private void DFS(TreeNode node, int targetSum, List<int> path, List<IList<int>> res){
        if(node == null) return;            // Base case: null node, exit

        path.Add(node.val);                 // Add current node value to path
        targetSum -= node.val;             // Decrease target by current value

        // If leaf node and targetSum becomes 0 → path is valid
        if(node.left == null && node.right == null && targetSum == 0){
            res.Add(new List<int>(path));   // Store a copy of the current path
        }

        // Recurse left and right
        DFS(node.left, targetSum, path, res);
        DFS(node.right, targetSum, path, res);

        path.RemoveAt(path.Count - 1);      // Backtrack: remove last node to restore state
    }
}
