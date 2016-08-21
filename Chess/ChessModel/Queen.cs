using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Queen: Piece
    {
        public Queen(bool isWhite) : base(isWhite)
        {
            pieceType = PieceName.QUEEN;
            if (isWhite)
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:\\cSharp\\Chess\\Chess\\Chess\\PiecePNGs\\White Queen.png"));
            }
            else
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:\\cSharp\\Chess\\Chess\\Chess\\PiecePNGs\\Black Queen.png"));
            }
        }

        public override string ToString()
        {
            return base.ToString() + "Q";
        }
    }
}
