public class Solution {
    public int ShortestPathLength(int[][] graph) {
        int n = graph.Length;
        int target = (1 << n) - 1; // /** Bitmask: all nodes visited **/

        var queue = new Queue<(int node, int mask, int steps)>(); // /** BFS state: (current node, visited nodes mask, steps taken) **/
        var visited = new bool[n, 1 << n]; // /** visited[node, mask] to prevent cycles **/

        // /** Start BFS from every node **/
        for (int i = 0; i < n; i++) {
            queue.Enqueue((i, 1 << i, 0)); // /** Start at node i, visited only itself **/
            visited[i, 1 << i] = true;
        }

        while (queue.Count > 0) {
            var (node, mask, steps) = queue.Dequeue();

            if (mask == target) return steps; // /** All nodes visited **/

            foreach (int neigh in graph[node]) {
                int newmask = mask | (1 << neigh); // /** Visit new neighbor **/

                if (!visited[neigh, newmask]) {
                    visited[neigh, newmask] = true;
                    queue.Enqueue((neigh, newmask, steps + 1));
                }
            }
        }

        return -1; // /** Should not reach here **/
    }
}
