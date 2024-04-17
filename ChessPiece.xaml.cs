using System.Windows;
using System.Windows.Controls;
using СlassicCheckers.Models;

namespace СlassicCheckers.UserControls
{
    public partial class ChessPiece : UserControl
    {
        public static readonly DependencyProperty PieceProperty = DependencyProperty.Register("Piece", typeof(CellValueEnum), typeof(ChessPiece));

        public CellValueEnum Piece
        {
            get => (CellValueEnum)GetValue(PieceProperty);
            set => SetValue(PieceProperty, value);
        }

        public ChessPiece()
        {
            InitializeComponent();
        }
    }
}
