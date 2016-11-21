using MazeSolverAssignment;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolverAssignment
{
    class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        public static void Main(string[] args)
        {
            //check if input from command line is correct
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid number of arguments");
                Console.WriteLine("Input should be: MazeSolverAssignment.exe source.[bmp,png,jpg] destination.[bmp,png,jpg]");
                Console.ReadLine();
                return;
            }
            string imagePath = args[0];
            string outputImagePath = args[1];

            try
            {
                IWalledMaze mazeProblem = new WalledMaze(imagePath, ColorHexCodes.BLACK, ColorHexCodes.WHITE, ColorHexCodes.RED, ColorHexCodes.BLUE);
                IMazeSolver mazeSolver = new MazeSolver();
                mazeSolver.Solve(mazeProblem, (solvedPath) =>
                {
                    Bitmap bitMap = new Bitmap(imagePath);
                    foreach (var node in solvedPath)
                    {
                        bitMap.SetPixel(node.ColPosition, node.RowPosition, Color.Green); 
                    }
                    bitMap.Save(outputImagePath);
                    Console.WriteLine("Completed, See '{0}' for solution", outputImagePath);
                    Console.ReadLine();
                });
            }
            //error handling.
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
                Console.ReadLine();
            }
        }

    }
}
