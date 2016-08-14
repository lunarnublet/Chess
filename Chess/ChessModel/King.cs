using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class King : Piece
    {
        public King(bool isWhite) : base(isWhite)
        {
            pieceType = PieceName.KING;
        }

        public override string ToString()
        {
            return base.ToString() + "K";
        }
    }
}
