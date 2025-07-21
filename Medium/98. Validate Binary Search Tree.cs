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
 */


public class Solution {
    // Entry function: validates if the entire tree is a BST
    public bool IsValidBST(TreeNode root) {
        // Start validation with the widest possible range
        return Validate(root, long.MinValue, long.MaxValue);
    }

    // Recursive helper function to check if current node respects BST rules
    private bool Validate(TreeNode node, long min, long max){
        if (node == null) return true; // Null subtree is always valid

        // If current node violates range constraints → not a BST
        if (node.val <= min || node.val >= max) return false;

        // Recurse left with updated max boundary
        // Recurse right with updated min boundary
        return Validate(node.left, min, node.val) &&
               Validate(node.right, node.val, max);
    }
}
