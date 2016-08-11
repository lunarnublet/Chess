using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public enum Behavior { DIAGONAL, STRAIGHT, SINGLE, L, FORWARD, MULTIPLE};
    public enum PieceName { KING, QUEEN, ROOK, BISHOP, KNIGHT, PAWN };
    public abstract class Piece
    {
        public PieceName pieceType;
        public Behavior[] MoveSet;
    }
}
