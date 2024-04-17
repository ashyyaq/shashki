using СlassicCheckers.ViewModels.Base;

namespace СlassicCheckers.Models
{
    public class Cell : ViewModel
    {
        private CellValueEnum _cellValueEnum;
        private bool _act;
        private int _row;
        private int _col;
        private Board? _board;

        public CellValueEnum CellValueEnum
        {
            get => _cellValueEnum;
            set
            {
                _cellValueEnum = value;
                OnPropertyChanged();
            }
        }

        public bool Act
        {
            get => _act;
            set
            {
                _act = value;
                OnPropertyChanged();
            }
        }

        public int Row
        {
            get => _row;
            set
            {
                _row = value;
                OnPropertyChanged();
            }
        }

        public int Column
        {
            get => _col;
            set
            {
                _col = value;
                OnPropertyChanged();
            }
        }

        public Board Board
        {
            get => _board!;
            set
            {
                _board = value;
                OnPropertyChanged();
            }
        }

        public Cell(int row, int column, Board board)
        {
            Row = row;
            Column = column;
            Board = board;
        }
    }
}
