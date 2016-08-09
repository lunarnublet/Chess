using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;
using ChessController;
using System.IO;

namespace ConsolChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program starting...");
            Board board = new Board();
            board.Initialize(args[0]);
            Console.WriteLine(board.ToString());
            board.TakeSquare("h1 d2", true);
            //board.InitializeReaderTest(args[0]);
            Console.WriteLine("Program ending...");
        }
    }
}
