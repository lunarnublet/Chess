using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Rook : Piece
    {
        public bool hasBeenMoved;
        public Rook(bool isWhite) : base(isWhite)
        {
            pieceType = PieceName.ROOK;
            MoveSet = new Behavior[1] {Behavior.STRAIGHT };
            hasBeenMoved = false;
        }

        public override string ToString()
        {
            return base.ToString() + "R";
        }
    }
}
