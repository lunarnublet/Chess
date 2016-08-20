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
            //if (isWhite) { imageURI = "C:\\Chess\\Chess\\Chess\\PiecePNGs\\White Knight.png"; }
            //else { imageURI = "C:\\Chess\\Chess\\Chess\\PiecePNGs\\Black Knight.png"; }
            if (isWhite)
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("C:\\Chess\\Chess\\Chess\\PiecePNGs\\White Knight.png"));
            }
            else
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("C:\\Chess\\Chess\\Chess\\PiecePNGs\\Black Knight.png"));
            }
        }

        public override string ToString()
        {
            return base.ToString() + "N";
        }
    }
}
