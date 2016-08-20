using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessModel;
using ChessController;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isMove;
        int rowSelected;
        int colSelected;
        List<Piece> availablePieces;
        Canvas[,] canvases;
        Game game;
        Board board;

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Board_BoardPiecesChanged(Piece[,] boardPieces)
        {
            int i = 0;
            int v = 0;
            foreach (object mainChild in MainPanel.Children)
            {
                if (mainChild is StackPanel)
                {
                    foreach (object child in ((StackPanel)mainChild).Children)
                    {
                        if (child is Canvas)
                        {
                            canvases[i, v] = (Canvas)child;
                            foreach (object canvasChild in ((Canvas)child).Children)
                            {

                                if (canvasChild is Image)
                                {
                                    
                                    if (boardPieces[i, v] != null)
                                    {
                                        try
                                        {
                                            ((Image)canvasChild).Source = boardPieces[i, v].image;
                                        }
                                        catch (InvalidCastException) { }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            ((Image)canvasChild).Source = null;
                                        }
                                        catch (InvalidCastException) { }

                                    }
                                    ++v;
                                    if (v == 8)
                                    {
                                        v = 0;
                                        ++i;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Initialize()
        {
            isMove = false;
            availablePieces = new List<Piece>();
            game = new Game();
            game.Run();
            board = game.getBoard();
            board.BoardPiecesChanged += Board_BoardPiecesChanged;
            canvases = new Canvas[Board.boardSize, Board.boardSize];
            Board_BoardPiecesChanged(board.BoardPieces);
            NewTurn();
        }

        private void NewTurn()
        {
            availablePieces = game.OnNewTurn();
            DisplayAvailablePieceBorders();
        }

        private void DisplayAvailablePieceBorders()
        {
            foreach (Piece piece in availablePieces)
            {
                for (int i = 0; i < Board.boardSize; ++i)
                {
                    for (int v = 0; v < Board.boardSize; ++v)
                    {
                        if (board.BoardPieces[i, v] == piece)
                        {
                            foreach (object child in canvases[i, v].Children)
                            {
                                if (child is Border)
                                {
                                    ((Border)child).BorderBrush = Brushes.DarkSlateBlue;
                                    ((Border)child).Visibility = Visibility.Visible;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MainPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = Mouse.GetPosition(MainPanel);
            if (isMove)
            {
                if (e.ClickCount == 1) // for double-click, remove this condition if only want single click
                {
                    int row = 0;
                    int col = 0;
                    row = (int)(point.Y / 75);
                    col = (int)(point.X / 75);

                    if (game.OnMove(ref board.BoardPieces[rowSelected, colSelected], row, col))
                    {
                        HideCanvasBorders(true);
                        isMove = false;
                        NewTurn();
                    }
                }
            }
            else
            {
                if (e.ClickCount == 1)
                {
                    rowSelected = (int)(point.Y / 75);
                    colSelected = (int)(point.X / 75);
                    if (ShowPossibleMoveBorders(game.OnCheckMoves(ref board.BoardPieces[rowSelected, colSelected])))
                    {
                        foreach (object child in canvases[rowSelected, colSelected].Children)
                        {
                            if (child is Border)
                            {
                                ((Border)child).BorderBrush = Brushes.Blue;
                                ((Border)child).Visibility = Visibility.Visible;
                            }
                        }
                        isMove = true;
                    }
                }
            }
        }

        private void MainPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            HideCanvasBorders(false);
            DisplayAvailablePieceBorders();
            isMove = false;
        }

        private bool ShowPossibleMoveBorders(List<string> list)
        {
            if (list == null)
            {
                return false;
            }
                foreach (var item in list)
                {
                    int row;
                    int col;
                    if (!int.TryParse(item.Substring(0, 1), out row))
                    {
                        return false;
                    }
                    if (!int.TryParse(item.Substring(1, 1), out col))
                    {
                        return false;
                    }
                    foreach (object child in canvases[row, col].Children)
                    {
                        if (child is Border)
                        {
                            if (board.BoardPieces[row, col] == null)
                            {
                                ((Border)child).BorderBrush = Brushes.Black;
                                ((Border)child).Visibility = Visibility.Visible;
                            }
                            else
                            {
                                ((Border)child).BorderBrush = Brushes.Red;
                                ((Border)child).Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
        return true;
        }

        private void HideCanvasBorders( bool HidePossibleSelections)
        {
            foreach (object mainChild in MainPanel.Children)
            {
                if (mainChild is StackPanel)
                {
                    foreach (object child in ((StackPanel)mainChild).Children)
                    {
                        if (child is Canvas)
                        {
                            foreach (object canvasChild in ((Canvas)child).Children)
                            {
                                if (canvasChild is Border)
                                {
                                    if (((Border)canvasChild).BorderBrush == Brushes.DarkSlateBlue)
                                    {
                                        if (HidePossibleSelections)
                                        {
                                            ((Border)canvasChild).Visibility = Visibility.Hidden;
                                        }
                                    }
                                    else
                                    {
                                        ((Border)canvasChild).Visibility = Visibility.Hidden;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
