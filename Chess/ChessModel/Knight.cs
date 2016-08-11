using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Knight:Piece
    {
        public Knight()
        {
            pieceType = PieceName.KNIGHT;
            MoveSet = new Behavior[2] { Behavior.SINGLE, Behavior.L};
        }
    }
}
