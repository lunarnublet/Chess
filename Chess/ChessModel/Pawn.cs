using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Pawn : Piece
    {
        public Pawn()
        {
            pieceType = PieceName.PAWN;
            MoveSet = new Behavior[2] { Behavior.SINGLE, Behavior.FORWARD };
        }
    }
}
