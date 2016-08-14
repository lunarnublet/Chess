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

            Game game = new Game();
            game.Run();


            Console.WriteLine("Program ending...");
        }

        //static void referenceTest( Piece[] pieces)
        //{
        //    Console.WriteLine(pieces[0].ToString() + ", " + pieces[1].ToString());
        //    pieces[0] = new Rook(false);
        //    pieces[1] = new Pawn(false);
        //    Console.WriteLine(pieces[0].ToString() + ", " + pieces[1].ToString());
        //}
    }
}
