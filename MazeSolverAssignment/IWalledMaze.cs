using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolverAssignment
{
    /// <summary>
    /// Represents an interface for a walled maze.
    /// </summary>
    public interface IWalledMaze
    {
        //Indicates the height of the maze
        int height { get; }

        //Indicates the width of the maze
        int width { get; }
        
        //Indicates the start point of the maze
        IWalledMazeNode start { get; } 
        
        //Indicate the end point of the maze
        IWalledMazeNode finish { get; } 
        
        //Used by the client to check if its the end point.
        bool IsEndPoint(IWalledMazeNode currentNode);

        //Gets a specific node given the position.
        IWalledMazeNode getNode(int row, int col);

        //Gets all nodes for the maze.
        IEnumerator<IWalledMazeNode> getNodes();
        
        //Lists all adjacent nodes for a given node.
        IEnumerable<IWalledMazeNode> getAdjacentNodes(IWalledMazeNode currentNode); 
       
    }
}