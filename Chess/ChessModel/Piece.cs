using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ChessModel
{
    public enum PieceName { KING, QUEEN, ROOK, BISHOP, KNIGHT, PAWN };
    public abstract class Piece
    {
        public PieceName pieceType;
        public bool isWhite;
        public bool hasMoved;
        public BitmapImage image;
        public List<string> possibleMoves;
        
        public Piece(bool isWhite)
        {
            this.isWhite = isWhite;
            this.hasMoved = false;
            this.possibleMoves = new List<string>();
        }

        public override string ToString()
        {
            if (isWhite) { return "W"; }
            else { return "B"; }
        }
    }
}
