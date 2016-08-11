using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class King : Piece
    {
        public King()
        {
            pieceType = PieceName.KING;
            MoveSet = new Behavior[3] { Behavior.SINGLE, Behavior.STRAIGHT, Behavior.DIAGONAL };
        }
    }
}
