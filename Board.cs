using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace СlassicCheckers.Models
{
    public class Board : IEnumerable<Cell>
    {
        private readonly Cell[,] _area;

        public CellValueEnum this[int row, int column]
        {
            get => _area[row, column].CellValueEnum;
            set => _area[row, column].CellValueEnum = value;
        }

        public Board()
        {
            _area = new Cell[8, 8];
            for (int i = 0; i < _area.GetLength(0); i++)
            {
                for (int j = 0; j < _area.GetLength(1); j++)
                {
                    _area[i, j] = new Cell(i, j, this);
                }
            }
        }

        public bool VictoryCondition(CellValueEnum currentPlayer)
        {
            int opponentCount = 0;

            foreach (var cell in _area)
            {
                if (cell.CellValueEnum != currentPlayer && cell.CellValueEnum != CellValueEnum.Empty)
                {
                    opponentCount++;
                }
            }

            return opponentCount == 0;
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return _area.Cast<Cell>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _area.GetEnumerator();
        }
    }
}
