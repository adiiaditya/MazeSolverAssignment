using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MazeSolverAssignment
{
    public class WalledMaze : IWalledMaze
    {
        WalledMazeNode _source = null;
        WalledMazeNode _destination = null;
        WalledMazeNode[,] _maze = null;

        int _width = 0;
        int _height = 0;
        int i, j;

        //Indicates the total width of the maze.
        public int width
        {
            get { return _width; }
        }

        //Indicates the total height of the maze.
        public int height
        {
            get { return _height; }
        }

        //Indicates the start point of the maze.
        public IWalledMazeNode start
        {
            get { return _source; }
        }

        //Indicate the end point of the maze.
        public IWalledMazeNode finish
        {
            get { return _destination; }
        }

        //Determine if it has arrived at the end point.
        public bool IsEndPoint(IWalledMazeNode currentNode)
        {
            if (currentNode == null)
                return false;
            return currentNode.Equals(_destination);
        }

        public WalledMaze(string imagePath, string wallColor, string gapColor, string startColor, string finishColor)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException(string.Format("File {0} not found", imagePath), "imagePath");
            if (!ColorHexCodes.Contains(wallColor) || !ColorHexCodes.Contains(gapColor) || !ColorHexCodes.Contains(startColor) || !ColorHexCodes.Contains(finishColor))
                throw new ArgumentException("Please check the color codes.");
            InitializeMaze(imagePath, wallColor, gapColor, startColor, finishColor);
        }

        /// <summary>
        /// Scans the image and stores information.
        /// </summary>
        private void InitializeMaze(string imagePath, string wallColor, string gapColor, string startColor, string finishColor)
        {
            Bitmap mazeImage = null;
            try
            {
                mazeImage = new Bitmap(imagePath);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Unable to convert the file to bitmap.");
            }

            _maze = new WalledMazeNode[mazeImage.Height, mazeImage.Width];
            _height = mazeImage.Height;
            _width = mazeImage.Width;

            for (i = 0; i < _height; i++)
            {
                for (j = 0 ; j < _width; j++)
                {
                    Color color = mazeImage.GetPixel(j, i);

                    if (color.Name == wallColor)
                    {
                        _maze[i, j] = new WalledMazeNode(i, j) { State = WalledMazeNodeState.Blocked };
                        continue;
                    }
                    else if (color.Name == startColor)
                    {
                        _maze[i, j] = new WalledMazeNode(i, j) { State = WalledMazeNodeState.Gap };
                        if (_source == null)
                            _source = _maze[i, j];
                    }
                    else if (color.Name == finishColor)
                    {
                        _maze[i, j] = new WalledMazeNode(i, j) { State = WalledMazeNodeState.Gap };
                        if (_destination == null)
                            _destination = _maze[i, j];
                    }
                    else if (color.Name == gapColor)
                    {
                        _maze[i, j] = new WalledMazeNode(i, j) { State = WalledMazeNodeState.Gap };
                    }
                    else
                    {
                        //Invalid color.
                        throw new Exception(string.Format("Color {0} is an illegal color. Please use colors specified.", color.Name));
                    }
                }
            }
        }

        /// <summary>
        /// Gets a node.
        /// </summary>
        public IWalledMazeNode getNode(int row, int col)
        {
            if (row < 0 || row >= _height || col < 0 || col >= _width)
                throw new ArgumentException("Supplied node is out of bounds!");
            return _maze[row, col];
        }

        /// <summary>
        /// Gets all maze nodes.
        /// </summary>
        public IEnumerator<IWalledMazeNode> getNodes()
        {
            for (i = 0; i < _height; i++)
                for (j = 0; j < _width; j++)
                    yield return _maze[i, j];
        }

        /// <summary>
        /// Gets adjacents for a node, any node can have at most 8 adjacents.
        /// </summary>
        public IEnumerable<IWalledMazeNode> getAdjacentNodes(IWalledMazeNode currentNode)
        {
            int rowPosition = currentNode.RowPosition;
            int colPosition = currentNode.ColPosition;

            if (rowPosition < 0 || rowPosition >= _height || colPosition < 0 || colPosition >= _width) 
            //if given node is out of bounds return a empty list as adjacents.
                return new List<WalledMazeNode>(0);

            List<WalledMazeNode> adjacents = new List<WalledMazeNode>(8);
            for (i = rowPosition - 1; i <= rowPosition + 1; i++)
            {
                for (j = colPosition - 1; j <= colPosition + 1; j++)
                {
                    if (i < 0 || i >= _height || j < 0 || j >= _width || (i == rowPosition && j == colPosition))
                        continue;
                    adjacents.Add(_maze[i, j]);
                }
            }
            return adjacents;
        }
    }
}