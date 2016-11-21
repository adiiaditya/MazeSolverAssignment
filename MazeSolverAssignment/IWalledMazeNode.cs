using System;
using System.Collections.Generic;

namespace MazeSolverAssignment
{
    /// <summary>
    /// Represents a node for the maze.
    /// </summary>

    public interface IWalledMazeNode
    {
        //Y-coordinate
        int RowPosition { get; }
        
        //X-coordinate
        int ColPosition { get; }

        //State information. States for a walled maze are Gap or Blocked.
        WalledMazeNodeState State { get; set; } 
    }
}