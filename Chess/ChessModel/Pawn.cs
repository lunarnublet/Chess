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
            if (isWhite)
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:\\cSharp\\Chess\\Chess\\Chess\\PiecePNGs\\White Pawn.png")); 
            }
            else
            {
                image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:\\cSharp\\Chess\\Chess\\Chess\\PiecePNGs\\Black Pawn.png"));
            }
        }
    

        public override string ToString()
        {
            return base.ToString() + "P";
        }
    }
}
