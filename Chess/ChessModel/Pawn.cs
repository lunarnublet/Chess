using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Pawn : Piece
    {
        public Pawn(bool isWhite):base(isWhite)
        {
            pieceType = PieceName.PAWN;
        }

        public override string ToString()
        {
            return base.ToString() + "P";
        }
    }
}
