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
    // Pointers to the misplaced nodes
    private TreeNode first = null;
    private TreeNode second = null;
    private TreeNode prev = null;

    // Main function to recover the BST
    public void RecoverTree(TreeNode root) {
        DFS(root); // In-order traversal to identify swapped nodes

        // Swap the values of the misplaced nodes to fix the tree
        int temp = first.val;
        first.val = second.val;
        second.val = temp;
    }

    // In-order traversal to detect anomalies
    private void DFS(TreeNode node){
        if (node == null) return;

        DFS(node.left); // Traverse left subtree

        // Detect violation of BST property: previous node > current node
        if (prev != null && prev.val > node.val) {
            if (first == null) first = prev; // First anomaly detected
            second = node; // Second node might be updated multiple times
        }

        prev = node; // Move forward in in-order traversal
        DFS(node.right); // Traverse right subtree
    }
}
