using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;
using System.IO;

namespace ChessController
{
    class FileIO
    {
        public bool Save(ref Board board)
        {
            return false;
        }
        public bool Load(ref Board board, string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            while (!sr.EndOfStream)
            {
                string placementRaw = sr.ReadLine();
                PlacePiece(placementRaw, ref board);
            }
            return true;
        }

        public bool PlacePiece(string placementRaw, ref Board board)
        {
            string move;
            if (ParseString(placementRaw, out move))
            {
                char placeChar;
                int placeInt;
                char place1;
                char place2;
                if (!ParseChar(move, 2, out placeChar)) { return false; }
                if (!ParseInt(move, 3, out placeInt)) { return false; }
                if (!ParseChar(move, 0, out place1)) { return false; }
                if (!ParseChar(move, 1, out place2)) { return false; }

                Piece piece = null;
                bool isWhite = true;
                if (place1 == 'B')
                {
                    isWhite = false;
                }
                switch (place2)
                {
                    case 'R':
                        piece = new Rook(isWhite);
                        break;
                    case 'N':
                        piece = new Knight(isWhite);
                        break;
                    case 'B':
                        piece = new Bishop(isWhite);
                        break;
                    case 'K':
                        piece = new King(isWhite);
                        break;
                    case 'Q':
                        piece = new Queen(isWhite);
                        break;
                    case 'P':
                        piece = new Pawn(isWhite);
                        break;
                }
                board.PlacePiece(placeInt, placeChar - 65, ref piece);
            }
            return true;
        }

        public bool ParseChar(string move, int position, out char outChar)
        {
            try
            {
                outChar = move[position];
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                outChar = '\0';
                return false;
            }
        }

        public bool ParseInt(string move, int position, out int outInt)
        {
            int moveInt;
            try
            {
                if (!int.TryParse(move.Substring(position, 1), out moveInt))
                {
                    outInt = -1;
                    return false;
                }
                outInt = -moveInt + 8;
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                outInt = 0;
                return false;
            }
        }

        public bool ParseString(string moveRaw, out string outMove)
        {
            moveRaw = moveRaw.ToUpper();
            string[] moveArray = moveRaw.Split(new Char[] { ' ' });
            string move = "";
            for (int i = 0; i < moveArray.Length; ++i)
            {
                move += Convert.ToString(moveArray[i]);
            }
            outMove = move;
            return true;
        }
    }
}
