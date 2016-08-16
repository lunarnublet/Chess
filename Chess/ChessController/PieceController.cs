using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;

namespace ChessController
{
    public class PieceController
    {
        public ChessModel.Piece[] aliveWhitePieces;
        public ChessModel.Piece[] deadWhitePieces;
        public ChessModel.Piece[] aliveBlackPieces;
        public ChessModel.Piece[] deadBlackPieces;

        public delegate void PieceKilled(ref Piece piece);
        public event PieceKilled pieceKilled;

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
                aliveWhitePieces[0] = new Rook(true);
                aliveWhitePieces[1] = new Knight(true);
                aliveWhitePieces[2] = new Bishop(true);
                aliveWhitePieces[3] = new King(true);
                aliveWhitePieces[4] = new Queen(true);
                aliveWhitePieces[5] = new Bishop(true);
                aliveWhitePieces[6] = new Knight(true);
                aliveWhitePieces[7] = new Rook(true);

                aliveBlackPieces[0] = new Rook(false);
                aliveBlackPieces[1] = new Knight(false);
                aliveBlackPieces[2] = new Bishop(false);
                aliveBlackPieces[3] = new King(false);
                aliveBlackPieces[4] = new Queen(false);
                aliveBlackPieces[5] = new Bishop(false);
                aliveBlackPieces[6] = new Knight(false);
                aliveBlackPieces[7] = new Rook(false);

                for (int i = 8; i < aliveBlackPieces.Length; ++i)
                {
                    aliveWhitePieces[i] = new Pawn(true);
                    aliveBlackPieces[i] = new Pawn(false);
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            return true;
        }

        public bool changePiece(ref Piece oldPiece, ref Piece newPiece)
        {
            for (int i = 0; i < aliveWhitePieces.Length; ++i)
            {
                if (oldPiece.isWhite)
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

        public bool killPiece(ref Piece piece)
        {
            for (int i = 0; i < aliveWhitePieces.Length; ++i)
            {
                if (piece.isWhite)
                {
                    if (aliveWhitePieces[i] == piece)
                    {
                        aliveWhitePieces[i] = null;
                        deadWhitePieces[i] = piece;
                        OnPieceKilled(ref piece);
                        return true;
                    }
                }
                else
                {
                    if (aliveBlackPieces[i] == piece)
                    {
                        aliveBlackPieces[i] = null;
                        deadBlackPieces[i] = piece;
                        OnPieceKilled(ref piece);
                        return true;
                    }
                }
            }
            return false;
        }

        private void OnPieceKilled(ref Piece piece)
        {
            if( pieceKilled != null)
            {
                pieceKilled.Invoke(ref piece);
            }
        }

        public List<string> PieceMovementOptions(ref Piece piece, Piece[,] boardPieces)
        {
            List<string> moveOptions = new List<string>();
            if (piece == null) { return moveOptions; }
            int pieceRow = 0;
            int pieceCol = 0;
            for (int i = 0; i < Math.Sqrt(boardPieces.Length); ++i)
            {
                for (int v = 0; v < Math.Sqrt(boardPieces.Length); ++v)
                {
                    if (boardPieces[i, v] == piece)
                    {
                        pieceRow = i;
                        pieceCol = v;
                        break;
                    }
                }
            }
            switch (piece.pieceType)
            {
                case PieceName.PAWN:
                    {
                        moveOptions.AddRange(CheckPawnMoves(pieceRow, pieceCol, boardPieces, (Pawn)piece));
                        break;
                    }
                case PieceName.KING:
                    {
                        moveOptions.AddRange(CheckKingMoves(pieceRow, pieceCol, boardPieces, (King)piece));
                        break;
                    }
                case PieceName.BISHOP:
                    {
                        moveOptions.AddRange(CheckDiagnalMoves(pieceRow, pieceCol, boardPieces, piece, true));
                        break;
                    }
                case PieceName.QUEEN:
                    {
                        moveOptions.AddRange(CheckStraightMoves(pieceRow, pieceCol, boardPieces, piece, true));
                        moveOptions.AddRange(CheckDiagnalMoves(pieceRow, pieceCol, boardPieces, piece, true));
                        break;
                    }
                case PieceName.ROOK:
                    {
                        moveOptions.AddRange(CheckStraightMoves(pieceRow, pieceCol, boardPieces, piece, true));
                        break;
                    }
                case PieceName.KNIGHT:
                    {
                        moveOptions.AddRange(CheckKnightMoves(pieceRow, pieceCol, boardPieces, piece));
                        break;
                    }
            }
            return moveOptions;
        }

        private IEnumerable<string> CheckKnightMoves(int pieceRow, int pieceCol, Piece[,] boardPieces, Piece piece)
        {
            List<string> moves = new List<string>();

            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 2; ++i)
                {
                    for (int v = 0; v < 2; ++v)
                    {
                        try
                        {
                            int x;
                            int y;
                            if (j == 0) { x = 1; y = 2; }
                            else { x = 2; y = 1; }
                            if (i > 0) { x *= -1; }
                            if (v > 0) { y *= -1; }
                            if (boardPieces[pieceRow + x, pieceCol + y] == null)
                            {
                                moves.Add((pieceRow + x) + "" + (pieceCol + y));
                            }
                            else
                            {
                                if (boardPieces[pieceRow + x, pieceCol + y].isWhite != piece.isWhite)
                                {
                                    moves.Add((pieceRow + x) + "" + (pieceCol + y));
                                }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                }
            }
            return moves;

        }

        private List<string> CheckKingMoves(int pieceRow, int pieceCol, Piece[,] boardPieces, King piece)
        {
            List<string> moves = CheckStraightMoves(pieceRow, pieceCol, boardPieces, piece, false);
            moves.AddRange(CheckDiagnalMoves(pieceRow, pieceCol, boardPieces, piece, false));

            return moves;
        }

        private List<string> CheckStraightMoves(int pieceRow, int pieceCol, Piece[,] boardPieces, Piece piece, bool recursion)
        {
            List<string> moves = new List<string>();

            int looptimes = 1;
            if (recursion) { looptimes = 8; }

            bool up = true, down = true, left = true, right = true;

            for (int j = 1; j <= looptimes; ++j)
            {
                for (int i = 1; i <= 3; ++i)
                {
                    for (int v = 1; v <= 3; ++v)
                    {
                        if (i % 2 == v % 2) { continue; }
                        int z = (i % 2) * j;
                        int x = (v % 2) * j;
                        if (i > 2) { z *= -1; }
                        if (v > 2) { x *= -1; }
                        try
                        {
                            if ((z > 0 && !up) || (z < 0 && !down) || (x > 0 && !right) || (x < 0 && !left))
                            {
                                continue;
                            }
                            if (boardPieces[pieceRow + z, pieceCol + x] == null)
                            {
                                moves.Add((pieceRow + z) + "" + (pieceCol + x));
                            }
                            else
                            {
                                if (boardPieces[pieceRow + z, pieceCol + x].isWhite != piece.isWhite)
                                {
                                    moves.Add((pieceRow + z) + "" + (pieceCol + x));
                                }
                                if (z > 0) { up = false; }
                                if (z < 0) { down = false; }
                                if (x > 0) { right = false; }
                                if (x < 0) { left = false; }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                }
            }
            return moves;
        }

        private List<string> CheckDiagnalMoves(int pieceRow, int pieceCol, Piece[,] boardPieces, Piece piece, bool recursion)
        {
            List<string> moves = new List<string>();

            int looptimes = 1;
            if (recursion) { looptimes = 8; }

            bool upleft = true, upright = true, downleft = true, downright = true;

            for (int j = 1; j <= looptimes; ++j)
            {
                for (int i = 1; i <= 3; ++i)
                {
                    for (int v = 1; v <= 3; ++v)
                    {
                        if (i % 2 != v % 2) { continue; }
                        int z = (i % 2) * j;
                        int x = (v % 2) * j;
                        if (i > 2) { z *= -1; }
                        if (v > 2) { x *= -1; }
                        try
                        {
                            if ((z > 0 && x > 0 && !upright) || (z < 0 && x > 0 && !downright) || (x < 0 && z < 0 && !downleft) || (x < 0 && z > 0 && !upleft))
                            {
                                continue;
                            }
                            if (boardPieces[pieceRow + z, pieceCol + x] == null)
                            {
                                moves.Add((pieceRow + z) + "" + (pieceCol + x));
                            }
                            else
                            {
                                if (boardPieces[pieceRow + z, pieceCol + x].isWhite != piece.isWhite)
                                {
                                    moves.Add((pieceRow + z) + "" + (pieceCol + x));
                                }
                                if (z > 0 && x > 0) { upright = false; }
                                if (z < 0 && x > 0) { downright = false; }
                                if (x < 0 && z < 0) { downleft = false; }
                                if (x < 0 && z > 0) { upleft = false; }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                }
            }
            return moves;
        }

        private List<string> CheckPawnMoves(int pieceRow, int pieceCol, Piece[,] boardPieces, Piece p)
        {
            List<string> moves = new List<string>();
            int direction = -1;
            if (!p.isWhite)
            {
                direction = 1;
            }

            try
            {
                if (boardPieces[pieceRow + direction, pieceCol + 1] != null && boardPieces[pieceRow + direction, pieceCol + 1].isWhite == p.isWhite)
                {
                    moves.Add((pieceRow + direction) + "" + (pieceCol + 1));
                }
            }
            catch (IndexOutOfRangeException)
            {

            }
            try
            {
                if (boardPieces[pieceRow + direction, pieceCol - 1] != null && boardPieces[pieceRow + direction, pieceCol - 1].isWhite != p.isWhite)
                {
                    moves.Add((pieceRow + direction) + "" + (pieceCol - 1));
                }
            }
            catch (IndexOutOfRangeException)
            {
                
            }
            if (boardPieces[pieceRow + direction, pieceCol] == null)
            {
                moves.Add((pieceRow + direction) + "" + pieceCol);
                if (!p.hasMoved)
                {
                    if (boardPieces[pieceRow + (direction * 2), pieceCol] == null)
                    {
                        moves.Add(pieceRow + (direction * 2) + "" + pieceCol);
                    }
                }
            }
            return moves;
        }
    }
}
