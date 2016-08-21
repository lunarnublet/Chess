using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Rook : Piece
    {
        public Rook(bool isWhite) : base(isWhite)
        {
            pieceType = PieceName.ROOK;
            //if (isWhite) { imageURI = "C:\\Chess\\Chess\\Chess\\PiecePNGs\\White Rook.png"; }
            //else { imageURI = "C:\\Chess\\Chess\\Chess\\PiecePNGs\\Black Rook.png"; }
            if (isWhite)
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:\\cSharp\\Chess\\Chess\\Chess\\PiecePNGs\\White Rook.png"));
            }
            else
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:\\cSharp\\Chess\\Chess\\Chess\\PiecePNGs\\Black Rook.png"));
            }
        }

        public override string ToString()
        {
            return base.ToString() + "R";
        }
    }
}
