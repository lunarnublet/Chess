using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;

namespace ChessController
{
    class PieceController
    {
        ChessModel.Piece[] aliveWhitePieces;
        ChessModel.Piece[] deadWhitePieces;
        ChessModel.Piece[] aliveBlackPieces;
        ChessModel.Piece[] deadBlackPieces;



        public bool initializeOld(Piece[] alivePiecesWhite, Piece[] deadPiecesWhite, Piece[] alivePiecesBlack, Piece[] deadPiecesBlack)
        {
            aliveWhitePieces = new Piece[16];
            deadWhitePieces = new Piece[16];
            aliveBlackPieces = new Piece[16];
            deadBlackPieces = new Piece[16];
            try
            {
                for (int i = 0; i < alivePiecesWhite.Length; ++i)
                {
                    aliveWhitePieces[i] = alivePiecesWhite[i];
                    deadWhitePieces[i] = deadPiecesWhite[i];
                    aliveBlackPieces[i] = alivePiecesBlack[i];
                    deadBlackPieces[i] = deadPiecesBlack[i];
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            return true;
        }

        public bool initializeNew()
        {
            aliveWhitePieces = new Piece[16];
            deadWhitePieces = new Piece[16];
            aliveBlackPieces = new Piece[16];
            deadBlackPieces = new Piece[16];

            try
            {
                aliveWhitePieces[0] = new Rook();
                aliveWhitePieces[1] = new Knight();
                aliveWhitePieces[2] = new Bishop();
                aliveWhitePieces[3] = new King();
                aliveWhitePieces[4] = new Queen();
                aliveWhitePieces[5] = new Bishop();
                aliveWhitePieces[6] = new Knight();
                aliveWhitePieces[7] = new Rook();

                aliveBlackPieces[0] = new Rook();
                aliveBlackPieces[1] = new Knight();
                aliveBlackPieces[2] = new Bishop();
                aliveBlackPieces[3] = new King();
                aliveBlackPieces[4] = new Queen();
                aliveBlackPieces[5] = new Bishop();
                aliveBlackPieces[6] = new Knight();
                aliveBlackPieces[7] = new Rook();

                for (int i = 8; i < aliveBlackPieces.Length; ++i)
                {
                    aliveWhitePieces[i] = new Pawn();
                    aliveBlackPieces[i] = new Pawn();
                }
            }
            catch(IndexOutOfRangeException)
            {
                return false;
            }
            return true;
        }

        public bool changePiece(ref Piece oldPiece, ref Piece newPiece, bool isWhite)
        {
            for (int i = 0; i < aliveWhitePieces.Length; ++i)
            {
                if (isWhite)
                {
                    if (aliveWhitePieces[i] == oldPiece)
                    {
                        aliveWhitePieces[i] = newPiece;
                        return true;
                    }
                }
                else
                {
                    if (aliveBlackPieces[i] == oldPiece)
                    {
                        aliveBlackPieces[i] = newPiece;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool killPiece(ref Piece piece, bool isWhite)
        {
            for (int i = 0; i < aliveWhitePieces.Length; ++i)
            {
                if (isWhite)
                {
                    if (aliveWhitePieces[i] == piece)
                    {
                        aliveWhitePieces[i] = null;
                        deadWhitePieces[i] = piece;
                        return true;
                    }
                }
                else
                {
                    if (aliveBlackPieces[i] == piece)
                    {
                        aliveBlackPieces[i] = null;
                        deadBlackPieces[i] = piece;
                        return true;
                    }
                }
            }
            return false;
        }

        public string[] PieceMovementOptions(ref Piece piece, bool isWhite, Piece[,] boardPieces)
        {

            switch (piece.MoveSet[0])
            {
                case Behavior.SINGLE:
                    for(int i = 0; i < piece.beha)
                    break;

                case Behavior.MULTIPLE:
                    break;
            }
        }
    }
}
