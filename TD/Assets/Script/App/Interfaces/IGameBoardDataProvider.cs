using Core.Interfaces;

namespace App.Interfaces
{
    public interface IGameBoardDataProvider<out TGridSlot> where TGridSlot : IGridSlot
    {
        TGridSlot[,] GetGameBoardSlots(int level);
    }
}