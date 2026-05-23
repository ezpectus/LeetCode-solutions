# Convex Hull Trick (CHT) — Standalone

## Origin & Motivation

The **Convex Hull Trick** (CHT) is a DP optimization technique that reduces certain O(n²) DP transitions to O(n log n) or O(n) by maintaining a set of linear functions and answering "which line gives the minimum (or maximum) at query point x?" efficiently.

The name comes from the geometric interpretation: the lower envelope of a set of lines `y = m*x + b` forms a convex hull in the dual space. The minimum value at any query x is achieved by the line on this lower envelope at x.

The technique appears in competitive programming literature from the early 2000s. It applies whenever a DP recurrence has the form:

```
dp[j] = min over i < j of { dp[i] + cost(i, j) }
```

and `cost(i, j)` decomposes as `m[i] * x[j] + b[i]` — a linear function in a query value `x[j]`, with slope `m[i]` and intercept `b[i]` depending only on i.

Complexity: **O(n)** with monotone slopes and queries, **O(n log n)** otherwise.

---

## Where It Is Used

- DP on sequences where cost factors as slope × query
- Minimum cost convex hull optimization (geometry)
- Line container for online range minimum queries over lines
- Competitive programming: "hiring problem," "divide factory," slope trick
- Offline batch queries on sets of linear functions
- Li Chao tree (segment tree variant for online arbitrary queries)

---

## Problem Statement

Given n lines `L_i: y = m_i * x + b_i`, answer q queries: for each query `x_j`, find:

```
min over i of { m_i * x_j + b_i }     // minimum CHT
max over i of { m_i * x_j + b_i }     // maximum CHT
```

And the DP application: given transitions where state `j` depends on state `i` via:

```
dp[j] = min over i { dp[i] + m[i] * x[j] + b[i] }
      = min over i { (m[i]) * x[j] + (dp[i] + b[i]) }
```

Each previous state i defines a line with slope `m[i]` and intercept `dp[i] + b[i]`.

---

## Key Geometry

The lower envelope of lines is the function `E(x) = min_i(m_i * x + b_i)`. Lines that are never minimal for any x are **redundant** and can be removed. The remaining lines form a convex hull:

**Line L2 is redundant given L1 and L3 (L1 has smaller slope, L3 has larger slope):**
```
L2 is below both L1 and L3 at their intersection x* iff:
  intersect(L1, L3) <= intersect(L1, L2)
  
Intersection of lines y=m1*x+b1 and y=m2*x+b2:
  x* = (b2 - b1) / (m1 - m2)
  
bad(L1, L2, L3):
  (b3 - b1) / (m1 - m3) <= (b2 - b1) / (m1 - m2)
  ⟺  (b3 - b1)(m1 - m2) <= (b2 - b1)(m1 - m3)
```

Using cross-multiplication avoids floating point.

---

## Variants

| Variant | Slope order | Query order | Structure | Complexity |
|---|---|---|---|---|
| Offline, monotone slopes + queries | Decreasing | Increasing | Deque + pointer | O(n) |
| Offline, monotone slopes + arbitrary queries | Decreasing | Arbitrary | Deque + binary search | O(n log n) |
| Online, arbitrary slopes + queries | Arbitrary | Arbitrary | Li Chao Tree | O(n log C) |
| Online, arbitrary slopes + monotone queries | Arbitrary | Increasing | Sorted multiset | O(n log n) |

---

## Complexity Analysis

| Variant | Add line | Query | Total |
|---|---|---|---|
| Monotone slopes + queries (pointer) | O(1) amortized | O(1) amortized | O(n) |
| Monotone slopes + binary search | O(1) amortized | O(log n) | O(n log n) |
| Li Chao Tree | O(log C) | O(log C) | O(n log C) |

C = range of x coordinates.

---

## Implementation (C++)

```cpp
#include <bits/stdc++.h>
using namespace std;
using ll = long long;

const ll INF = 2e18;

// ================================================================
// LINE — y = m*x + b
// ================================================================
struct Line {
    ll m, b;
    ll eval(ll x) const { return m * x + b; }
};

// Intersection x of L1 and L2, scaled to avoid floats:
// (b2 - b1) / (m1 - m2)
// bad(L1, L2, L3): L2 is redundant given L1 and L3
// For minimum hull (slopes decreasing left to right):
bool bad_min(Line L1, Line L2, Line L3) {
    // L2 redundant if intersection(L1,L3) <= intersection(L1,L2)
    // (b3-b1)*(m1-m2) <= (b2-b1)*(m1-m3)
    return (__int128)(L3.b - L1.b) * (L1.m - L2.m)
        <= (__int128)(L2.b - L1.b) * (L1.m - L3.m);
}

// ================================================================
// CHT — Monotone slopes (decreasing), monotone queries (increasing)
// Add lines with decreasing slopes; query with increasing x.
// O(n) total.
// ================================================================
struct CHT_Mono {
    deque<Line> hull;
    int ptr = 0; // query pointer for monotone queries

    // Add line y = m*x + b. Slopes must be added DECREASING.
    void add(ll m, ll b) {
        Line L = {m, b};
        while (hull.size() >= 2 && bad_min(hull[hull.size()-2], hull[hull.size()-1], L))
            hull.pop_back();
        hull.push_back(L);
    }

    // Query minimum at x. x must be NON-DECREASING across calls.
    ll query_min(ll x) {
        while (ptr + 1 < (int)hull.size() &&
               hull[ptr+1].eval(x) <= hull[ptr].eval(x))
            ptr++;
        return hull[ptr].eval(x);
    }

    // Query minimum at arbitrary x (binary search on hull).
    ll query_min_bs(ll x) const {
        int lo = 0, hi = (int)hull.size() - 1;
        while (lo < hi) {
            int mid = (lo + hi) / 2;
            if (hull[mid].eval(x) <= hull[mid+1].eval(x)) hi = mid;
            else lo = mid + 1;
        }
        return hull[lo].eval(x);
    }
};

// ================================================================
// CHT — Maximum hull (slopes increasing, queries increasing)
// For max queries: lines with increasing slopes.
// ================================================================
bool bad_max(Line L1, Line L2, Line L3) {
    return (__int128)(L3.b - L1.b) * (L1.m - L2.m)
        >= (__int128)(L2.b - L1.b) * (L1.m - L3.m);
}

struct CHT_Max {
    deque<Line> hull;
    int ptr = 0;

    // Add line with INCREASING slope.
    void add(ll m, ll b) {
        Line L = {m, b};
        while (hull.size() >= 2 && bad_max(hull[hull.size()-2], hull[hull.size()-1], L))
            hull.pop_back();
        hull.push_back(L);
    }

    // Query maximum at NON-DECREASING x.
    ll query_max(ll x) {
        while (ptr + 1 < (int)hull.size() &&
               hull[ptr+1].eval(x) >= hull[ptr].eval(x))
            ptr++;
        return hull[ptr].eval(x);
    }

    ll query_max_bs(ll x) const {
        int lo = 0, hi = (int)hull.size() - 1;
        while (lo < hi) {
            int mid = (lo + hi) / 2;
            if (hull[mid].eval(x) >= hull[mid+1].eval(x)) hi = mid;
            else lo = mid + 1;
        }
        return hull[lo].eval(x);
    }
};

// ================================================================
// LI CHAO TREE — arbitrary slopes and queries
// Segment tree over x-domain [x_lo, x_hi].
// Each node stores the "dominant" line at the midpoint.
// O(log C) per add and query.
// ================================================================
struct LiChaoTree {
    struct Node {
        Line line;
        bool has = false;
        int left = 0, right = 0; // child node indices
    };

    vector<Node> nodes;
    ll x_lo, x_hi;

    LiChaoTree(ll x_lo, ll x_hi) : x_lo(x_lo), x_hi(x_hi) {
        nodes.push_back({}); // dummy index 0
        nodes.push_back({}); // root = index 1
    }

    int new_node() {
        nodes.push_back({});
        return (int)nodes.size() - 1;
    }

    void add_line(int v, ll lo, ll hi, Line line) {
        if (!nodes[v].has) { nodes[v].line = line; nodes[v].has = true; return; }

        ll mid = lo + (hi - lo) / 2;
        bool left_better  = line.eval(lo)  < nodes[v].line.eval(lo);
        bool mid_better   = line.eval(mid) < nodes[v].line.eval(mid);

        if (mid_better) swap(nodes[v].line, line);

        if (lo == hi) return;

        if (left_better != mid_better) {
            if (!nodes[v].left) nodes[v].left = new_node();
            add_line(nodes[v].left, lo, mid, line);
        } else {
            if (!nodes[v].right) nodes[v].right = new_node();
            add_line(nodes[v].right, mid + 1, hi, line);
        }
    }

    ll query(int v, ll lo, ll hi, ll x) {
        if (!v) return INF;
        ll res = nodes[v].has ? nodes[v].line.eval(x) : INF;
        if (lo == hi) return res;
        ll mid = lo + (hi - lo) / 2;
        if (x <= mid) return min(res, query(nodes[v].left,  lo,    mid, x));
        else          return min(res, query(nodes[v].right, mid+1, hi,  x));
    }

    void add(ll m, ll b) { add_line(1, x_lo, x_hi, {m, b}); }
    ll   query(ll x)     { return query(1, x_lo, x_hi, x);  }
};

// ================================================================
// DP EXAMPLE: Minimum cost hiring problem
// dp[j] = min over i < j of { dp[i] + a[i] * b[j] }
// Lines: slope = a[i], intercept = dp[i]
// Query point: b[j]
// Slopes a[i] must be monotone for O(n) CHT.
// ================================================================
vector<ll> solve_hiring(vector<ll>& a, vector<ll>& b) {
    int n = a.size();
    vector<ll> dp(n, INF);
    dp[0] = 0;

    CHT_Mono cht;
    cht.add(a[0], dp[0]); // line: y = a[0]*x + dp[0]

    for (int j = 1; j < n; j++) {
        dp[j] = cht.query_min(b[j]);
        if (dp[j] < INF)
            cht.add(a[j], dp[j]);
    }
    return dp;
}

// ================================================================
// Usage
// ================================================================
int main() {
    // Basic CHT: find minimum line value at each query
    {
        printf("=== CHT Monotone ===\n");
        CHT_Mono cht;
        // Lines with DECREASING slopes: 3x+1, 1x+4, -1x+9, -3x+16
        cht.add(3,  1);
        cht.add(1,  4);
        cht.add(-1, 9);
        cht.add(-3, 16);

        // Query with INCREASING x: 0,1,2,3,4,5,6
        printf("Min line value at x=0..6:\n");
        for (ll x = 0; x <= 6; x++)
            printf("  x=%lld: min=%lld\n", x, cht.query_min(x));
    }

    // Binary search queries (arbitrary order)
    {
        printf("\n=== CHT Binary Search (arbitrary queries) ===\n");
        CHT_Mono cht;
        cht.add(5,  0);
        cht.add(2,  6);
        cht.add(-1, 15);
        cht.add(-4, 24);

        for (ll x : {3LL, 0LL, 7LL, 1LL, 5LL})
            printf("  x=%lld: min=%lld\n", x, cht.query_min_bs(x));
    }

    // Li Chao Tree (arbitrary slopes and queries)
    {
        printf("\n=== Li Chao Tree ===\n");
        LiChaoTree lct(-100, 100);
        // Add lines in arbitrary order
        lct.add(3,  1);
        lct.add(-1, 9);
        lct.add(1,  4);
        lct.add(-3, 16);
        lct.add(0,  6);

        printf("Min value at various x:\n");
        for (ll x : {-5LL, 0LL, 2LL, 5LL, 10LL})
            printf("  x=%lld: min=%lld\n", x, lct.query(x));
    }

    // DP: hiring problem
    {
        printf("\n=== DP: Hiring Problem ===\n");
        // dp[j] = min over i<=j of (dp[i] + a[i]*b[j])
        // a = cost coefficients (decreasing for monotone CHT)
        // b = query values (increasing)
        vector<ll> a = {10, 8, 6, 4, 2};
        vector<ll> b = { 1, 2, 3, 4, 5};
        auto dp = solve_hiring(a, b);
        printf("dp values: ");
        for (ll x : dp) printf("%lld ", x);
        printf("\n");
    }

    // Max CHT example
    {
        printf("\n=== Max CHT ===\n");
        CHT_Max cht;
        // Lines with INCREASING slopes for max hull
        cht.add(-3, 16);
        cht.add(-1,  9);
        cht.add( 1,  4);
        cht.add( 3,  1);

        printf("Max line value at x=0..6:\n");
        for (ll x = 0; x <= 6; x++)
            printf("  x=%lld: max=%lld\n", x, cht.query_max(x));
    }

    return 0;
}
```

---

## Implementation (C#)

```csharp
using System;
using System.Collections.Generic;

// ================================================================
// CHT — Monotone slopes and queries (minimum hull)
// ================================================================
public class CHTMono {
    private record Line(long M, long B) {
        public long Eval(long x) => M * x + B;
    }

    private readonly List<Line> hull = new();
    private int ptr = 0;

    private static bool Bad(Line L1, Line L2, Line L3) =>
        (decimal)(L3.B - L1.B) * (L1.M - L2.M)
     <= (decimal)(L2.B - L1.B) * (L1.M - L3.M);

    // Add line with DECREASING slope
    public void Add(long m, long b) {
        var L = new Line(m, b);
        while (hull.Count >= 2 && Bad(hull[^2], hull[^1], L))
            hull.RemoveAt(hull.Count - 1);
        hull.Add(L);
    }

    // Query min at NON-DECREASING x
    public long QueryMin(long x) {
        while (ptr + 1 < hull.Count && hull[ptr+1].Eval(x) <= hull[ptr].Eval(x))
            ptr++;
        return hull[ptr].Eval(x);
    }

    // Binary search for arbitrary x
    public long QueryMinBS(long x) {
        int lo = 0, hi = hull.Count - 1;
        while (lo < hi) {
            int mid = (lo + hi) / 2;
            if (hull[mid].Eval(x) <= hull[mid+1].Eval(x)) hi = mid;
            else lo = mid + 1;
        }
        return hull[lo].Eval(x);
    }
}

// ================================================================
// LI CHAO TREE — arbitrary slopes and queries
// ================================================================
public class LiChaoTree {
    private class Node {
        public (long m, long b) Line;
        public bool Has;
        public Node? Left, Right;
    }

    private readonly Node root = new();
    private readonly long xLo, xHi;
    private const long Inf = long.MaxValue / 2;

    public LiChaoTree(long xLo, long xHi) { this.xLo = xLo; this.xHi = xHi; }

    private static long Eval((long m, long b) L, long x) => L.m * x + L.b;

    private void AddLine(Node node, long lo, long hi, (long m, long b) line) {
        if (!node.Has) { node.Line = line; node.Has = true; return; }

        long mid = lo + (hi - lo) / 2;
        bool leftBetter = Eval(line, lo)  < Eval(node.Line, lo);
        bool midBetter  = Eval(line, mid) < Eval(node.Line, mid);

        if (midBetter) (node.Line, line) = (line, node.Line);
        if (lo == hi) return;

        if (leftBetter != midBetter) {
            node.Left ??= new Node();
            AddLine(node.Left, lo, mid, line);
        } else {
            node.Right ??= new Node();
            AddLine(node.Right, mid+1, hi, line);
        }
    }

    private long Query(Node? node, long lo, long hi, long x) {
        if (node == null) return Inf;
        long res = node.Has ? Eval(node.Line, x) : Inf;
        if (lo == hi) return res;
        long mid = lo + (hi - lo) / 2;
        return Math.Min(res, x <= mid
            ? Query(node.Left,  lo,    mid, x)
            : Query(node.Right, mid+1, hi,  x));
    }

    public void Add(long m, long b) => AddLine(root, xLo, xHi, (m, b));
    public long Query(long x)       => Query(root, xLo, xHi, x);
}

public class Program {
    public static void Main() {
        // Monotone CHT
        var cht = new CHTMono();
        cht.Add(3,1); cht.Add(1,4); cht.Add(-1,9); cht.Add(-3,16);
        Console.WriteLine("CHT min at x=0..6:");
        for (long x=0;x<=6;x++) Console.Write($"{cht.QueryMin(x)} ");
        Console.WriteLine();

        // Li Chao Tree
        var lct = new LiChaoTree(-100, 100);
        lct.Add(3,1); lct.Add(-1,9); lct.Add(1,4); lct.Add(-3,16); lct.Add(0,6);
        Console.WriteLine("LiChao min:");
        foreach (long x in new long[]{-5,0,2,5,10})
            Console.Write($"x={x}:{lct.Query(x)} ");
        Console.WriteLine();
    }
}
```

---

## Choosing the Right Variant

```
Slopes monotone AND queries monotone?
  → CHT with deque + moving pointer    O(n) total

Slopes monotone, queries arbitrary?
  → CHT with deque + binary search     O(n log n) total

Slopes arbitrary, queries arbitrary?
  → Li Chao Tree                       O(n log C) total

Need to support deletion or updates?
  → Kinetic heaps or Li Chao on dynamic structure
```

---

## DP Template

The general pattern for applying CHT to a DP:

```
dp[0] = base_case
hull = empty CHT structure

For j = 1..n:
    // Query: dp[j] = min over added lines at query point x[j]
    dp[j] = hull.query(x[j])
    
    // Add: new line from state j for future transitions
    // slope  = m[j]   (function of j only)
    // intercept = dp[j] + extra[j]  (function of dp[j] and j)
    hull.add(m[j], dp[j] + extra[j])
```

**When to use CHT:**
- `dp[j] = min_i { f(i) * g(j) + h(i) + c(j) }`
- Factor out `c(j)` (constant for fixed j, no min needed)
- Remaining: `min_i { f(i) * g(j) + h(i) }` = min over lines `y = f(i)*x + h(i)` at `x = g(j)`
- Slope `m[i] = f(i)`, intercept `b[i] = h(i)`, query `x[j] = g(j)`

---

## Pitfalls

- **Slope ordering matches hull type** — minimum hull requires slopes in decreasing order for the deque variant. Adding slopes in increasing order builds the maximum hull. Confusing the two gives wrong answers with no runtime error.
- **Pointer never resets for monotone queries** — the `ptr` pointer in the monotone query variant advances but never goes backward. This works only if queries are non-decreasing. If the calling DP processes j in non-increasing order of `x[j]`, either reverse the processing order or use binary search.
- **bad() uses cross multiplication to avoid floats** — `(b3-b1)*(m1-m2) <= (b2-b1)*(m1-m3)` must be computed with `__int128` or `decimal` to avoid integer overflow when slopes and intercepts are large (near 10^9). Using `double` introduces precision errors that incorrectly keep or discard hull lines.
- **Li Chao Tree x-range must be fixed at construction** — the tree is built over `[x_lo, x_hi]`. Querying outside this range gives undefined behavior. When query values span a wide range (e.g., negative and positive), ensure `x_lo` and `x_hi` cover the full range before inserting any lines.
- **Empty hull query** — before any line is added, `hull.query(x)` accesses an empty container and crashes. Always add at least one base-case line before querying. In DP applications, this is the initial state: add the line corresponding to `dp[0]` before processing `j = 1`.
- **Maximum hull: slopes must be INCREASING** — the bad() check for maximum hull reverses the inequality. Additionally, the maximum hull adds lines in increasing slope order (instead of decreasing for minimum hull). Reusing the minimum CHT code with negated values (`add(-m, -b)`, negate query result`) is safer than re-implementing the bad() check.

---

## Complexity Summary

| Variant | Add | Query | Space | Use case |
|---|---|---|---|---|
| Deque + pointer | O(1) | O(1) amortized | O(n) | Monotone slopes + queries |
| Deque + binary search | O(1) | O(log n) | O(n) | Monotone slopes, arbitrary queries |
| Li Chao Tree | O(log C) | O(log C) | O(n log C) | Arbitrary slopes + queries |

---

## Conclusion

The Convex Hull Trick is the **fundamental line-query optimization** for DP problems with linear cost decomposition:

- The deque-based CHT achieves O(n) for monotone slopes with a monotone-pointer query — the tightest possible bound.
- Li Chao Tree handles arbitrary slopes and queries in O(n log C) with a clean segment-tree implementation — the most general and robust variant.
- The geometric interpretation (lower/upper envelope of lines) makes the algorithm intuitive: lines not on the envelope are permanently dominated and can be discarded.
- CHT is the inner engine of the DP Optimization Techniques framework alongside Divide & Conquer and Knuth's optimization.

**Key takeaway:**  
CHT reduces O(n²) DP transitions to O(n log n) whenever the transition `dp[j] = min_i {dp[i] + m[i]*x[j] + c}` factors the cost into a slope depending only on i and a query depending only on j. Identify the slope `m[i]`, intercept `dp[i] + b[i]`, and query point `x[j]`. Then choose: monotone slopes → deque CHT; arbitrary → Li Chao Tree.
