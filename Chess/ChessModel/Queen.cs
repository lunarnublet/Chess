using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    class Queen: Piece
    {
        public Queen()
        {
            pieceType = PieceName.QUEEN;
            behavior = new MoveSet[3] { MoveSet.MULTIPLE, MoveSet.STRAIGHT, MoveSet.DIAGONAL };
        }
    }
}
