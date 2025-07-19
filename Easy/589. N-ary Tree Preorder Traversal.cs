/* 
// Definition for a Node in an N-ary tree.
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
    public IList<int> Preorder(Node root) {
        /**
         * Goal: Perform a preorder traversal of an N-ary tree.
         * Preorder means: visit root first, then traverse children left to right.
         * We use an explicit stack to simulate recursion and track nodes.
         */

        var result = new List<int>();

        // Edge case: if tree is empty, return empty list.
        if (root == null) return result;

        // Stack to simulate DFS traversal.
        var stack = new Stack<Node>();
        stack.Push(root);

        while (stack.Count > 0) {
            // Pop current node and add its value to result list.
            var curr = stack.Pop();
            result.Add(curr.val);

            /**
             * Push children in reverse order (right to left) onto the stack.
             * This ensures the leftmost child is processed first in preorder.
             */
            for (int i = curr.children.Count - 1; i >= 0; i--) {
                stack.Push(curr.children[i]);
            }
        }

        /**
         * Return list of node values in preorder sequence.
         */
        return result;
    }
}
