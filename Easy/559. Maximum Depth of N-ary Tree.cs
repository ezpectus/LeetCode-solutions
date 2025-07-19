/*
// Definition for an N-ary tree Node.
public class Node {
    public int val;
    public IList<Node> children;

    public Node() {}

    public Node(int _val) {
        val = _val;
    }

    public Node(int _val, IList<Node> _children) {
        val = _val;
        children = _children;
    }
}
*/

public class Solution {
    public int MaxDepth(Node root) {
        /**
         * Goal: Compute the maximum depth of an N-ary tree.
         * Depth = number of nodes along the longest path from root to leaf.
         * We solve this recursively: each node returns 1 + max depth of its children.
         */

        if (root == null) return 0;  /** Empty tree has depth 0 */

        int depth = 0;

        /**
         * Recursively explore each child, collecting max depth among them.
         * We compare 'depth' to each child's returned depth, and keep the largest.
         */
        foreach (Node child in root.children) {
            int dd = MaxDepth(child);       /** Recursively compute child depth */
            depth = Math.Max(depth, dd);    /** Update depth if child is deeper */
        }

        /**
         * Return depth of current node = 1 (itself) + max depth among its children.
         */
        return depth + 1;
    }
}
