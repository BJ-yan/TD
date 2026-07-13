using System.Collections.Generic;
using Core.Interfaces;

namespace App.Interfaces
{
    public interface ISpecialItemDetector<TGridSlot> where TGridSlot : IGridSlot
    {
        IEnumerable<TGridSlot> GetSpecialItemGridSlots(IGameBoard<TGridSlot> gameBoard, TGridSlot gridSlot);
    }
}