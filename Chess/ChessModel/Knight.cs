using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    class Knight:Piece
    {
        public Knight()
        {
            pieceType = PieceName.KNIGHT;
            behavior = new MoveSet[2] { MoveSet.SINGLE, MoveSet.L};
        }
    }
}
