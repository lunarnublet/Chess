using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    class Rook : Piece
    {
        public Rook()
        {
            pieceType = PieceName.ROOK;
            behavior = new MoveSet[2] { MoveSet.MULTIPLE, MoveSet.STRAIGHT };
        }
    }
}
