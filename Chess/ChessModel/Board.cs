using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChessModel
{
    public class Board
    {
        const int boardSize = 8;
        public String[,] spaces = new String[boardSize, boardSize];

        public Board()
        {
            
        }

        public bool InitializeReaderTest(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            while(!sr.EndOfStream)
            {
                string placementRaw = sr.ReadLine();
                Console.WriteLine(LineMessage(placementRaw));
            }
            return true;
        }

        public string LineMessage(string lineRaw)
        {
            string line;
            ParseString(lineRaw, out line);
            int checkingInt;
            if (!ParseInt(line, 1, out checkingInt))
            {
                return lineRaw + " Places a piece on the board.";
            }
            else
            {
                return lineRaw + " Moves a piece on the board.";
            }
        }

        public bool Initialize(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            while (!sr.EndOfStream)
            {
                string placementRaw = sr.ReadLine();
                PlacePiece(placementRaw);
            }
            return true;
        }

        public bool PlacePiece(string placementRaw)
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
                if (spaces[placeInt, placeChar - 65] != null){ return false; }
                spaces[placeInt, placeChar - 65] = place1.ToString() + place2.ToString();
            }
            return true;
        }

        public override string ToString()
        { 
            string temp = "    A    B    C    D    E    F    G    H\n" + "  -----------------------------------------\n";
            for (int i = 0; i < boardSize; ++i)
            {
                temp += (-i + 8) + " | ";
                for (int v = 0; v < boardSize; ++v)
                {
                    if (spaces[i, v] == null)
                    {
                        temp += "  ";
                    }
                    else
                    {
                        temp += spaces[i, v];
                    }
                    temp += " | ";
                }
                temp += "\n  -----------------------------------------\n";
            }
            return temp;
        }

        public bool TakeSquare(string moveRaw, bool isWhite)
        {
            string move;
            if (ParseString(moveRaw, out move))
            {
                char fromChar;
                int fromInt;
                char toChar;
                int toInt;
                if (!ParseChar(move, 0, out fromChar)) { return false; }
                if (!ParseInt(move, 1, out fromInt)) { return false; }
                if (!ParseChar(move, 2, out toChar)) { return false; }
                if (!ParseInt(move, 3, out toInt)) { return false; }
                if (isWhite)
                {
                    if (spaces[fromInt, fromChar - 65][0] != 'W') { return false; }
                }
            }

            return true;

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
    }
}
