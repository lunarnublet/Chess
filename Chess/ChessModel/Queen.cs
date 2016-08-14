using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Queen: Piece
    {
        public Queen(bool isWhite) : base(isWhite)
        {
            pieceType = PieceName.QUEEN;
            MoveSet = new Behavior[2] { Behavior.STRAIGHT, Behavior.DIAGONAL };
        }

        public override string ToString()
        {
            return base.ToString() + "Q";
        }
    }
}
