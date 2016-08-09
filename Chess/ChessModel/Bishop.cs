using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    class Bishop : Piece
    {
        public Bishop()
        {
            pieceType = PieceName.BISHOP;
            behavior = new MoveSet[2] { MoveSet.MULTIPLE, MoveSet.DIAGONAL };
        }
    }
}
