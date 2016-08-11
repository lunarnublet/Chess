using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Rook : Piece
    {
        public Rook()
        {
            pieceType = PieceName.ROOK;
            MoveSet = new Behavior[2] { Behavior.MULTIPLE, Behavior.STRAIGHT };
        }
    }
}
