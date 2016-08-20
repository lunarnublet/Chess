using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChessModel
{
    public class Board
    {
        public const int boardSize = 8;
        private Piece[,] boardPieces = new Piece[boardSize,boardSize];

        public Piece[,] BoardPieces
        {
            get { return boardPieces; }
            set { boardPieces = value; OnBoardPiecesChanged(); }
        }

        public delegate void BoardChanged(Piece[,] boardPieces);
        public event BoardChanged BoardPiecesChanged;

        public Board()
        {

        }

        public bool PlacePiece(int row, int col, ref Piece piece)
        {
            Piece[,] temp = BoardPieces;
            if (temp[row, col] == null)
            {
                temp[row, col] = piece;
                BoardPieces = temp;
                return true;
            }
            return false;
        }

        public override string ToString()
        { 
            string temp = "    A    B    C    D    E    F    G    H\n" + "  -----------------------------------------\n";
            for (int i = 0; i < boardSize; ++i)
            {
                temp += (-i + 8) + " | ";
                for (int v = 0; v < boardSize; ++v)
                {
                    if (boardPieces[i, v] == null)
                    {
                        temp += "  ";
                    }
                    else
                    {
                        temp += boardPieces[i, v].ToString();
                    }
                    temp += " | ";
                }
                temp += "\n  -----------------------------------------\n";
            }
            return temp;
        }

        public bool KillPiece(int row, int col)
        {
            try
            {
                Piece[,] temp = BoardPieces;
                temp[row, col] = null;
                BoardPieces = temp;
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public bool MovePiece(ref Piece piece, int newRow, int newCol)
        {
            Piece[,] temp = BoardPieces;
            for (int i = 0; i < boardSize; ++i)
            {
                for (int v = 0; v < boardSize; ++v)
                {
                    if (temp[i,v] == piece)
                    { 
                        try
                        {
                            temp[newRow, newCol] = piece;
                            temp[i, v] = null;
                            BoardPieces = temp;
                            return true;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        private void OnBoardPiecesChanged()
        {
            if (BoardPiecesChanged != null)
            {
                BoardPiecesChanged(BoardPieces);
            }
        }
    }
}
