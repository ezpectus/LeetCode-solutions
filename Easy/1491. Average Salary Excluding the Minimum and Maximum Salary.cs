public class Solution {
    // Calculates average salary excluding the minimum and maximum
    public double Average(int[] salary) {
        int min = int.MaxValue;   // Track the smallest salary
        int max = int.MinValue;   // Track the largest salary
        int sum = 0;              // Sum of all salaries

        // Iterate through the salary array
        foreach (int num in salary) {
            sum += num;               // Add current salary to total
            if (num > max) max = num; // Update max if necessary
            if (num < min) min = num; // Update min if necessary
        }

        // Subtract min and max from the sum and divide by (n - 2)
        return (double)(sum - min - max) / (salary.Length - 2);
    }
}
