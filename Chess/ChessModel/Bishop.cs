﻿using System;
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
        }

        public override string ToString()
        {
            return base.ToString() + "B";
        }
    }
}
