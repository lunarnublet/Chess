using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;

namespace ChessController
{
    public interface IController
    {
        List<string> OnCheckMoves(ref Piece piece);
        bool OnMove(ref Piece piece, int newRow, int newCol);
        Board getBoard();
    }
}
