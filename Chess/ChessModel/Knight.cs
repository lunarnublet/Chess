using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Knight:Piece
    {
        public bool hasBeenMoved;
        public Knight(bool isWhite) : base(isWhite)
        {
            pieceType = PieceName.KNIGHT;
            hasBeenMoved = false;
        }

        public override string ToString()
        {
            return base.ToString() + "N";
        }
    }
}
