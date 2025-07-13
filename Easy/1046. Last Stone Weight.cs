public class Solution {
    public int LastStoneWeight(int[] stones) {
        // Step 1: Create a max-heap using PriorityQueue with negative priorities
        PriorityQueue<int, int> maxheap = new PriorityQueue<int, int>();

        // Step 2: Enqueue all stones with negative priority (to simulate max-heap)
        foreach (int stone in stones) {
            maxheap.Enqueue(stone, -stone);
        }

        // Step 3: Process stones until 0 or 1 remains
        while (maxheap.Count > 1) {
            int stone1 = maxheap.Dequeue();  // Heaviest stone
            int stone2 = maxheap.Dequeue();  // Second heaviest

            // Step 4: If stones are not equal, add the difference back into heap
            if (stone1 != stone2) {
                int newStone = stone1 - stone2;
                maxheap.Enqueue(newStone, -newStone);
            }
        }

        // Step 5: Return the last remaining stone or 0
        return maxheap.Count == 1 ? maxheap.Dequeue() : 0;
    }
}
