using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolverAssignment
{
    // Valid states for a walled maze.
    public enum WalledMazeNodeState : int
    {
        Gap = 0,
        Blocked = 1
    }

    public sealed class WalledMazeNode : IWalledMazeNode
    {
        const int defaultPosition = 0;
        int _rowPosition, _colPosition = defaultPosition;
        WalledMazeNodeState _state = WalledMazeNodeState.Gap;

        public int RowPosition
        {
            get { return _rowPosition; }
        }

        public int ColPosition
        {
            get { return _colPosition; }
        }

        public WalledMazeNodeState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public WalledMazeNode(int row, int col)
        {
            _rowPosition = row;
            _colPosition = col;
        }

        //Override the Equals method to check for end point node.
        public override bool Equals(object obj)
        {
            WalledMazeNode mazeNode = obj as WalledMazeNode;
            if (mazeNode == null)
                return false;
            if(object.ReferenceEquals(mazeNode,this))
                return true;
            return (mazeNode.ColPosition == this.ColPosition && mazeNode.RowPosition == this.RowPosition && mazeNode.State == this.State);
        }

        //Override the GetHashCode method to allow a type to work correctly in a hash table.
        public override int GetHashCode()
        {
            return base.GetHashCode() + ColPosition + RowPosition + (int)State;
        }
    }
}