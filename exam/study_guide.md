# EDAF05 Munta Notes (Grade 3)

## Table of Contents
1. [Asymptotic Notation](#asymptotic-notation)
2. [Introduction](#introduction)
3. [Data Structures](#data-structures)
4. [Greedy Algorithms](#greedy-algorithms)
5. [Shortest Paths and Minimum Spanning Trees](#shortest-paths-and-minimum-spanning-trees)
6. [Divide and Conquer](#divide-and-conquer)
7. [Dynamic Programming](#dynamic-programming)
8. [Network Flow](#network-flow)
9. [NP-Completeness](#np-completeness)
10. [Linear Programming](#linear-programming)

## Asymptotic Notation
### What do O(n), Ω(n), and Θ(n) mean?

1. **O(n) - Big O Notation**
   - Upper bound on how fast a function grows
   - f(n) = O(g(n)) means f(n) will never grow faster than g(n)
   - Example: 2n + 3 is O(n) because it grows linearly

2. **Ω(n) - Big Omega Notation**
   - Lower bound on how fast a function grows
   - f(n) = Ω(g(n)) means f(n) will never grow slower than g(n)
   - Example: 2n + 3 is Ω(n) because it can't grow slower than linear

3. **Θ(n) - Big Theta Notation**
   - When a function is both O and Ω of the same function
   - f(n) = Θ(g(n)) means f(n) is both O(g(n)) and Ω(g(n))
   - Example: 2n + 3 is Θ(n) because it's both O(n) and Ω(n)

#### Real Examples:
- **Bubble Sort**
  * Best case (sorted array): Θ(n)
  * Worst case (reverse sorted): Θ(n²)
  * Average case: Θ(n²)

- **Binary Search**
  * Best case (middle element): O(1), Ω(1)
  * Worst case (not found): Θ(log n)

- **Linear Search**
  * Best case (first element): O(1), Ω(1)
  * Worst case (last element): Θ(n)

#### Summary:
When talking about algorithms:
- O(something) tells us "it will never be slower than this" - leaving room for better performance
- Ω(something) tells us "it will never be faster than this" - giving us a lower bound on performance
- Θ(something) tells us "it will always be exactly this" - no room for improvement
- When we say an algorithm is O(something) in general, we usually mean its worst case
- We can use Θ for the whole algorithm only if all cases (best, worst, average) have the same complexity
- If best and worst cases are different, we can't use Θ for the whole algorithm

### Algorithm Analysis
#### Common Time Complexities
- O(1): Constant time (e.g., accessing array element)
- O(log n): Logarithmic time (e.g., binary search)
- O(n): Linear time (e.g., linear search)
- O(n log n): Linearithmic time (e.g., merge sort)
- O(n²): Quadratic time (e.g., bubble sort)
- O(2ⁿ): Exponential time (e.g., recursive Fibonacci)

#### Space Complexity
- Similar to time complexity but measures memory usage
- Example: O(n) space means algorithm uses memory proportional to input size

## Introduction
### Stable Matching Algorithm
The Stable Matching Algorithm solves the stable marriage problem:

Given n men and n women, where each person has ranked all members of the opposite sex in order of preference, marry the men and women together such that there are no two people of opposite sex who would both rather have each other than their current partners. When there are no such pairs of people, the set of marriages is deemed stable.

#### Key Concepts:
1. **Stable Matching**: A matching is stable if there are no pairs (A,B) where:
   - A prefers B over their current match
   - B prefers A over their current match

2. **Multiple Stable Matchings**:
   - A problem can have multiple stable matchings
   - For a pair to be unstable, BOTH must prefer each other over their current matches
   - If only one prefers the other, it's still stable

3. **Gale-Shapley Algorithm**:
   - Each man proposes to his most preferred woman who hasn't rejected him
   - Each woman accepts if:
     * She is unmatched, or
     * She prefers the new proposal over her current match
   - Process continues until everyone is matched
   - Always produces a stable matching
   - Men-optimal (or proposer-optimal)
   - Women-pessimal (or proposee-pessimal)

#### Time Complexity: O(n²)
- Each man proposes at most n times
- n men making proposals
- Total: n * n = O(n²)

## Data Structures
### Hash Tables
A hash table stores key-value pairs and provides fast lookups. It uses a hash function to convert keys into array indices.

#### How it works:
1. **Hash Function**: Converts key to a number
2. **Modulo Operation**: Converts hash to array index
3. **Collision Handling**: When two keys map to same index

#### Collision Handling Methods:
1. **Separate Chaining**
   - Each array slot holds a list
   - Colliding items go in the same list
   - Simple but can get slow with long lists

2. **Open Addressing**
   - Try to find another empty slot
   - Types of probing:
     * Linear: try next slot, then next, etc.
     * Quadratic: try slots with increasing gaps (+1, +4, +9, etc.)
     * Double hashing: use second hash function

#### Deletion in Open Addressing:
- Can't just clear slot (breaks probe chain)
- Two approaches:
  1. Use tombstone (special marker)
  2. Move items back (more complex)

#### Load Factor (α)
- α = number of items / total slots
- Affects performance and insertion success
- Different methods have different limits:
  * Separate chaining: works with α > 1
  * Linear probing: works up to α ≈ 0.7-0.9
  * Quadratic probing: keep α ≤ 0.5

#### Conclusion about α:
- Higher α means more collisions and slower operations
- Different probing methods have different maximum α values
- Double hashing can work with higher α than quadratic probing
- When α gets too high, we need to resize the table
- The choice of maximum α is a trade-off between space and time

### Graph Representations
There are two main ways to represent a graph:

1. **Adjacency Matrix**
   - 2D array where A[i][j] = 1 if there's an edge from i to j
   - A[i][j] = 0 if no edge
   - For weighted graphs: A[i][j] = weight of edge
   - Space: O(V²) where V is number of vertices
   - Good for dense graphs (many edges)
   - Fast to check if edge exists: O(1)

2. **Adjacency List**
   - Array of lists, where each list contains neighbors
   - For each vertex, store list of its adjacent vertices
   - For weighted graphs: store (vertex, weight) pairs
   - Space: O(V + E) where E is number of edges
   - Good for sparse graphs (few edges)
   - Slower to check if edge exists: O(degree of vertex)

#### Comparison:
- Matrix: Faster edge lookups, more space
- List: Less space, better for sparse graphs
- Choose based on graph density and operations needed

### Depth First Search (DFS)
- Explores as far as possible along each branch before backtracking
- Uses a stack (either explicitly or through recursion):
  * When visiting a vertex, push all its unvisited neighbors onto stack
  * Pop next vertex to visit from stack
  * This ensures we explore deep into one path before backtracking
- Time complexity: O(V + E) where V is vertices, E is edges
- Space complexity: O(V) for the stack
- Applications:
  * Finding cycles
  * Topological sorting: ordering vertices so all edges point forward
    - Only possible in directed acyclic graphs (DAGs)
    - Useful for scheduling tasks with dependencies
  * Finding strongly connected components:
    - A strongly connected component is a subgraph where every vertex can reach every other vertex
    - Example: In a social network, a group where everyone can message everyone else
    - Can be found using DFS by:
      * First DFS to get finish times (when we finish exploring all descendants of a vertex)
      * Then DFS on reversed graph in order of finish times (most recently finished first)
  * Maze solving

### Breadth First Search (BFS)
- Explores all vertices at current depth before moving deeper
- Uses a queue:
  * When visiting a vertex, add all its unvisited neighbors to queue
  * Process vertices in order they were discovered
  * This ensures we explore all vertices at current distance before moving further
- Time complexity: O(V + E) where V is vertices, E is edges
- Space complexity: O(V) for the queue
- Applications:
  * Finding shortest path in unweighted graphs
  * Checking if graph is strongly connected:
    - Do BFS from any vertex
    - Reverse all edges
    - Do BFS from same vertex
    - If all vertices are reachable in both cases, the graph is strongly connected

### Greedy Algorithms
- Make locally optimal choices at each step, hoping to find global optimum
- Key characteristics:
  * Makes decisions one at a time
  * Never reconsider previous choices
  * Always takes the best immediate option
- Common applications:
  * Scheduling problems
  * Minimum spanning trees (Prim's and Kruskal's)
  * Huffman coding
  * Activity selection
- Proving correctness:
  * Exchange argument: Show that any optimal solution can be transformed into our greedy solution
  * Greedy stays ahead: Show that our greedy solution is always at least as good as any other solution at each step
- Example: Activity Selection
  * Problem: Select maximum number of activities that don't overlap
  * Greedy choice: Always pick the activity with earliest finish time
  * Why it works: Leaves maximum time for remaining activities

## Shortest Paths and Minimum Spanning Trees
### Minimum Spanning Trees (MST)
- A tree that connects all vertices with minimum total edge weight
- Properties:
  * No cycles (by definition of a tree)
  * Connects all vertices
  * Has exactly V-1 edges (where V is number of vertices)
- Applications:
  * Network design (minimizing cost of wiring/cables)
  * Road planning
  * Clustering
  * Any scenario where you need to connect all points with minimum total cost
- Key characteristic: Exactly one unique path between any two vertices
- If an edge fails in an MST, the network becomes disconnected (no redundancy)

### Dijkstra's Algorithm
- Finds shortest paths from a source vertex to all other vertices
- Key idea: Always process the vertex with smallest known distance
- Process:
  1. Initialize distances: 0 for source, ∞ for all others
  2. Use priority queue to track vertices by their current distance
  3. For each vertex:
     * Process unvisited neighbors
     * Update distances if we find a shorter path
     * Mark current vertex as visited
- Properties:
  * Works only with non-negative edge weights
  * Guarantees shortest path when all weights are positive
  * Uses a priority queue for efficiency
- Time complexity: O((V + E)log V)
  * V vertices and E edges
  * log V from priority queue operations
- Space complexity: O(V) for distances and priority queue

### Jarnik's Algorithm (Prim)
- Builds MST by growing a single tree from a starting vertex
- Key idea: Always add the cheapest edge that connects to a new vertex
- Process:
  1. Start with any vertex
  2. Keep track of cheapest edge to each unvisited vertex
  3. At each step:
     * Add vertex with cheapest connecting edge
     * Update costs to unvisited neighbors
     * Mark vertex as visited
- Properties:
  * Always maintains a single connected tree
  * Guarantees minimum total weight
  * Similar to Dijkstra's but focuses on edge weights, not path lengths
- Implementation:
  * Uses priority queue to track cheapest edges
  * Tracks which vertices are in the tree
  * Updates costs when new vertex is added
- Time complexity: O((V + E)log V)
  * V vertices and E edges
  * log V from priority queue operations
- Space complexity: O(V) for costs and priority queue

### Kruskal's Algorithm
- Builds MST by adding edges in order of increasing weight
- Key idea: Always add the smallest edge that doesn't create a cycle
- Process:
  1. Sort all edges by weight
  2. Start with empty graph
  3. For each edge in sorted order:
     * Add edge if it doesn't create a cycle
     * Skip edge if it would create a cycle
- Implementation details:
  * Use Union-Find data structure to track connected components
  * Path compression and union by rank for efficiency
  * Each edge contains: two vertices and weight
- Why it works:
  * Always picks smallest available edge
  * Skips edges that would create cycles
  * This guarantees minimal total weight
  * Ensures all vertices are connected
- Time complexity: O(M log M) where M is number of edges
  * Dominated by sorting edges
  * Sorting works by repeatedly splitting list in middle (log M steps)
  * At each step processes all M edges
  * Union-Find operations are nearly constant time due to path compression
- Properties:
  * Can handle disconnected components initially
  * Works with any graph structure
  * Produces same total weight as Prim's (though different edges)
- Real-world considerations:
  * If an edge fails, network becomes disconnected
  * No redundancy in final tree
  * Need to recompute if edges change
- Applications:
  * Network design (minimizing cost of wiring/cables)
  * Clustering
  * Road planning
  * Any scenario where you need to:
    - Connect all points
    - Minimize total cost
    - Have exactly one unique path between any two points

### Union-Find
- Data structure used to keep track of connected components in a graph
- Key operations:
  * Find: Determine which component a vertex belongs to
  * Union: Connect two components
- Implementation:
  * Path compression: Flatten the structure when Find is called
  * Union by rank: Connect smaller tree under larger tree
- Time complexity: O(α(n)) where α(n) is the inverse Ackermann function
  * Nearly constant time for practical graph sizes

## Divide and Conquer and Convex Hull
### Divide and Conquer
- Key steps:
  1. Divide: Split problem into smaller instances
  2. Conquer: Solve subproblems recursively
  3. Combine: Merge solutions into final answer
- Common examples:
  * Merge sort
  * Quick sort
  * Binary search
  * Finding closest pair of points
- Master Theorem:
  * When we solve a problem by breaking it into smaller pieces, we often get a pattern like:
    - To solve a problem of size n, we need to solve some number of subproblems (let's call this number a)
    - Each of these subproblems is of size n/b (where b is how much we divide the problem size by)
    - Plus we need to do some extra work f(n) to combine the results
  * This pattern is called a recurrence relation
  * Example: In merge sort:
    - We split array in half (so b = 2, because we divide size by 2)
    - We solve two subproblems (so a = 2):
      * Sort the left half of the array
      * Sort the right half of the array
    - We merge the sorted halves (which takes n time, so f(n) = n)
    - So T(n) = 2T(n/2) + n
  * Different example: In binary search:
    - We split array in half (so b = 2)
    - We solve only one subproblem (so a = 1):
      * Search in either the left half OR the right half
      * We don't need to search both halves because we can determine which half to look in
    - We do constant work to compare and choose which half (so f(n) = 1)
    - So T(n) = T(n/2) + 1
  * The Master Theorem formula:
    - If T(n) = aT(n/b) + f(n) where:
      * a ≥ 1 (we must have at least one subproblem)
      * b > 1 (we must divide the problem into smaller pieces)
      * f(n) is asymptotically positive (f(n) is positive for most values of n, especially the large ones we care about)
    - Then T(n) is one of these three cases:
      1. If the extra work f(n) is much smaller than n^(log_b(a)), then T(n) = Θ(n^(log_b(a)))
         - Example: In binary search, f(n) = 1 is much smaller than n^(log_2(1)) = 1, so T(n) = Θ(log n)
      2. If the extra work f(n) is about the same size as n^(log_b(a)), then T(n) = Θ(n^(log_b(a)) * log n)
         - Example: In merge sort, f(n) = n is about the same as n^(log_2(2)) = n, so T(n) = Θ(n log n)
      3. If the extra work f(n) is much larger than n^(log_b(a)), then T(n) = Θ(f(n))
         - Example: If f(n) = n², which is much larger than n^(log_2(2)) = n, then T(n) = Θ(n²)
  * Master Theorem helps us quickly find the time complexity without solving the recurrence
  * Common patterns to recognize:
    - If we split into a parts and each part is 1/b of the size:
      * a = number of subproblems
      * b = how much smaller each subproblem is
    - f(n) = extra work at each step (like merging in merge sort)
  * Quick way to use Master Theorem:
    1. Write down a, b, and f(n)
    2. Calculate n^(log_b(a))
    3. Compare f(n) with n^(log_b(a))
    4. Pick the matching case

### Convex Hull
- Smallest convex polygon containing all points
- Properties:
  * All points lie on or inside the hull
  * No interior angles > 180°
  * Minimum perimeter enclosing all points
- Algorithms:
  1. Jarvis March (Gift Wrapping):
     * Start with leftmost point
     * Find point with smallest angle
     * Continue until back to start
     * Time: O(nh) where h is hull size
  2. Graham Scan:
     * Sort points by polar angle
     * Use stack to build hull
     * Remove points that create concave angles
     * Time: O(n log n) for sorting
  3. Preparata-Hong:
     * Divide points into two sets
     * Find hulls recursively
     * Merge hulls efficiently
     * Time: O(n log n)

### Finding Nearest Points
- Problem: Find closest pair of points in plane
- Divide and conquer approach:
  1. Sort points by x-coordinate
  2. Divide into left and right halves
  3. Find closest pairs in each half
  4. Check points near dividing line
- Time complexity: O(n log n)
- Key insight: Only need to check points within strip of width 2d
  where d is minimum distance found so far

## Dynamic Programming
### Main Ideas
[To be filled with your understanding]

### Sequence Alignment
[To be filled with your understanding]

## Network Flow
### Ford-Fulkerson Algorithm
- Main ideas
- Time complexity

### Bipartite Graph Matching
[To be filled with your understanding]

## NP-Completeness
### Complexity Classes
[To be filled with your understanding]

### Key Problems
- Vertex Cover
- Independent Set
- Set Cover
- Traveling Salesperson Problem
- Graph Coloring

## Linear Programming
### Linear and Integer Linear Programming
- Main ideas
[To be filled with your understanding] 