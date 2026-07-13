using Core.Interfaces;

namespace App.Interfaces
{
    public interface ILevelGoalsProvider<TGridSlot> where TGridSlot : IGridSlot
    {
        LevelGoal<TGridSlot>[] GetLevelGoals(int level, IGameBoard<TGridSlot> gameBoard);
    }
}