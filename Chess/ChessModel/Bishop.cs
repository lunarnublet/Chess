using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            pieceType = PieceName.BISHOP;
            //if (isWhite) { imageURI = "C:\\Chess\\Chess\\Chess\\PiecePNGs\\White Bishop.png"; }
            //else { imageURI = "C:\\Chess\\Chess\\Chess\\PiecePNGs\\Black Bishop.png"; }
            if (isWhite)
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:\\cSharp\\Chess\\Chess\\Chess\\PiecePNGs\\White Bishop.png"));
            }
            else
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:\\cSharp\\Chess\\Chess\\Chess\\PiecePNGs\\Black Bishop.png"));
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            return base.ToString() + "B";
        }
    }
}
