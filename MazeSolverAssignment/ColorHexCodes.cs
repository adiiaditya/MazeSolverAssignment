using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolverAssignment
{
    /// <summary>
    /// Encapsulation for all the colors defined in this maze.
    /// </summary>
    public class ColorHexCodes
    {
        // Color of Wall
        public const string BLACK = "ff000000";
        // Color of Gap
        public const string WHITE = "ffffffff";
        // Color of Start
        public const string RED = "ffff0000";
        // Color of End
        public const string BLUE = "ff0000ff";
        // Color of Path
        public const string GREEN = "ff00ff00";

        static public bool Contains(string color)
        {
            return (color == BLACK || color == WHITE || color == RED || color == BLUE || color == GREEN);
        }
    }

}
