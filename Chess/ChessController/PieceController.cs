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
        public ChessModel.Piece[] aliveBlackPieces;

        Piece whiteKingCheckedBy = null;
        Piece blackKingCheckedBy = null;

        public bool whiteKingInCheck = false;
        public bool blackKingInCheck = false;

        public List<Piece> availableToMove = new List<Piece>();

        public delegate void PieceKilled(ref Piece piece);
        public event PieceKilled pieceKilled;

        public bool initializeOld(Piece[] alivePiecesWhite, Piece[] deadPiecesWhite, Piece[] alivePiecesBlack, Piece[] deadPiecesBlack)
        {
            aliveWhitePieces = new Piece[16];
            aliveBlackPieces = new Piece[16];
            try
            {
                for (int i = 0; i < alivePiecesWhite.Length; ++i)
                {
                    aliveWhitePieces[i] = alivePiecesWhite[i];
                    aliveBlackPieces[i] = alivePiecesBlack[i];
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
            aliveBlackPieces = new Piece[16];

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

        internal Piece[,] GetCopyOfBoard(Piece[,] boardPieces)
        {
            Piece[,] temp = new Piece[Board.boardSize, Board.boardSize];
            for (int j = 0; j < Board.boardSize; ++j)
            {
                for (int k = 0; k < Board.boardSize; ++k)
                {
                    temp[j, k] = boardPieces[j, k];
                }
            }
            return temp;
        }

        internal void GetPiecePosition(ref Piece piece, Piece[,] boardPieces, out int outRow, out int outCol)
        {
            for (int n = 0; n < Board.boardSize; ++n)
            {
                for (int m = 0; m < Board.boardSize; ++m)
                {
                    if (boardPieces[n, m] == piece)
                    {
                        outRow = n;
                        outCol = m;
                        return;
                    }
                }
            }
            outRow = -1;
            outCol = -1;
        }

        internal List<Piece> GetPiecesThatCanMove(Piece[,] boardPieces, bool whitesTurn)
        {
            List<Piece> temp = new List<Piece>();
            Piece[,] tempBoard = new Piece[Board.boardSize, Board.boardSize];
            Piece checker = null;
            if (whitesTurn)
            {
                for (int i = 0; i < aliveWhitePieces.Length; ++i)
                {
                    if (aliveWhitePieces[i] != null)
                    {
                        temp.Add(aliveWhitePieces[i]);
                    }
                }
                if (whiteKingCheckedBy != null)
                {
                    checker = whiteKingCheckedBy;
                }
            }
            else
            {
                for (int i = 0; i < aliveBlackPieces.Length; ++i)
                {
                    if (aliveBlackPieces[i] != null)
                    {
                        temp.Add(aliveBlackPieces[i]);
                    }
                }
                if (blackKingCheckedBy != null)
                {
                    checker = blackKingCheckedBy;
                }
            }
            List<Piece> possibleRemoval = new List<Piece>();
            foreach (Piece piece in temp)
            {
                int i = -1;
                int v = -1;
                for (int j = 0; j < Board.boardSize; ++j)
                {
                    for (int k = 0; k < Board.boardSize; ++k)
                    {
                        if (boardPieces[j, k] == piece)
                        {
                            i = j;
                            v = k;
                        }
                    }
                }
                tempBoard = GetCopyOfBoard(boardPieces);
                if (i != -1)
                {
                    List<string> tempMoves = tempBoard[i,v].possibleMoves;
                    if (tempMoves.Count > 0)
                    {
                        if (checker != null)
                        {
                            int checkerRow = -1;
                            int checkerCol = -1;
                            GetPiecePosition(ref checker, boardPieces, out checkerRow, out checkerCol);
                            List<string> tempMoves2 = new List<string>();
                            List<string> tempRemove = new List<string>();
                            List<string> oldCheckerMoves = checker.possibleMoves;
                            foreach (string item in tempMoves)
                            {
                                if (item == checkerRow + "" + checkerCol)
                                {
                                    tempMoves2.Add(item);
                                }
                                foreach (string item2 in checker.possibleMoves)
                                {
                                    if (piece is King)
                                    {
                                        if (item != item2)
                                        {
                                            bool exists = false;
                                            foreach (string item3 in tempMoves2)
                                            {
                                                if (item == item3)
                                                {
                                                    exists = true;
                                                }
                                            }
                                            if (!exists)
                                            {
                                                tempMoves2.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (item == item2)
                                        {
                                            bool exists = false;
                                            foreach (string item3 in tempMoves2)
                                            {
                                                if (item == item3)
                                                {
                                                    exists = true;
                                                }
                                            }
                                            if (!exists)
                                            {
                                                tempMoves2.Add(item);
                                            }
                                        }
                                    }
                                }
                            }
                            foreach (string item in tempMoves2)
                            {
                                int row = int.Parse(item.Substring(0, 1));
                                int col = int.Parse(item.Substring(1, 1));
                                tempBoard[row, col] = tempBoard[i, v];
                                tempBoard[i, v] = null;
                                UpdateMoveOptions(tempBoard, !whitesTurn);
                                if (Check(tempBoard, whitesTurn, true))
                                {
                                    tempRemove.Add(item);
                                }
                                tempBoard = GetCopyOfBoard(boardPieces);
                            }
                            foreach (var item in tempRemove)
                            {
                                tempMoves2.Remove(item);
                            }
                            if (tempMoves2.Count == 0)
                            {
                                possibleRemoval.Add(piece);
                            }
                            boardPieces[i, v].possibleMoves = tempMoves2;
                        }
                        else
                        {
                            List<string> tempRemove = new List<string>();
                            foreach (string move in tempMoves)
                            {
                                int row = int.Parse(move.Substring(0, 1));
                                int col = int.Parse(move.Substring(1, 1));
                                tempBoard[row, col] = tempBoard[i, v];
                                tempBoard[i, v] = null;
                                UpdateMoveOptions(tempBoard, !whitesTurn);
                                if (Check(tempBoard, whitesTurn, true))
                                {
                                    tempRemove.Add(move);
                                }
                                tempBoard = GetCopyOfBoard(boardPieces);
                            }
                            foreach (string move in tempRemove)
                            {
                                tempMoves.Remove(move);
                            }
                            if (tempMoves.Count == 0)
                            {
                                possibleRemoval.Add(piece);
                            }
                            boardPieces[i, v].possibleMoves = tempMoves;
                        }
                    }
                    else
                    {
                        possibleRemoval.Add(piece);
                    }
                }
            }
            foreach (Piece item in possibleRemoval)
            {
                temp.Remove(item);
            }
            return temp;
        }

        public void UpdateMoveOptions( Piece[,] boardPieces)
        {
            for (int i = 0; i < aliveWhitePieces.Length; ++i)
            {
                if (aliveWhitePieces[i] != null)
                {
                    aliveWhitePieces[i].possibleMoves = PieceMovementOptions(ref aliveWhitePieces[i], boardPieces);
                }
                if (aliveBlackPieces[i] != null)
                {
                    aliveBlackPieces[i].possibleMoves = PieceMovementOptions(ref aliveBlackPieces[i], boardPieces);
                }
            }
        }

        public void UpdateMoveOptions(Piece[,] boardPieces, bool whiteMoves)
        {
            if (whiteMoves)
            {
                for (int i = 0; i < aliveWhitePieces.Length; ++i)
                {
                    if (aliveWhitePieces[i] != null)
                    {
                        aliveWhitePieces[i].possibleMoves = PieceMovementOptions(ref aliveWhitePieces[i], boardPieces);
                    }
                }
            }
            else
            {
                for (int i = 0; i < aliveBlackPieces.Length; ++i)
                {
                    if (aliveBlackPieces[i] != null)
                    {
                        aliveBlackPieces[i].possibleMoves = PieceMovementOptions(ref aliveBlackPieces[i], boardPieces);
                    }
                }
            }
        }

        public bool ChangePiece(ref Piece oldPiece, Piece newPiece)
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
            if (piece == null)
            {
                return false;
            }
            for (int i = 0; i < aliveWhitePieces.Length; ++i)
            {
                if (piece.isWhite)
                {
                    if (aliveWhitePieces[i] == piece)
                    {
                        aliveWhitePieces[i] = null;
                        OnPieceKilled(ref piece);
                        return true;
                    }
                }
                else
                {
                    if (aliveBlackPieces[i] == piece)
                    {
                        aliveBlackPieces[i] = null;
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

        public bool Check(Piece[,] boardPieces, bool whitesTurn, bool isTest)
        {
            bool myKing = false;
            List<Piece> tempEnemy = new List<Piece>();
            if (whitesTurn)
            {
                for (int i = 0; i < aliveBlackPieces.Length; ++i)
                {
                    if (aliveBlackPieces[i] != null)
                    {
                        tempEnemy.Add(aliveBlackPieces[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < aliveWhitePieces.Length; ++i)
                {
                    if (aliveWhitePieces[i] != null)
                    {
                        tempEnemy.Add(aliveWhitePieces[i]);
                    }
                }
            }
            foreach (Piece enemyPiece in tempEnemy)
            {
                foreach (string move in enemyPiece.possibleMoves)
                {
                    Piece piece = boardPieces[int.Parse(move.Substring(0, 1)), int.Parse(move.Substring(1, 1))];
                    if (piece is King)
                    {
                        myKing = true;
                        if (!isTest)
                        {
                            if (whitesTurn)
                            {
                                whiteKingInCheck = true;
                                whiteKingCheckedBy = enemyPiece;
                            }
                            else
                            {
                                blackKingInCheck = true;
                                blackKingCheckedBy = enemyPiece;
                            }
                        }
                    }
                }
            }
            if (!isTest)
            {
                if (!myKing)
                {
                    if (whitesTurn)
                    {
                        whiteKingInCheck = false;
                        whiteKingCheckedBy = null;
                    }
                    else
                    {
                        blackKingInCheck = false;
                        blackKingCheckedBy = null;
                    }
                }
            }
            return myKing;
        }

        public List<string> PieceMovementOptions(ref Piece piece, Piece[,] boardPieces)
        {
            List<string> moveOptions = new List<string>();
            if (piece == null) { return moveOptions; }
            int pieceRow = -1;
            int pieceCol = -1;
            GetPiecePosition(ref piece, boardPieces, out pieceRow, out pieceCol);
            if (pieceRow == -1)
            {
                return moveOptions;
            }
            switch (piece.pieceType)
            {
                case PieceName.PAWN:
                    {
                        moveOptions.AddRange(CheckPawnMoves(pieceRow, pieceCol, boardPieces, piece));
                        break;
                    }
                case PieceName.KING:
                    {
                        moveOptions.AddRange(CheckKingMoves(pieceRow, pieceCol, boardPieces, piece));
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

        private List<string> CheckKingMoves(int pieceRow, int pieceCol, Piece[,] boardPieces, Piece piece)
        {
            List<string> moves = CheckStraightMoves(pieceRow, pieceCol, boardPieces, piece, false);
            moves.AddRange(CheckDiagnalMoves(pieceRow, pieceCol, boardPieces, piece, false));
            if (piece.hasMoved == false)
            {
                int row = -1;
                int col = -1;
                GetPiecePosition(ref piece, boardPieces, out row, out col);
                if (boardPieces[row, col - 3].hasMoved == false)
                {
                    if (boardPieces[row, col - 2] == null && boardPieces[row, col - 1] == null)
                    {
                        moves.Add(row + "" + (col - 2));
                    }
                }
                if (boardPieces[row, col + 4].hasMoved == false)
                {
                    if (boardPieces[row, col + 3] == null && boardPieces[row, col + 2] == null && boardPieces[row, col + 1] == null)
                    {
                        moves.Add(row + "" + (col + 2));
                    }
                }
            }
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
                if (boardPieces[pieceRow + direction, pieceCol + 1] != null && boardPieces[pieceRow + direction, pieceCol + 1].isWhite != p.isWhite)
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
