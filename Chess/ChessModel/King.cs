using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    class King : Piece
    {
        public King()
        {
            pieceType = PieceName.KING;
            behavior = new MoveSet[3] { MoveSet.SINGLE, MoveSet.STRAIGHT, MoveSet.DIAGONAL };
        }
    }
}
