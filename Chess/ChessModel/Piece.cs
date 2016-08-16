using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public enum PieceName { KING, QUEEN, ROOK, BISHOP, KNIGHT, PAWN };
    public abstract class Piece
    {
        public PieceName pieceType;
        public bool isWhite;
        public bool hasMoved;
        public Piece(bool isWhite)
        {
            this.isWhite = isWhite;
            this.hasMoved = false;
        }

        public override string ToString()
        {
            if (isWhite) { return "W"; }
            else { return "B"; }
        }
    }
}
