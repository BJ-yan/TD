using Core.Interfaces;
using App.Interfaces;

namespace App.Interfaces
{
    public interface ISolvedSequencesConsumer<TGridSlot> where TGridSlot : IGridSlot
    {
        void OnSequencesSolved(SolvedData<TGridSlot> solvedData);
    }
}