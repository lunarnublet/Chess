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
            //if (isWhite) { imageURI = "C:\\Chess\\Chess\\Chess\\PiecePNGs\\White King.png"; }
            //else { imageURI = "C:\\Chess\\Chess\\Chess\\PiecePNGs\\Black King.png"; }
            if (isWhite)
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("C:\\Chess\\Chess\\Chess\\PiecePNGs\\White King.png"));
            }
            else
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("C:\\Chess\\Chess\\Chess\\PiecePNGs\\Black King.png"));
            }
        }

        public override string ToString()
        {
            return base.ToString() + "K";
        }
    }
}
