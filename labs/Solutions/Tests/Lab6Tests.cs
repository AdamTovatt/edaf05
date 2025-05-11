using Common;
using Common.DataSources;
using Lab6;
using Lab6.Algorithms;
using Lab6.Models;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class Lab6Tests
    {
        private const string basePath = "..\\..\\..\\TestData\\Lab6";

        [DataTestMethod]
        [DataRow("sample", "1")]
        [DataRow("secret", "0mini")]
        [DataRow("secret", "1small")]
        [DataRow("secret", "2med")]
        [DataRow("secret", "3large")]
        [DataRow("secret", "4huge")]
        public void Solve_ReturnsCorrectAnswer(string subdirectory, string fileName)
        {
            TestCasePath path = TestCasePath.From(basePath, subdirectory, fileName);
            RunTestWithPaths(path);
        }

        private void RunTestWithPaths(TestCasePath path)
        {
            using FileInputDataSource inputSource = new FileInputDataSource(path.InputPath);
            using StreamReader expectedOutputReader = new StreamReader(path.AnswerPath);

            SectionTimer sectionTimer = new SectionTimer();
            sectionTimer.StartSection("fullSolve", excludeFromTotalSum: true);

            SolveResult result = Program.Solve(inputSource, sectionTimer);

            sectionTimer.StopSection("fullSolve");

            string? expected = expectedOutputReader.ReadLine();

            Assert.IsNotNull(expected);

            expected = expected.Trim();
            string actual = result.ToString().Trim();

            Assert.AreEqual(expected, actual, $"Mismatch in test case: {Path.GetFileName(path.InputPath)}");

            Console.WriteLine(sectionTimer.ToString());
        }

        [TestMethod]
        public void Parse_CorrectlyParsesInputData()
        {
            string input = @"3 3 10 3
0 1 10
0 2 10
1 2 10
0
2
1";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);

            Assert.AreEqual(3, data.NodeCount);
            Assert.AreEqual(3, data.EdgeCount);
            Assert.AreEqual(10, data.RequiredFlow);
            Assert.AreEqual(3, data.PlanLength);

            Assert.AreEqual(3, data.Edges.Count);
            Assert.AreEqual(0, data.Edges[0].Start);
            Assert.AreEqual(1, data.Edges[0].End);
            Assert.AreEqual(10, data.Edges[0].Capacity);

            CollectionAssert.AreEqual(new List<int> { 0, 2, 1 }, data.RemovalPlan);
        }

        [TestMethod]
        public void CreateFromInputData_CreatesCorrectGraph()
        {
            string input = @"3 3 10 3
0 1 10
0 2 20
1 2 30
0
2
1";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            Assert.AreEqual(3, graph.Nodes.Count);
            Assert.AreEqual(3, graph.Edges.Count);

            // Edge 0: 0–1 (10)
            Edge edge0 = graph.Edges[0]!;
            Assert.AreEqual(0, edge0.Start.Id);
            Assert.AreEqual(1, edge0.End.Id);
            Assert.AreEqual(10, edge0.Capacity);
            Assert.AreEqual(0, edge0.Flow);

            // Edge 1: 0–2 (20)
            Edge edge1 = graph.Edges[1]!;
            Assert.AreEqual(0, edge1.Start.Id);
            Assert.AreEqual(2, edge1.End.Id);
            Assert.AreEqual(20, edge1.Capacity);

            // Edge 2: 1–2 (30)
            Edge edge2 = graph.Edges[2]!;
            Assert.AreEqual(1, edge2.Start.Id);
            Assert.AreEqual(2, edge2.End.Id);
            Assert.AreEqual(30, edge2.Capacity);

            // Check node-edge linkage
            Node node0 = graph.Nodes[0];
            Assert.IsTrue(node0.Edges.Contains(edge0));
            Assert.IsTrue(node0.Edges.Contains(edge1));

            Node node1 = graph.Nodes[1];
            Assert.IsTrue(node1.Edges.Contains(edge0));
            Assert.IsTrue(node1.Edges.Contains(edge2));

            Node node2 = graph.Nodes[2];
            Assert.IsTrue(node2.Edges.Contains(edge1));
            Assert.IsTrue(node2.Edges.Contains(edge2));
        }

        [TestMethod]
        public void RemoveEdgeByIndex_RemovesEdgeFromGraphAndNodes()
        {
            string input = @"3 3 10 1
0 1 10
0 2 20
1 2 30
0";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            Edge? edgeToRemove = graph.Edges[0];
            Node start = edgeToRemove!.Start;
            Node end = edgeToRemove.End;

            // Pre-check: edge is in both node lists
            Assert.IsTrue(start.Edges.Contains(edgeToRemove));
            Assert.IsTrue(end.Edges.Contains(edgeToRemove));

            graph.RemoveEdgeByIndex(0);

            // Graph.Edges[0] should now be null
            Assert.IsNull(graph.Edges[0]);

            // Edge should be removed from both node edge lists
            Assert.IsFalse(start.Edges.Contains(edgeToRemove));
            Assert.IsFalse(end.Edges.Contains(edgeToRemove));

            // Other edges should remain
            Assert.AreEqual(3, graph.Edges.Count);
            Assert.IsNotNull(graph.Edges[1]);
            Assert.IsNotNull(graph.Edges[2]);
        }

        [TestMethod]
        public void ResetFlows_SetsAllEdgeFlowsToZero()
        {
            string input = @"3 3 10 0
0 1 10
0 2 20
1 2 30";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            // Set up dummy flow values
            foreach (Edge? edge in graph.Edges)
            {
                if (edge != null)
                    edge.Flow = 999;
            }

            graph.ResetFlows();

            foreach (Edge? edge in graph.Edges)
            {
                if (edge != null)
                    Assert.AreEqual(0, edge.Flow, $"Edge {edge} did not reset flow.");
            }
        }

        [TestMethod]
        public void FindAugmentingPath_FindsPath_WhenResidualCapacityExists()
        {
            string input = @"3 2 1 0
0 1 10
1 2 10";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            Node sourceNode = graph.Nodes[0];
            Node sinkNode = graph.Nodes[2];

            FlowPath path = graph.FindAugmentingPath(sourceNode, sinkNode);

            Assert.IsFalse(path.IsEmpty);
            int bottleneck = path.CalculateBottleneck();
            Assert.AreEqual(10, bottleneck);
        }

        [TestMethod]
        public void FindAugmentingPath_ReturnsEmpty_WhenNoPathExists()
        {
            string input = @"3 2 1 0
0 1 10
1 2 10";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            // Simulate edges at full capacity
            foreach (Edge? edge in graph.Edges)
                if (edge != null)
                    edge.Flow = edge.Capacity;

            Node sourceNode = graph.Nodes[0];
            Node sinkNode = graph.Nodes[2];

            FlowPath path = graph.FindAugmentingPath(sourceNode, sinkNode);

            Assert.IsTrue(path.IsEmpty);
        }

        [TestMethod]
        public void FlowPath_CalculateBottleneckAndApplyFlow_WorkCorrectly()
        {
            string input = @"3 2 1 0
0 1 5
1 2 8";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            Node sourceNode = graph.Nodes[0];
            Node sinkNode = graph.Nodes[2];

            FlowPath path = graph.FindAugmentingPath(sourceNode, sinkNode);

            Assert.IsFalse(path.IsEmpty);

            int bottleneck = path.CalculateBottleneck();
            Assert.AreEqual(5, bottleneck); // 5 is the limiting capacity on edge 0–1

            path.ApplyFlow(bottleneck);

            // Now the flows should be updated:
            Edge edge01 = graph.Edges[0]!;
            Edge edge12 = graph.Edges[1]!;

            Assert.AreEqual(5, edge01.Flow);
            Assert.AreEqual(5, edge12.Flow);
        }

        [TestMethod]
        public void ComputeMaxFlow_ReturnsCorrectFlow_SimplePath()
        {
            string input = @"3 2 1 0
0 1 5
1 2 7";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            MaxFlowSolver solver = new MaxFlowSolver(graph);
            int maxFlow = solver.ComputeMaxFlow(graph.Nodes[0], graph.Nodes[2]);

            // Flow is limited by edge 0–1 (capacity 5)
            Assert.AreEqual(5, maxFlow);
        }

        [TestMethod]
        public void ComputeMaxFlow_ReturnsCorrectFlow_WithParallelPaths()
        {
            string input = @"4 4 1 0
0 1 10
1 3 10
0 2 5
2 3 5";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            MaxFlowSolver solver = new MaxFlowSolver(graph);
            int maxFlow = solver.ComputeMaxFlow(graph.Nodes[0], graph.Nodes[3]);

            // Two disjoint paths: 0–1–3 (10) and 0–2–3 (5)
            Assert.AreEqual(15, maxFlow);
        }

        [TestMethod]
        public void Solve_RemovesCorrectNumberOfEdgesAndReportsFinalFlow()
        {
            string input = @"3 3 10 3
0 1 10
0 2 10
1 2 10
0
2
1";

            IInputDataSource source = new StringInputDataSource(input);
            SolveResult result = Program.Solve(source);

            Assert.AreEqual(2, result.RoutesRemoved);
            Assert.AreEqual(10, result.FinalFlow);
        }

        [TestMethod]
        public void Solve_FailingCaseFromMiniIn()
        {
            string input = @"5 7 20 7
3 4 13
0 2 33
1 3 35
1 2 54
0 1 48
2 3 14
2 4 77
3
1
5
6
2
0
4";

            IInputDataSource source = new StringInputDataSource(input);
            SolveResult result = Program.Solve(source);

            Assert.AreEqual(27, result.FinalFlow);
            Assert.AreEqual(2, result.RoutesRemoved);
        }

        [TestMethod]
        public void SimplifiedGraph_ComputeMaxFlow_IsCorrect()
        {
            string input = @"5 7 0 0
3 4 13
0 2 33
1 3 35
1 2 54
0 1 48
2 3 14
2 4 77";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            MaxFlowSolver solver = new MaxFlowSolver(graph);
            int flow = solver.ComputeMaxFlow(graph.Nodes[0], graph.Nodes[4]);

            Assert.AreEqual(81, flow); // This must match what we’ve seen
        }

        [TestMethod]
        public void Edge_AddFlow_ReversesFlowWhenGoingBackward()
        {
            Node a = new Node(0);
            Node b = new Node(1);
            Edge edge = new Edge(a, b, 10);

            edge.AddFlow(a, b, 5); // Forward flow
            edge.AddFlow(b, a, 3); // Reverse flow

            Assert.AreEqual(2, edge.Flow); // Net flow is forward 2
        }

        [TestMethod]
        public void FindAugmentingPath_UsesBackwardEdge_WhenNeeded()
        {
            // Build graph: 0 → 1 → 2, then reverse flow to test 0 ← 1 ← 2
            Node node0 = new Node(0);
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Edge edge01 = new Edge(node0, node1, 10);
            Edge edge12 = new Edge(node1, node2, 10);

            node0.Edges.Add(edge01);
            node1.Edges.Add(edge01);
            node1.Edges.Add(edge12);
            node2.Edges.Add(edge12);

            Graph graph = new Graph(new List<Node> { node0, node1, node2 }, new List<Edge?> { edge01, edge12 });

            // Send 10 flow from 0 → 2
            edge01.AddFlow(node0, node1, 10);
            edge12.AddFlow(node1, node2, 10);

            // Now try to send flow back from 2 to 0 — should go 2 ← 1 ← 0 (backward edges)
            FlowPath path = graph.FindAugmentingPath(node2, node0);
            int bottleneck = path.CalculateBottleneck();

            Assert.IsFalse(path.IsEmpty, "Expected a path using backward edges.");
            Assert.AreEqual(10, bottleneck, "Expected full reverse residual capacity.");
        }

        [TestMethod]
        public void Edge_ResidualCapacity_ReturnsCorrectBackwardCapacity()
        {
            Node a = new Node(0);
            Node b = new Node(1);
            Edge edge = new Edge(a, b, 10);

            // Simulate forward flow
            edge.AddFlow(a, b, 7);

            // Check residual in reverse direction
            int backwardResidual = edge.ResidualCapacity(b, a);

            Assert.AreEqual(7, backwardResidual);
        }

        [TestMethod]
        public void AfterRemovingEdge_1_2_WithCapacity54_FlowDropsToExpected()
        {
            string input = @"5 6 0 0
3 4 13
0 2 33
1 3 35
0 1 48
2 3 14
2 4 77";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            // Remove edge (1,2)
            graph.RemoveEdgeByIndex(3); // original index of (1,2) in full graph

            MaxFlowSolver solver = new MaxFlowSolver(graph);
            int flow = solver.ComputeMaxFlow(graph.Nodes[0], graph.Nodes[4]);

            Assert.AreEqual(33, flow);
        }

        [TestMethod]
        public void ComputeMaxFlow_InitialFlowBeforeRemovals_IsCorrect()
        {
            string input = @"5 7 20 7
3 4 13
0 2 33
1 3 35
1 2 54
0 1 48
2 3 14
2 4 77
0
1
2
3
4
5
6";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            MaxFlowSolver solver = new MaxFlowSolver(graph);
            int flow = solver.ComputeMaxFlow(graph.Nodes[0], graph.Nodes[4]);

            Assert.AreEqual(81, flow);
        }

        [TestMethod]
        public void ComputeMaxFlow_SingleEdge_DoesNotExceedCapacity()
        {
            string input = @"2 1 10 0
0 1 10";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            MaxFlowSolver solver = new MaxFlowSolver(graph);
            int flow = solver.ComputeMaxFlow(graph.Nodes[0], graph.Nodes[1]);

            // This should be exactly 10, and never exceed the edge capacity
            Assert.AreEqual(10, flow);
        }

        [TestMethod]
        public void ComputeMaxFlow_TriangleOveruseBug_ReproducesFlowLeak()
        {
            string input = @"3 3 10 0
0 1 10
0 2 10
1 2 10";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            MaxFlowSolver solver = new MaxFlowSolver(graph);
            int flow = solver.ComputeMaxFlow(graph.Nodes[0], graph.Nodes[2]);

            // Max flow should be 20 (0→1→2 and 0→2)
            Assert.AreEqual(20, flow);
        }

        [TestMethod]
        public void ComputeMaxFlow_NoEdgeExceedsCapacity()
        {
            string input = @"5 7 20 0
3 4 13
0 2 33
1 3 35
1 2 54
0 1 48
2 3 14
2 4 77";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            MaxFlowSolver solver = new MaxFlowSolver(graph);
            int flow = solver.ComputeMaxFlow(graph.Nodes[0], graph.Nodes[4]);

            foreach (Edge? edge in graph.Edges)
            {
                if (edge != null)
                {
                    Assert.IsTrue(edge.Flow <= edge.Capacity, $"Edge {edge} exceeded capacity.");
                    Assert.IsTrue(edge.Flow >= 0, $"Edge {edge} has negative flow.");
                }
            }
        }

        [TestMethod]
        public void RemoveEdgeByIndex_RemovesEdgeFromGraphAndNodes2()
        {
            string input = @"3 3 10 0
0 1 10
1 2 15
0 2 20";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            // Remove edge at index 1: 1–2 (capacity 15)
            graph.RemoveEdgeByIndex(1);

            // Check Graph.Edges
            Assert.IsNull(graph.Edges[1], "Edge should be null in Graph.Edges after removal.");

            // Get the nodes that were connected by the removed edge
            Node node1 = graph.Nodes[1];
            Node node2 = graph.Nodes[2];

            // Ensure it's removed from Node.Edges of both endpoints
            bool edgeInNode1 = node1.Edges.Any(e => (e.Start == node1 && e.End == node2) || (e.Start == node2 && e.End == node1));
            bool edgeInNode2 = node2.Edges.Any(e => (e.Start == node1 && e.End == node2) || (e.Start == node2 && e.End == node1));

            Assert.IsFalse(edgeInNode1, "Edge should be removed from node1.Edges.");
            Assert.IsFalse(edgeInNode2, "Edge should be removed from node2.Edges.");
        }

        [TestMethod]
        public void RemoveEdgeByIndex_RemovesExactOriginalEdgeObject()
        {
            string input = @"3 3 10 0
0 1 10
1 2 15
0 2 20";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            // Store the reference to the edge at index 1 (1–2)
            Edge? edgeToRemove = graph.Edges[1];

            Assert.IsNotNull(edgeToRemove, "Edge should exist before removal.");

            // Remove it
            graph.RemoveEdgeByIndex(1);

            // Check that the same edge reference is now null in the graph
            Assert.IsNull(graph.Edges[1], "Edge reference should be null after removal.");

            // Ensure that edge is no longer in either node
            Node node1 = graph.Nodes[1];
            Node node2 = graph.Nodes[2];

            Assert.IsFalse(node1.Edges.Contains(edgeToRemove), "Removed edge should not be in node1.Edges.");
            Assert.IsFalse(node2.Edges.Contains(edgeToRemove), "Removed edge should not be in node2.Edges.");
        }

        [TestMethod]
        public void FindAugmentingPath_DoesNotIncludeRemovedEdge()
        {
            string input = @"3 3 10 0
0 1 10
1 2 10
0 2 10";

            IInputDataSource source = new StringInputDataSource(input);
            InputData data = InputData.Parse(source);
            Graph graph = Graph.CreateFromInputData(data);

            // Remove edge at index 2: 0–2
            Edge? removedEdge = graph.Edges[2];
            graph.RemoveEdgeByIndex(2);

            // Sanity check
            Assert.IsNotNull(removedEdge);

            // Find a path from 0 to 2
            FlowPath path = graph.FindAugmentingPath(graph.Nodes[0], graph.Nodes[2]);

            // Ensure that removedEdge is not used in the path
            string pathStr = path.ToString();
            string removedDesc = removedEdge!.ToString();

            Assert.IsFalse(pathStr.Contains(removedDesc), "Path should not include removed edge.");
        }
    }
}
