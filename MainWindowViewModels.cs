using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using СlassicCheckers.Models;
using СlassicCheckers.ViewModels.Base;

namespace СlassicCheckers.ViewModels
{
    public class MainWindowViewModels : ViewModel
    {
        private Board _board = new Board();
        private ICommand? _newGameCommand;
        private ICommand? _cellCommand;
        private CellValueEnum _currentPlayer = CellValueEnum.WhiteChecker;

        public MainWindowViewModels()
        {
            SetupBoard();
        }

        public Board Board
        {
            get => _board;
            set
            {
                _board = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewGameCommand =>
            _newGameCommand ??= new Command(parameter =>
            {
                SetupBoard();
            });

        public ICommand CellCommand => _cellCommand ??= new Command(parameter =>
        {
            Cell cell = (Cell)parameter;
            Cell activeCell = Board.FirstOrDefault(x => x.Act);

            if (cell.CellValueEnum != CellValueEnum.Empty)
            {
                if (!cell.Act && (activeCell == null || cell == activeCell))
                {
                    cell.Act = true;
                }
                else
                {
                    cell.Act = false;
                }
            }
            else if (activeCell != null && activeCell.CellValueEnum == _currentPlayer)
            {
                int rowDiff = Math.Abs(activeCell.Row - cell.Row);
                int colDiff = Math.Abs(activeCell.Column - cell.Column);

                if ((rowDiff == 1 && colDiff == 1) ||
                    (rowDiff == 2 && colDiff == 2 &&
                    Board.Any(x => x.Row == (activeCell.Row + cell.Row) / 2 && x.Column == (activeCell.Column + cell.Column) / 2 &&
                    x.CellValueEnum != CellValueEnum.Empty && x.CellValueEnum != activeCell.CellValueEnum)))
                {
                    activeCell.Act = false;

                    if (rowDiff == 2 && colDiff == 2)
                    {
                        int capturedRow = (activeCell.Row + cell.Row) / 2;
                        int capturedColumn = (activeCell.Column + cell.Column) / 2;
                        Cell? capturedCell = Board.FirstOrDefault(x => x.Row == capturedRow && x.Column == capturedColumn);

                        if (capturedCell != null && capturedCell.CellValueEnum != CellValueEnum.Empty && capturedCell.CellValueEnum != activeCell.CellValueEnum)
                        {
                            capturedCell.CellValueEnum = CellValueEnum.Empty;
                        }
                    }

                    cell.CellValueEnum = activeCell.CellValueEnum;
                    activeCell.CellValueEnum = CellValueEnum.Empty;

                    if (Board.VictoryCondition(_currentPlayer))
                    {
                        if (_currentPlayer == CellValueEnum.WhiteChecker)
                        {
                            ShowEndGameMessage(false);
                            SetupBoard();
                        }
                        else
                        {
                            ShowEndGameMessage(true);
                            SetupBoard();
                        }
                    }
                    else
                    {
                        _currentPlayer = _currentPlayer == CellValueEnum.WhiteChecker ? CellValueEnum.BlackChecker : CellValueEnum.WhiteChecker;
                    }
                }
            }
        }, parameter => parameter is Cell cell && (Board.Any(x => x.Act) || (cell.CellValueEnum != CellValueEnum.Empty && cell.CellValueEnum == _currentPlayer)));

        private void SetupBoard() 
        {
            Board board = new Board();
            
            board[0, 1] = CellValueEnum.BlackChecker;
            board[0, 3] = CellValueEnum.BlackChecker;
            board[0, 5] = CellValueEnum.BlackChecker;
            board[0, 7] = CellValueEnum.BlackChecker;

            board[7, 0] = CellValueEnum.WhiteChecker;
            board[7, 2] = CellValueEnum.WhiteChecker;
            board[7, 4] = CellValueEnum.WhiteChecker;
            board[7, 6] = CellValueEnum.WhiteChecker;

            board[6, 1] = CellValueEnum.WhiteChecker;
            board[6, 3] = CellValueEnum.WhiteChecker;
            board[6, 5] = CellValueEnum.WhiteChecker;
            board[6, 7] = CellValueEnum.WhiteChecker;

            board[5, 2] = CellValueEnum.WhiteChecker;
            board[5, 4] = CellValueEnum.WhiteChecker;
            board[5, 6] = CellValueEnum.WhiteChecker;
            board[5, 0] = CellValueEnum.WhiteChecker;

            board[1, 0] = CellValueEnum.BlackChecker;
            board[1, 4] = CellValueEnum.BlackChecker;
            board[1, 6] = CellValueEnum.BlackChecker;
            board[5, 0] = CellValueEnum.BlackChecker;

            board[2, 1] = CellValueEnum.BlackChecker;
            board[2, 3] = CellValueEnum.BlackChecker;
            board[2, 5] = CellValueEnum.BlackChecker;
            board[2, 7] = CellValueEnum.BlackChecker;

            Board = board;
        }

        public static void ShowEndGameMessage(bool isWhiteWinner)
        {
            string winner = isWhiteWinner ? "чёрные шашки" : "белые шашки";
            MessageBox.Show($"Конец игры. Победитель - {winner}.", "Конец игры", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
