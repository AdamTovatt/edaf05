# EDAF05 Example Questions 2023

> Note: There will be no programming questions. There may be a problem solving question but nothing complex or "tricky" if you understand the course contents.

## Algorithm Analysis and Complexity
1. Explain what O(n), Ω(n), and Θ(n) mean.
2. Explain what the Master theorem is about.

## Algorithm Design Techniques
3. Explain what is meant by a divide-and-conquer algorithm (söndra-och-härska).
4. Explain what is meant by dynamic programming (dynamisk programmering).
5. Suppose you have invented a greedy algorithm that finds an optimal solution to a problem. Explain two approaches to prove its output really is optimal.

## Hash Tables
6. With open addressing, how can pairs be deleted?
7. What does quadratic probing (kvadratisk prövning) mean with hash tables?
8. What does double hashing (dubbel hashning) mean? Why can α be larger with double hashing than with quadratic probing?

## Heaps and Union-Find
9. Explain how hollow heaps work. Focus on the simplest version with multiple root nodes.
10. What can make the naïve version of union-find slow?
11. Explain how union-find can be made faster than as in the naïve version.

## Graph Algorithms
12. What does it mean that a directed graph is strongly connected (starkt sammankopplad), and how can you use BFS to determine if a graph is strongly connected?
13. Explain how Tarjan's algorithm can find the strongly connected components in a directed graph.
14. What is a bipartite graph (bipartit graf), and how can you determine if a graph is bipartite?
15. Explain how Dijkstra's algorithm works and why it is correct.
16. Explain what can happen if there are negative edge weights (negativa kanter).
17. Explain how the Bellman-Ford algorithm works and why it is correct.
18. Explain what a minimum spanning tree (minimalt uppspännande träd) is and how it can be found using Prim's and Kruskal's algorithms.
19. What is a safe edge (säker kant) for miminum spanning trees?

## Network Flow
20. What is network flow (nätverksflöde) about? Give an example of when it can be used.
21. Explain the Ford-Fulkerson algorithm and why it is correct. What is its time complexity, and why?
22. Explain the Goldberg-Tarjan (preflow-push) algorithm and why it is correct.

## Stable Matching
23. Explain why the Gale-Shapley algorithm finds a stable matching (stabil matchning)?
24. Explain the time complexity of Gale-Shapley.

## Sequence Alignment
25. What is sequence alignment and how can it be done?

## NP-Completeness
26. What does it mean that a problem is NP-complete (NP-fullständigt)?
27. If you want to prove that a new problem is NP-complete, how would you do?
28. Explain how the first NP-complete problem was shown to be NP-complete.
29. Explain how it can be shown that Hamiltonian cycle (Hamiltonsk cykel) is NP-complete.
30. Explain how it can be shown that the Traveling salesman problem (Handelsresandeproblemet) is NP-complete.
31. Explain how it can be shown that graph coloring (graffärgning) is NP-complete.

## SAT Solvers and Linear Programming
32. Explain what unit propagation (unär propagering) in SAT-solvers mean.
33. Explain what the simplex algorithm can do (but not why it works).
34. Explain what the branch-and-bound paradigm (förgrena-och-begränsa) is and can used exploited in integer linear programming (heltalsprogrammering).

## Computational Geometry
35. What is a convex hull?
36. Explain the Graham scan algorithm
37. Explain the main ideas of the Preparata-Hong algorithm
38. Why is it important to compare either α or β with γ first in different situations? What is likely to happen otherwise? You do not need to explain exactly when which is compared with γ first!
39. How can you know if a point p is between q and r on a line?
40. How can you know the direction (left, right, or straight) when going from a point pr through ps to pt? 