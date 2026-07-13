using Core.Interfaces;
using Core.Structs;

namespace App.Interfaces
{
    public interface ISequenceDetector<TGridSlot> where TGridSlot : IGridSlot
    {
        ItemSequence<TGridSlot> GetSequence(IGameBoard<TGridSlot> gameBoard, GridPosition gridPosition);
    }
}