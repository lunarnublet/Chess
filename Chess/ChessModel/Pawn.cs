﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    class Pawn : Piece
    {
        public Pawn()
        {
            pieceType = PieceName.PAWN;
            behavior = new MoveSet[2] { MoveSet.SINGLE, MoveSet.FORWARD };
        }
    }
}
