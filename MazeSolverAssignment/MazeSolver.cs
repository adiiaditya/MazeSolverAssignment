using System;
using System.Collections.Generic;

namespace MazeSolverAssignment
{
    /// <summary>
    /// Solves a maze using Breadth First Search Algorithm.
    /// </summary>
    class MazeSolver : IMazeSolver
    {
        //BFS Node State
        enum BFSNodeState
        {
            NotVisited = 0, //White
            Visited = 1, //Black
            Queued = 2, //Gray
        }

        /// <summary>
        /// BFS internal node representation.
        /// </summary>
        class BFSNode
        {
            public int RowPosition { get; set; }
            public int ColPosition { get; set; }
            //Internal state of the BFS node.
            public BFSNodeState State
            {
                get;
                set;
            }
            //Distance from the source node.
            public int Distance
            {
                get;
                set;
            }
            //Pointer to the previous node.
            public BFSNode Predecessor
            {
                get;
                set;
            }

            public BFSNode(int row, int col)
            {
                RowPosition = row;
                ColPosition = col;
            }
        }

        BFSNode[,] _bfsNodes = null;

        /// <summary>
        /// Implementatin of the Solve() method. Uses the Breadth Fist Search Algorithm that tracks predecessors and distance from source.
        /// </summary>
        public void Solve(IWalledMaze maze, Action<IEnumerable<IWalledMazeNode>> solvedResultCallback)
        {
            InitializeSolver(maze);
            Queue<BFSNode> bfsNodesQueue = new Queue<BFSNode>();
            BFSNode startNode = GetBFSNode(maze.start);
            startNode.Distance = 0;
            startNode.Predecessor = null;
            startNode.State = BFSNodeState.Queued;
            bfsNodesQueue.Enqueue(startNode);

            while (bfsNodesQueue.Count > 0)
            {
                BFSNode curBFSNode = bfsNodesQueue.Dequeue();
                IWalledMazeNode curMazeNode = GetMazeNode(maze, curBFSNode);
                if (maze.IsEndPoint(curMazeNode))
                {
                    IEnumerable<IWalledMazeNode> solvedPath = TraceSolvedPath(maze, curBFSNode);
                    solvedResultCallback(solvedPath);
                    return;
                }
                foreach (IWalledMazeNode adjMazeNode in maze.getAdjacentNodes(curMazeNode))
                {
                    BFSNode adjBFSNode = GetBFSNode(adjMazeNode);
                    if (adjBFSNode.State == BFSNodeState.NotVisited)
                    {
                        adjBFSNode.State = BFSNodeState.Queued;
                        adjBFSNode.Predecessor = curBFSNode;
                        adjBFSNode.Distance = curBFSNode.Distance + 1;
                        bfsNodesQueue.Enqueue(adjBFSNode);
                    }
                }
                curBFSNode.State = BFSNodeState.Visited;
            }
            //No solution found
            solvedResultCallback(null);
        }

        /// <summary>
        /// Conversion function. Converts a maze node to a internal BFS node.
        /// </summary>
        private BFSNode GetBFSNode(IWalledMazeNode mazeNode)
        {
            return _bfsNodes[mazeNode.RowPosition, mazeNode.ColPosition];
        }

        /// <summary>
        /// Conversion function. Converts a BFS node to a maze node.
        /// </summary>
        private IWalledMazeNode GetMazeNode(IWalledMaze maze, BFSNode bfsNode)
        {
            return maze.getNode(bfsNode.RowPosition, bfsNode.ColPosition);
        }

        /// <summary>
        /// Scans all maze's nodes and converts into an internal solver's Node.
        /// </summary>
        private void InitializeSolver(IWalledMaze maze)
        {
            _bfsNodes = new BFSNode[maze.height, maze.width];
            IEnumerator<IWalledMazeNode> mazeNodes = maze.getNodes();
            while (mazeNodes.MoveNext())
            {
                IWalledMazeNode mazeNode = mazeNodes.Current;
                if (mazeNode.State == WalledMazeNodeState.Blocked)
                    _bfsNodes[mazeNode.RowPosition, mazeNode.ColPosition] = new BFSNode(mazeNode.RowPosition, mazeNode.ColPosition) { State = BFSNodeState.Visited, Distance = int.MaxValue };
                else if (mazeNode.State == WalledMazeNodeState.Gap)
                    _bfsNodes[mazeNode.RowPosition, mazeNode.ColPosition] = new BFSNode(mazeNode.RowPosition, mazeNode.ColPosition) { State = BFSNodeState.NotVisited, Distance = int.MaxValue };
            }
        }
        /// <summary>
        /// Traces the solved path and builds the internal BFS tree.
        /// </summary>
        private IEnumerable<IWalledMazeNode> TraceSolvedPath(IWalledMaze maze, BFSNode endNode)
        {
            BFSNode currentNode = endNode;
            ICollection<IWalledMazeNode> pathTrace = new List<IWalledMazeNode>();
            while (currentNode != null)
            {
                pathTrace.Add(GetMazeNode(maze, currentNode));
                currentNode = currentNode.Predecessor;
            }
            return pathTrace;
        }
    }
}