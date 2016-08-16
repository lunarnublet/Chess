using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Knight:Piece
    {
        public Knight(bool isWhite) : base(isWhite)
        {
            pieceType = PieceName.KNIGHT;
        }

        public override string ToString()
        {
            return base.ToString() + "N";
        }
    }
}
