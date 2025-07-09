

```csharp
public class KthLargest
{
    private int k;
    private PriorityQueue<int, int> minheap; // MinHeap: item = val, priority = val

    public KthLargest(int k, int[] nums)
    {
        this.k = k;
        minheap = new PriorityQueue<int, int>();

        // Add all initial numbers to the heap
        foreach (int num in nums)
        {
            Add(num); // Reuse the Add logic
        }
    }

    public int Add(int val)
    {
        if (minheap.Count < k)
        {
            // Still building the heap → add directly
            minheap.Enqueue(val, val);
        }
        else if (val > minheap.Peek())
        {
            // New number is bigger than current k-th largest
            minheap.Dequeue();        // Remove smallest
            minheap.Enqueue(val, val); // Insert the new value
        }

        // Return the k-th largest (i.e., heap top)
        return minheap.Peek();
    }
}
```




