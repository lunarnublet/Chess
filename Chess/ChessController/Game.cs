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
            Console.WriteLine(board.ToString());

            OnCheckMoves(ref board.boardPieces[1, 0]);
            OnMove(ref board.boardPieces[1, 0], 3, 0);
            Console.WriteLine(board.ToString());

            OnCheckMoves(ref board.boardPieces[3, 0]);
            OnMove(ref board.boardPieces[3, 0], 4, 0);
            Console.WriteLine(board.ToString());

            OnCheckMoves(ref board.boardPieces[6, 2]);
            OnMove(ref board.boardPieces[6, 2], 4, 2);
            Console.WriteLine(board.ToString());

            OnCheckMoves(ref board.boardPieces[1, 1]);
            OnMove(ref board.boardPieces[1, 1], 2, 1);
            Console.WriteLine(board.ToString());

            OnCheckMoves(ref board.boardPieces[0, 2]);
            OnMove(ref board.boardPieces[0, 2], 2, 0);
            Console.WriteLine(board.ToString());

            OnCheckMoves(ref board.boardPieces[2, 0]);
            OnMove(ref board.boardPieces[2, 0], 4, 2);
            Console.WriteLine(board.ToString());

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
