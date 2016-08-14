using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public enum Behavior { DIAGONAL, STRAIGHT, L};
    public enum PieceName { KING, QUEEN, ROOK, BISHOP, KNIGHT, PAWN };
    public abstract class Piece
    {
        public PieceName pieceType;
        public Behavior[] MoveSet;
        public bool isWhite;
        public Piece(bool isWhite)
        {
            this.isWhite = isWhite;
        }

        public override string ToString()
        {
            if (isWhite) { return "W"; }
            else { return "B"; }
        }
    }
}
