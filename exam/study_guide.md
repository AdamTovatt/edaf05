# EDAF05 Munta Notes (Grade 3)

## Table of Contents
1. [Asymptotic Notation](#asymptotic-notation)
2. [Introduction](#introduction)
3. [Data Structures](#data-structures)
4. [Greedy Algorithms](#greedy-algorithms)
5. [Shortest Paths and Minimum Spanning Trees](#shortest-paths-and-minimum-spanning-trees)
6. [Divide and Conquer and Convex Hull](#divide-and-conquer-and-convex-hull)
7. [Dynamic Programming](#dynamic-programming)
8. [NP-Completeness](#np-completeness)
9. [Linear Programming](#linear-programming)

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

### Hollow Heaps
- A type of heap data structure that supports:
  * Insert: O(1) amortized time
  * Delete-min: O(log n) amortized time
  * Decrease-key: O(1) amortized time
- Key features:
  * Multiple root nodes allowed
  * Nodes can be "hollow" (deleted) or "full" (active)
  * Links between nodes can be "solid" or "dashed"
- Operations:
  * Insert: Create new node and link to existing roots
  * Delete-min: Remove minimum root, merge its children
  * Decrease-key: Cut subtree and make it a new root
- Advantages:
  * Better theoretical bounds than Fibonacci heaps
  * Simpler implementation
  * Good for algorithms needing many decrease-key operations

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

### Bipartite Graphs
- A graph whose vertices can be divided into two sets
- No edges between vertices in the same set
- Applications:
  * Matching problems
  * Resource allocation
  * Job assignments
- How to check if a graph is bipartite:
  * Use BFS or DFS with two colors
  * Color each vertex opposite to its neighbors
  * If we can't do this, graph isn't bipartite
  * Time complexity: O(V + E)

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
    - Tarjan's Algorithm:
      * More efficient method using a single DFS pass
      * Tracks discovery time and low link value for each vertex
      * Low link = earliest vertex that can be reached from current vertex
      * When low link equals discovery time, found an SCC
      * Time complexity: O(V + E)
      * Space complexity: O(V)
      * Advantages over Kosaraju's:
        - No need for second graph traversal
        - More efficient in practice
        - Better cache behavior
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
    - Assume there's a better solution
    - Show we can exchange elements to match our greedy solution
    - Prove this doesn't make the solution worse
    - Example: In activity selection, if we can swap activities, we can always choose the one that ends first
  * Greedy stays ahead: Show that our greedy solution is always at least as good as any other solution at each step
    - Show greedy solution is at least as good at each step
    - Compare with any other solution
    - Prove it maintains this advantage
    - Example: In Huffman coding, show greedy choice leads to optimal prefix code
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
- Data structure for tracking connected components
- Operations:
  * Find: Determine which set an element belongs to
  * Union: Merge two sets
- Naïve Implementation:
  * Use arrays to represent parent relationships
  * Find: Follow parent pointers to root
  * Union: Make one root point to another
  * Problems:
    - Can degenerate into linear chains
    - Find operations become O(n)
    - Union operations can be slow
- Optimized Implementation:
  * Path Compression:
    - During Find, make all nodes point directly to root
    - Flattens the tree structure
    - Makes future operations faster
  * Union by Rank:
    - Keep track of tree heights
    - Always attach smaller tree to larger one
    - Prevents linear chains
  * Time Complexity:
    - Nearly constant time: O(α(n))
    - α(n) is inverse Ackermann function
    - Grows very slowly with n
- Applications:
  * Kruskal's algorithm for MST
  * Connected components in graphs
  * Image processing
  * Network connectivity

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
     * Time: O(n log n) due to sorting
  3. Preparata-Hong:
     * Divide-and-conquer algorithm for 3D convex hull
     * Process:
       - Split points into two halves
       - Find hulls recursively
       - Merge hulls by finding "bridge" edges
     * Time complexity: O(n log n)
     * Advantages:
       - Works in 3D
       - Efficient merging step
       - Good for parallel implementation
     * Applications:
       - 3D modeling
       - Computer graphics
       - Collision detection
- Applications:
  * Collision detection
  * Path planning
  * Pattern recognition
  * Computer graphics
- Base case: When we have 3 or fewer points, use brute force

### Finding Nearest Points
- Problem: Find the smallest distance between any two points in a 2D plane
- Divide-and-conquer approach:
  1. Sort points by x and y coordinates (O(n log n))
  2. Split points into left and right halves
  3. Find closest pairs in each half recursively
  4. Check for closer pairs that cross the dividing line
- Time complexity: O(n log n)
  * a = 2 (we split into two subproblems)
  * b = 2 (each subproblem is half the size)
  * f(n) = n (we need to check points in the strip)
  * Using Master Theorem: n^(log_2(2)) = n, and f(n) = n, so we're in case 2
  * Therefore T(n) = Θ(n log n)
- Key insight: In the strip around the dividing line, each point only needs to be compared with at most 7 other points
- Base case: When we have 3 or fewer points, use brute force

## Dynamic Programming
### Main Ideas
- What is it?
  * A way to solve problems by breaking them into smaller subproblems
  * Key idea: Store solutions to subproblems to avoid solving them again
  * Like divide-and-conquer, but subproblems overlap
- When to use it:
  * When a problem has overlapping subproblems
    - Example: Fibonacci sequence
      * To find fib(5), we need fib(4) and fib(3)
      * To find fib(4), we need fib(3) and fib(2)
      * To find fib(3), we need fib(2) and fib(1)
      * Notice how fib(3) and fib(2) are needed multiple times
      * These are overlapping subproblems - we solve the same problem multiple times
    - Compare with divide-and-conquer (like merge sort):
      * In merge sort, we split array into left and right halves
      * Each half is a unique subproblem - we never solve the same subproblem twice
      * That's why we don't need to store solutions in divide-and-conquer
  * When subproblems can be combined to solve the main problem
  * When we can write a recurrence relation
- Two approaches:
  1. Top-down (memoization):
     * Start with the main problem
     * Solve subproblems as needed
     * Store results in a table
     * Example: Fibonacci with memoization
  2. Bottom-up (tabulation):
     * Start with smallest subproblems
     * Build up to the main problem
     * Fill a table systematically
     * Example: Fibonacci with iteration
- Common patterns:
  * 1D DP: Array where each cell depends on previous cells
    - Example: Fibonacci, longest increasing subsequence
  * 2D DP: Grid where each cell depends on cells above/left
    - Example: Edit distance, longest common subsequence
- Steps to solve a DP problem:
  1. Identify the subproblems
  2. Write the recurrence relation
     - A recurrence relation is like a recipe that tells you how to solve a problem using solutions to smaller versions of the same problem
     - It has two parts:
       1. The formula that shows how to combine smaller solutions
       2. The base cases (smallest problems that we can solve directly)
     - Example 1: Fibonacci
       * Formula: fib(n) = fib(n-1) + fib(n-2)
       * Base cases: fib(0) = 0, fib(1) = 1
     - Example 2: Longest Increasing Subsequence
       * Formula: lis(i) = 1 + max(lis(j)) where j < i and array[j] < array[i]
       * Base case: lis(0) = 1
     - Example 3: Edit Distance
       * Formula: ed(i,j) = min(
           ed(i-1,j) + 1,     // delete
           ed(i,j-1) + 1,     // insert
           ed(i-1,j-1) + cost // replace
         )
       * Base case: ed(0,0) = 0
  3. Decide on the approach (top-down or bottom-up)
  4. Implement the solution
  5. Analyze time and space complexity

### Sequence Alignment
- What is it?
  * Finding the best way to align two strings by inserting gaps
  * Used to compare DNA sequences, protein sequences, or text
  * Goal: Maximize similarity or minimize cost of alignment
- How it works:
  * Use dynamic programming with a 2D table
  * Each cell (i,j) represents best alignment of first i characters of string 1 and first j characters of string 2
  * For each cell, consider three options:
    1. Match/mismatch: align current characters
    2. Gap in first string: skip character in string 1
    3. Gap in second string: skip character in string 2
- Time complexity: O(N × M) where N and M are string lengths
  * Need to fill entire table
  * Each cell takes constant time to compute
- Applications:
  * Bioinformatics: comparing DNA or protein sequences
  * Text processing: finding similar words or documents
  * Version control: finding differences between files
- Cost matrix:
  * Defines cost of matching/mismatching characters
  * Can represent biological likelihoods or edit costs
  * Example: matching 'A' with 'A' might cost less than matching 'A' with 'B'

### Network Flow
- What is it?
  * A directed graph where each edge has a capacity
  * Has a source node (where flow starts) and a sink node (where flow ends)
  * Goal: Find maximum amount of flow from source to sink
- Applications:
  * Finding maximum matching in bipartite graphs
  * Finding maximum number of edge-disjoint paths
  * Modeling traffic flow in networks
  * Finding minimum cut in a graph
  * Modeling water pipes and electrical systems
  * Ecological applications (nutrient flow)
- Ford-Fulkerson Algorithm:
  * Main idea: Keep finding paths with available capacity and add flow
  * Steps:
    1. Start with zero flow
    2. Find a path from source to sink with available capacity
    3. Add flow along this path
    4. Update residual capacities
    5. Repeat until no more paths exist
  * Residual graph:
    - Forward edges: remaining capacity
    - Backward edges: current flow (can be "undone")
  * Time complexity: O(E × max_flow)
    - E is number of edges
    - max_flow is the maximum possible flow
    - Each path finding takes O(E) time
    - We might need to find max_flow paths
  * Why it works:
    - Always finds maximum flow
    - Can "undo" flow using backward edges
    - Stops when no more paths exist (minimum cut)
- Goldberg-Tarjan (Push-Relabel) Algorithm:
  * Different approach: works with node heights and excess flow
  * Key components:
    - Height labels for nodes
    - Excess flow at each node
    - Push flow to lower neighbors
    - Relabel nodes when stuck
  * Time complexity: O(V² × E)
    - V is number of vertices
    - E is number of edges
    - Better than Ford-Fulkerson for large capacities
  * Advantages:
    - Pushes full available flow in each step
    - Not affected by large edge capacities
    - Often faster in practice
- Handling Large Capacities:
  * Why Ford-Fulkerson can be slow:
    - Basic version increments flow by 1 unit at a time
    - With large capacities (e.g., 1,000,000), needs many iterations
    - Example: If max flow is 1,000,000, might need 1,000,000 iterations
  * Solutions:
    - Capacity scaling: Start with large steps, then refine
      * Begin with largest power of 2 less than max capacity
      * Halve the step size each iteration
      * Reduces iterations from O(max_flow) to O(log max_flow)
    - Use Push-Relabel algorithm instead
      * Pushes full available capacity in each step
      * Not affected by size of capacities
    - Use binary search over possible flow values
  * When to use which algorithm:
    - Ford-Fulkerson with capacity scaling:
      * Good for sparse graphs
      * Easy to implement
      * Works well with small to medium capacities
    - Push-Relabel:
      * Better for dense graphs
      * Better for very large capacities
      * More complex to implement
      * Often faster in practice

## NP-Completeness
### What are Complexity Classes?
* Groups of problems that are similar in how hard they are to solve
* "Hard" means how much time or memory they need
* Problems in the same class can be solved in similar ways
* Like sorting problems by difficulty level

### Complexity Classes:
* P: Problems that can be solved in polynomial time
  - Example: Sorting, shortest path
* NP: "Nondeterministic Polynomial time"
  - Problems where solutions can be verified in polynomial time
  - If we could "guess" the right answer, we could verify it quickly
  - Example: Given a path, can verify if it's a Hamiltonian cycle
* NP-complete: Hardest problems in NP
  - If we could solve any NP-complete problem quickly, we could solve all NP problems quickly
  - No known polynomial-time solution exists

### Common NP-complete Problems (Grade 3):
- Vertex Cover:
  - Find smallest set of vertices that covers all edges
  - Each edge must touch at least one vertex in the set
  - Example: In a network, find minimum servers to monitor all connections
- Independent Set:
  - Find largest set of vertices with no edges between them
  - No two vertices in the set can be connected
  - Example: Find maximum number of non-conflicting tasks
- Set Cover:
  - Given a collection of sets, find smallest number of sets that cover all elements
  - Each element must be in at least one chosen set
  - Example: Choose minimum number of radio stations to cover all cities
- Traveling Salesman Problem (TSP):
  - Find shortest route visiting all cities
  - Must visit each city exactly once
  - Example: Optimize delivery route for a truck
- Graph Coloring:
  - Color vertices so no adjacent vertices have same color
  - Find minimum number of colors needed
  - Example: Schedule exams so no student has two exams at same time

### How to prove a problem is NP-complete:
1. Show it's in NP:
  * Given a solution, can verify it's correct in polynomial time
  * Example: For vertex cover, can check if chosen vertices cover all edges
2. Show it's NP-hard:
  * Take a known NP-complete problem
  * Show how to convert any instance of that problem into an instance of your problem
  * The conversion must take polynomial time

### Why it matters:
- If a problem is NP-complete, we probably can't find an efficient solution
- Instead, we need to use:
  - Approximation algorithms
  - Heuristics
  - Special cases
  - Exponential-time algorithms for small inputs

### First NP-Complete Problem
- Boolean Satisfiability (SAT) was the first problem proven NP-complete
- Problem: Given a boolean formula, is there an assignment of true/false that makes it true?
- Cook-Levin Theorem:
  * Proved SAT is NP-complete
  * Showed any NP problem can be reduced to SAT
  * Established foundation for NP-completeness theory
- Importance:
  * First concrete example of NP-complete problem
  * Used to prove other problems NP-complete
  * Basis for many practical applications

### Hamiltonian Cycle
- Problem: Find a cycle that visits each vertex exactly once
- NP-completeness proof:
  * Reduce from 3-SAT
  * Create gadget for each variable and clause
  * Show satisfiable if and only if Hamiltonian cycle exists
- Applications:
  * Circuit board testing
  * Genome sequencing
  * Network routing

### SAT Solvers
- Algorithms to solve boolean satisfiability problems
- Unit Propagation:
  * If a clause has only one literal, that literal must be true
  * Example: If (A ∨ B ∨ C) and A=false, B=false, then C must be true
  * Used to simplify formula and find forced assignments
- Applications:
  * Hardware verification
  * Software testing
  * Planning problems

## Linear Programming
### Linear and Integer Linear Programming
- What is Linear Programming?
  * A method to find the best outcome in a mathematical model
  * The model has linear relationships (straight lines)
  * Goal: Maximize or minimize a linear objective function
  * Subject to linear constraints (inequalities)

- Key Components:
  * Variables: What we're trying to find (x₁, x₂, ...)
  * Objective Function: What we want to maximize/minimize
  * Constraints: Limits on the variables
  * Feasible Region: All points that satisfy the constraints

- Example Problem:
  * Maximize: 3x + 2y
  * Subject to:
    - x + y ≤ 10
    - 2x + y ≤ 15
    - x ≥ 0
    - y ≥ 0

- Solving Methods:
  * Simplex Algorithm:
    - Moves along edges of feasible region
    - Stops at optimal vertex
    - Time complexity: Usually polynomial, but can be exponential
  * Interior Point Methods:
    - Moves through interior of feasible region
    - Often faster for large problems
    - Polynomial time complexity

- Integer Linear Programming (ILP):
  * Same as LP but variables must be integers
  * Much harder to solve than regular LP
  * Applications:
    - Scheduling problems
    - Resource allocation
    - Network design
  * Solving Methods:
    - Branch and Bound
    - Cutting Plane
    - Often use LP relaxation as starting point

- Applications:
  * Resource allocation
  * Production planning
  * Transportation problems
  * Network flow optimization
  * Portfolio optimization
  * Diet problems
  * Game theory

- Duality:
  * Every LP has a dual problem
  * Dual of maximization is minimization
  * Dual of constraints are variables
  * Strong duality: Optimal values are equal
  * Weak duality: Dual gives bound on primal

- Sensitivity Analysis:
  * How changes in parameters affect solution
  * Shadow prices: Value of relaxing constraints
  * Range of validity for current solution
  * Important for real-world applications 