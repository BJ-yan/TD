using Core.Interfaces;
using Core.Structs;

namespace App.Interfaces
{
    public interface IGameBoard<out TGridSlot> : IGrid where TGridSlot : IGridSlot
    {
        TGridSlot this[GridPosition position] { get; }
        TGridSlot this[int rowIndex, int columnIndex] { get; }

        bool IsPositionOnBoard(GridPosition gridPosition);
    }
}
