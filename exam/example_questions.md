# EDAF05 Example Questions 2023

> Note: There will be no programming questions. There may be a problem solving question but nothing complex or "tricky" if you understand the course contents.

## Algorithm Analysis and Complexity
1. Explain what O(n), Ω(n), and Θ(n) mean.

   *O(n) is an upper bound (never grows faster), Ω(n) is a lower bound (never grows slower), and Θ(n) means both bounds are tight (grows exactly at that rate).*

2. Explain what the Master theorem is about.

   *The Master theorem provides a way to solve recurrence relations of the form T(n) = aT(n/b) + f(n) by comparing the growth of f(n) with n^(log_b(a)).*

## Algorithm Design Techniques
3. Explain what is meant by a divide-and-conquer algorithm (söndra-och-härska).

   *Divide the problem into smaller subproblems, solve them recursively, and combine their solutions to solve the original problem.*

4. Explain what is meant by dynamic programming (dynamisk programmering).

   *Solving a problem by breaking it into overlapping subproblems and storing their solutions to avoid redundant calculations.*

5. Suppose you have invented a greedy algorithm that finds an optimal solution to a problem. Explain two approaches to prove its output really is optimal.

   *Use an exchange argument to show any optimal solution can be transformed into the greedy solution, or use greedy stays ahead to show the greedy solution is always at least as good as any other solution at each step.*

## Hash Tables
6. With open addressing, how can pairs be deleted?

   *Use a special marker (tombstone) to mark deleted slots, or move elements back to maintain the probe chain.*

7. What does quadratic probing (kvadratisk prövning) mean with hash tables?

   *When a collision occurs, try slots at increasing squared distances (h+1, h+4, h+9, etc.) from the original hash position.*

8. What does double hashing (dubbel hashning) mean? Why can α be larger with double hashing than with quadratic probing?

   *Use a second hash function to determine the probe sequence; it can handle higher load factors because it provides better distribution of probes.*

## Heaps and Union-Find
9. Explain how hollow heaps work. Focus on the simplest version with multiple root nodes.

   *Hollow heaps allow multiple root nodes and use "hollow" (deleted) nodes with solid/dashed links to maintain the heap property while supporting O(1) decrease-key operations.*

10. What can make the naïve version of union-find slow?

    *The naïve version can degenerate into linear chains, making find operations O(n) and union operations potentially slow.*

11. Explain how union-find can be made faster than as in the naïve version.

    *Use path compression (make all nodes point directly to root during find) and union by rank (attach smaller tree to larger one) to achieve nearly constant time operations.*

## Graph Algorithms
12. What does it mean that a directed graph is strongly connected (starkt sammankopplad), and how can you use BFS to determine if a graph is strongly connected?

    *A graph is strongly connected if every vertex can reach every other vertex; use BFS from any vertex, then reverse all edges and do BFS again from the same vertex - if all vertices are reachable in both cases, the graph is strongly connected.*

13. Explain how Tarjan's algorithm can find the strongly connected components in a directed graph.

    *Tarjan's algorithm uses a single DFS pass, tracking discovery time and low link values to identify SCCs when a vertex's low link equals its discovery time.*

14. What is a bipartite graph (bipartit graf), and how can you determine if a graph is bipartite?

    *A bipartite graph can be divided into two sets with no edges within the same set; use BFS or DFS with two colors, coloring each vertex opposite to its neighbors.*

15. Explain how Dijkstra's algorithm works and why it is correct.

    *Dijkstra's algorithm always processes the vertex with smallest known distance, updating distances to neighbors; it's correct because once a vertex is processed, its distance cannot be improved.*

16. Explain what can happen if there are negative edge weights (negativa kanter).

    *With negative weights, Dijkstra's algorithm may find incorrect shortest paths because it assumes processed vertices have final distances; use Bellman-Ford instead.*

17. Explain how the Bellman-Ford algorithm works and why it is correct.

    *Bellman-Ford relaxes all edges V-1 times; it's correct because the longest possible shortest path has V-1 edges, and after V-1 iterations, all distances must be correct.*

18. Explain what a minimum spanning tree (minimalt uppspännande träd) is and how it can be found using Prim's and Kruskal's algorithms.

    *An MST connects all vertices with minimum total edge weight; Prim's grows a single tree adding cheapest edges, while Kruskal's adds edges in order of increasing weight avoiding cycles.*

19. What is a safe edge (säker kant) for miminum spanning trees?

    *A safe edge is one that can be added to the current partial MST without creating a cycle and is part of some MST of the graph.*

## Network Flow
20. What is network flow (nätverksflöde) about? Give an example of when it can be used.

    *Network flow finds the maximum flow from source to sink in a directed graph with edge capacities; used for maximum matching, minimum cut, and traffic optimization.*

21. Explain the Ford-Fulkerson algorithm and why it is correct. What is its time complexity, and why?

    *Ford-Fulkerson repeatedly finds augmenting paths and adds flow until no more paths exist; it's correct because it always finds the maximum flow, with complexity O(E × max_flow).*

22. Explain the Goldberg-Tarjan (preflow-push) algorithm and why it is correct.

    *Goldberg-Tarjan uses node heights and excess flow, pushing flow to lower neighbors and relabeling when stuck; it's correct because it maintains the height property and terminates with maximum flow.*

## Stable Matching
23. Explain why the Gale-Shapley algorithm finds a stable matching (stabil matchning)?

    *Gale-Shapley always finds a stable matching because it continues until everyone is matched and no pair would prefer each other over their current matches.*

24. Explain the time complexity of Gale-Shapley.

    *The time complexity is O(n²) because each man proposes at most n times and there are n men.*

## Sequence Alignment
25. What is sequence alignment and how can it be done?

    *Sequence alignment finds the best way to align two strings by inserting gaps; it's done using dynamic programming to maximize similarity or minimize cost.*

## NP-Completeness
26. What does it mean that a problem is NP-complete (NP-fullständigt)?

    *A problem is NP-complete if it's in NP (solutions can be verified in polynomial time) and every NP problem can be reduced to it in polynomial time.*

27. If you want to prove that a new problem is NP-complete, how would you do?

    *Show it's in NP (verify solutions in polynomial time) and reduce a known NP-complete problem to it in polynomial time.*

28. Explain how the first NP-complete problem was shown to be NP-complete.

    *Boolean Satisfiability (SAT) was proven NP-complete by Cook-Levin theorem, showing any NP problem can be reduced to SAT in polynomial time.*

29. Explain how it can be shown that Hamiltonian cycle (Hamiltonsk cykel) is NP-complete.

    *Reduce from 3-SAT by creating gadgets for variables and clauses, showing a Hamiltonian cycle exists if and only if the formula is satisfiable.*

30. Explain how it can be shown that the Traveling salesman problem (Handelsresandeproblemet) is NP-complete.

    *Reduce from Hamiltonian cycle by setting edge weights to 1 for existing edges and 2 for non-existent edges, then solve TSP with bound n.*

31. Explain how it can be shown that graph coloring (graffärgning) is NP-complete.

    *Reduce from 3-SAT by creating a graph where each variable and its negation are connected, and each clause forms a triangle, showing 3-coloring exists if and only if the formula is satisfiable.*

## SAT Solvers and Linear Programming
32. Explain what unit propagation (unär propagering) in SAT-solvers mean.

    *If a clause has only one unassigned literal, that literal must be true to satisfy the clause.*

33. Explain what the simplex algorithm can do (but not why it works).

    *The simplex algorithm finds the optimal solution to a linear programming problem by moving along edges of the feasible region to vertices with better objective values.*

34. Explain what the branch-and-bound paradigm (förgrena-och-begränsa) is and can used exploited in integer linear programming (heltalsprogrammering).

    *Branch-and-bound solves integer programs by solving relaxed linear programs, branching on fractional variables, and using bounds to prune the search tree.*

## Computational Geometry
35. What is a convex hull?

    *The smallest convex polygon containing all points, with no interior angles greater than 180 degrees.*

36. Explain the Graham scan algorithm

    *Graham scan sorts points by polar angle, then uses a stack to build the hull by removing points that create concave angles.*

37. Explain the main ideas of the Preparata-Hong algorithm

    *Preparata-Hong is a divide-and-conquer algorithm for 3D convex hull that splits points, finds hulls recursively, and merges them efficiently.*

38. Why is it important to compare either α or β with γ first in different situations? What is likely to happen otherwise? You do not need to explain exactly when which is compared with γ first!

    *Comparing angles in a consistent order helps avoid floating-point errors and ensures numerical stability in geometric computations.*

39. How can you know if a point p is between q and r on a line?

    *Use cross product to check collinearity and dot product to check if p is between q and r on the line.*

40. How can you know the direction (left, right, or straight) when going from a point pr through ps to pt?

    *Use the cross product of vectors ps-pr and pt-ps; positive means left turn, negative means right turn, zero means straight.* 