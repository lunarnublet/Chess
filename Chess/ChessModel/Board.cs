using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChessModel
{
    public class Board
    {
        const int boardSize = 8;
        public Piece[,] boardPieces = new Piece[boardSize, boardSize];

        public Board()
        {
            
        }

        public bool PlacePiece(int row, int col, ref Piece piece)
        {
            if (boardPieces[row, col] == null)
            {
                boardPieces[row, col] = piece;
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

    }
}
