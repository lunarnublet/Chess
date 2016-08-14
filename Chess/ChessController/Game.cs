using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;

namespace ChessController
{
    public class Game
    {
        Board board;
        PieceController pieceController;

        public void Run()
        {
            board = new Board();
            pieceController = new PieceController();
            pieceController.initializeNew();
            SetUp();

            Console.WriteLine(board.ToString());
        }

        public void SetUp()
        {
            board.PlacePiece(7, 0, ref pieceController.aliveWhitePieces[0]);
            board.PlacePiece(7, 1, ref pieceController.aliveWhitePieces[1]);
            board.PlacePiece(7, 2, ref pieceController.aliveWhitePieces[2]);
            board.PlacePiece(7, 3, ref pieceController.aliveWhitePieces[3]);
            board.PlacePiece(7, 4, ref pieceController.aliveWhitePieces[4]);
            board.PlacePiece(7, 5, ref pieceController.aliveWhitePieces[5]);
            board.PlacePiece(7, 6, ref pieceController.aliveWhitePieces[6]);
            board.PlacePiece(7, 7, ref pieceController.aliveWhitePieces[7]);

            board.PlacePiece(0, 0, ref pieceController.aliveBlackPieces[0]);
            board.PlacePiece(0, 1, ref pieceController.aliveBlackPieces[1]);
            board.PlacePiece(0, 2, ref pieceController.aliveBlackPieces[2]); 
            board.PlacePiece(0, 3, ref pieceController.aliveBlackPieces[3]); 
            board.PlacePiece(0, 4, ref pieceController.aliveBlackPieces[4]);
            board.PlacePiece(0, 5, ref pieceController.aliveBlackPieces[5]);
            board.PlacePiece(0, 6, ref pieceController.aliveBlackPieces[6]);
            board.PlacePiece(0, 7, ref pieceController.aliveBlackPieces[7]);

            for (int i = 0; i < pieceController.aliveWhitePieces.Length / 2; ++i)
            {
                board.PlacePiece(6, i, ref pieceController.aliveWhitePieces[i + 8]);
                board.PlacePiece(1, i, ref pieceController.aliveBlackPieces[i + 8]);
            }
            pieceController.killPiece(ref board.boardPieces[0,0]);
            pieceController.killPiece(ref board.boardPieces[0,1]);
            pieceController.killPiece(ref board.boardPieces[0,2]);
        }
    }
}
