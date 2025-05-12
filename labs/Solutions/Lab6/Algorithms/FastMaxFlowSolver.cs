using Lab6.Models;

namespace Lab6.Algorithms
{
    public static class FastMaxFlowSolver
    {
        public static SolveResult Solve(InputData input)
        {
            int nodeCount = input.NodeCount;

            BuildBaseCapacityAndEdgeList(input, nodeCount, out int[,] baseCapacity, out List<EdgeReference> edgeList);

            int low = 0; // min point for binary search
            int high = input.RemovalPlan.Count; // max point for binary search
            int maxFlow = 0;
            int maxRemovableEdges = -1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                int[,] currentCapacity = ApplyEdgeRemovals(baseCapacity, edgeList, input.RemovalPlan, mid);

                int flow = ComputeMaxFlowWithPreflowPush(currentCapacity, 0, nodeCount - 1);
                if (flow >= input.RequiredFlow)
                {
                    maxRemovableEdges = mid;
                    maxFlow = flow;
                    low = mid + 1; // resize the area for the next binary search iteration
                }
                else
                {
                    high = mid - 1; // resize the area for the next binary search iteration
                }
            }

            return new SolveResult(maxRemovableEdges, maxFlow);
        }

        private static void BuildBaseCapacityAndEdgeList(
            InputData input,
            int nodeCount,
            out int[,] baseCapacity,
            out List<EdgeReference> edgeList)
        {
            baseCapacity = new int[nodeCount, nodeCount];
            edgeList = new List<EdgeReference>(input.Edges.Count);

            foreach (InputEdge edge in input.Edges)
            {
                baseCapacity[edge.From, edge.To] = edge.Capacity;
                baseCapacity[edge.To, edge.From] = edge.Capacity;
                edgeList.Add(new EdgeReference(edge.From, edge.To));
            }
        }

        private static int[,] ApplyEdgeRemovals(
            int[,] baseCapacity,
            List<EdgeReference> edgeList,
            List<int> removalPlan,
            int upToCount)
        {
            int[,] modifiedCapacity = (int[,])baseCapacity.Clone();

            for (int i = 0; i < upToCount; i++)
            {
                EdgeReference edge = edgeList[removalPlan[i]];
                modifiedCapacity[edge.From, edge.To] = 0;
                modifiedCapacity[edge.To, edge.From] = 0;
            }

            return modifiedCapacity;
        }

        private static int ComputeMaxFlowWithPreflowPush(int[,] capacity, int source, int sink)
        {
            int nodeCount = capacity.GetLength(0);
            int[,] flow = new int[nodeCount, nodeCount]; // current flow on each edge
            int[] height = new int[nodeCount]; // "height" of each node, like distance from the sink (but not actual distance! just push priority)
            int[] excess = new int[nodeCount]; // how much flow is waiting at a node to be pushed out
            int[] nextNeighbor = new int[nodeCount]; // which neighbor to try pushing to next

            height[source] = nodeCount; // start at max height so that it can push immediately
            excess[source] = int.MaxValue;

            for (int target = 0; target < nodeCount; target++)
            {
                if (capacity[source, target] > 0) // non existing edges will have 0 capacity meaning we skip them
                {
                    flow[source, target] = capacity[source, target];
                    flow[target, source] = -capacity[source, target]; // for residual capacity in the reverse direction
                    excess[target] = capacity[source, target]; // now the target node has excess from what we pushed
                }
            }

            Queue<int> activeNodes = new Queue<int>();
            for (int node = 1; node < nodeCount - 1; node++) // add all nodes with excess (except source and sink) to the queue
            {
                if (excess[node] > 0)
                    activeNodes.Enqueue(node);
            }

            while (activeNodes.Count > 0)
            {
                int currentNode = activeNodes.Dequeue();
                bool pushed = false;

                // try to push flow from the current node to one if its neighbors
                for (; nextNeighbor[currentNode] < nodeCount; nextNeighbor[currentNode]++)
                {
                    int neighbor = nextNeighbor[currentNode];
                    int residualCapacity = capacity[currentNode, neighbor] - flow[currentNode, neighbor]; // can we push anything from current to neighbor

                    // if the edge from current node to neighbor doesn't exist it will have 0 capacity

                    if (residualCapacity > 0 && height[currentNode] > height[neighbor]) // check that we have capacity and the height of the nodes allow pushing
                    {
                        int delta = Math.Min(excess[currentNode], residualCapacity); // the amount to push
                        flow[currentNode, neighbor] += delta; // push that amount
                        flow[neighbor, currentNode] -= delta;
                        excess[currentNode] -= delta; // update excess for nodes
                        excess[neighbor] += delta;

                        // don't enqueue source or sink and only enqueue nodes if they just became active
                        if (neighbor != source && neighbor != sink && excess[neighbor] == delta)
                            activeNodes.Enqueue(neighbor);

                        if (excess[currentNode] == 0) // if there is no more excess to push we don't have to keep trying to push excess to neighbors
                        {
                            pushed = true;
                            break;
                        }
                    }
                }

                if (!pushed) // if we are here it means excess but if we didn't push even though there was excess to push the height was too low
                {
                    height[currentNode]++; // increase height so that we can (hopefully) push next time
                    nextNeighbor[currentNode] = 0; // reset the next neighbor value for this node so we process it again
                    activeNodes.Enqueue(currentNode); // add this again to the queue
                }

                // we can increase the height because the source starts at max height and the sink stays at 0 forever
                // this means that for every node the max height it can be raised to is 2 * V - 1 making time complexity for relabeling O(V) per node
            }

            // all valid flow originates at the source and ends at the sink
            // summing flow out of the source gives the total max flow

            int totalFlow = 0;
            for (int i = 0; i < nodeCount; i++)
                totalFlow += flow[source, i];

            return totalFlow;
        }
    }
}
