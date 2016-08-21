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
        public bool whitesTurn = false;

        public bool GetIfWhiteKingInCheck()
        {
            return pieceController.whiteKingInCheck;
        }

        public bool GetIfBlackKingInCheck()
        {
            return pieceController.blackKingInCheck;
        }


        public List<string> OnCheckMoves(ref Piece piece)
        {
            if (piece.isWhite == whitesTurn)
            {
                foreach (Piece item in pieceController.availableToMove)
                {
                    if (piece == item)
                    {
                        return piece.possibleMoves;
                    }
                }
            }
            return null;
        }

        public bool OnMove(ref Piece piece, int row, int col)
        {
            if (piece.isWhite == whitesTurn)
            {
                for (int i = 0; i < piece.possibleMoves.Count; ++i)
                {
                    if (piece.possibleMoves[i].Equals(row + "" + col))
                    {
                        if (piece is Pawn)
                        {
                            if (row == 0 || row == 7)
                            {
                                Queen queen = new Queen(piece.isWhite);
                                pieceController.ChangePiece(ref piece, queen);
                                piece = queen;
                            }
                        }
                        if (board.BoardPieces[row, col] != null)
                        {
                            pieceController.killPiece(ref board.BoardPieces[row, col]);
                        }
                        if (piece is King)
                        {
                            int kingCol = -1;
                            int kingRow = -1;
                            pieceController.GetPiecePosition(ref piece, board.BoardPieces, out kingRow, out kingCol);
                            if (col == kingCol + 2)
                            {
                                board.MovePiece(ref board.BoardPieces[row, kingCol + 4], row, col -1);
                                board.BoardPieces[row, col - 1].hasMoved = true;
                            }
                            if (col == kingCol - 2)
                            {
                                board.MovePiece(ref board.BoardPieces[row, kingCol - 3], row, col + 1);
                                board.BoardPieces[row, col + 1].hasMoved = true;
                            }
                        }
                        board.MovePiece(ref piece, row, col);
                        board.BoardPieces[row, col].hasMoved = true;
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Piece> OnNewTurn()
        {
            whitesTurn = !whitesTurn;
            pieceController.UpdateMoveOptions(board.BoardPieces);
            pieceController.Check(board.BoardPieces, whitesTurn, false);
            pieceController.Check(board.BoardPieces, !whitesTurn, false);
            pieceController.availableToMove = pieceController.GetPiecesThatCanMove(board.BoardPieces, whitesTurn);
            return pieceController.availableToMove;
        }

        public void Run()
        {
            board = new Board();
            pieceController = new PieceController();
            pieceController.initializeNew();
            SetUpBoard();
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

        public Board getBoard()
        {
            return board;
        }
    }
}
