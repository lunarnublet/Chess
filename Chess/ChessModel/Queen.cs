using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Queen: Piece
    {
        public Queen()
        {
            pieceType = PieceName.QUEEN;
            MoveSet = new Behavior[3] { Behavior.MULTIPLE, Behavior.STRAIGHT, Behavior.DIAGONAL };
        }
    }
}
