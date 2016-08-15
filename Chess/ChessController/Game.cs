using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;

namespace ChessController
{
    public class Game : IController
    {
        Board board;
        PieceController pieceController;
        List<string> moves;

        public List<string> OnCheckMoves(ref Piece piece)
        {
            moves = pieceController.PieceMovementOptions(ref piece, board.boardPieces);
            return moves;
        }

        public bool OnMove(ref Piece piece, int row, int col)
        {
            for (int i = 0; i < moves.Count; ++i)
            {
                if (moves[i].Equals(row + "" + col))
                {
                    piece.hasMoved = true;
                    return board.MovePiece(ref piece, row, col);
                }
            }
            return false;
        }

        public void Run()
        {
            board = new Board();
            pieceController = new PieceController();
            pieceController.initializeNew();
            SetUpBoard();

            pieceController.killPiece(ref board.boardPieces[6, 3]);
            board.KillPiece(6, 3);

            Console.WriteLine(board.ToString());
            OnCheckMoves(ref board.boardPieces[6, 0]);
            OnMove(ref board.boardPieces[6, 0], 4, 0);
            Console.WriteLine(board.ToString());

            OnCheckMoves(ref board.boardPieces[7, 4]);
            OnMove(ref board.boardPieces[7, 4], 5, 6);
            Console.WriteLine(board.ToString());
            OnCheckMoves(ref board.boardPieces[7, 4]);
            OnMove(ref board.boardPieces[7, 4], 6, 3);
            Console.WriteLine(board.ToString());
            OnCheckMoves(ref board.boardPieces[6, 3]);
            OnMove(ref board.boardPieces[6, 3], 4, 1);
            Console.WriteLine(board.ToString());
            OnCheckMoves(ref board.boardPieces[4, 1]);
            OnMove(ref board.boardPieces[4, 1], 1, 4);
            Console.WriteLine(board.ToString());
            OnCheckMoves(ref board.boardPieces[1, 4]);
            OnMove(ref board.boardPieces[1, 4], 0, 4);
            Console.WriteLine(board.ToString());
            OnCheckMoves(ref board.boardPieces[0, 4]);
            OnMove(ref board.boardPieces[0, 4], 4, 4);
            Console.WriteLine(board.ToString());

            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[7, 3]);
            //OnMove(ref board.boardPieces[7, 3], 6, 2);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[6,2]);
            //OnMove(ref board.boardPieces[6,2], 5, 2);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[5,2]);
            //OnMove(ref board.boardPieces[5,2], 5, 3);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[5, 3]);
            //OnMove(ref board.boardPieces[5, 3], 6, 3);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[5, 3]);
            //OnMove(ref board.boardPieces[5, 3], 4,3);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[4, 3]);
            //OnMove(ref board.boardPieces[4, 3], 3, 3);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[3, 3]);
            //OnMove(ref board.boardPieces[3, 3], 2, 3);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[2, 3]);
            //OnMove(ref board.boardPieces[2, 3], 1, 3);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[1, 3]);
            //OnMove(ref board.boardPieces[1, 3], 0, 3);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[0, 3]);
            //OnMove(ref board.boardPieces[0, 3], -1, 3);
            //Console.WriteLine(board.ToString());
            //OnCheckMoves(ref board.boardPieces[0, 3]);
            //OnMove(ref board.boardPieces[0, 3], 2, 3);
            //Console.WriteLine(board.ToString());
        }

        public void SetUpBoard()
        {
            for (int i = 0; i < pieceController.aliveWhitePieces.Length / 2; ++i)
            {
                board.PlacePiece(7, i, ref pieceController.aliveWhitePieces[i]);
                board.PlacePiece(0, i, ref pieceController.aliveBlackPieces[i]);

                board.PlacePiece(6, i, ref pieceController.aliveWhitePieces[i + 8]);
                board.PlacePiece(1, i, ref pieceController.aliveBlackPieces[i + 8]);
            }
        }
    }
}
