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
            //board.Initialize("C:\\Chess\\Chess\\ConsolChess\\Data\\Initialization.txt");
            //Console.WriteLine(board.ToString());
            //board.TakeSquare("h1 d2", true);
            
            board.InitializeReaderTest("C:\\Chess\\Chess\\ConsolChess\\Data\\IOTest.txt");
            Console.WriteLine("Program ending...");
        }
    }
}
