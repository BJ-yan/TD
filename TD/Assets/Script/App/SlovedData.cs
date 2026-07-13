using System.Collections.Generic;
using Core.Interfaces;

namespace App
{
    /// <summary>
    /// SolvedData 是三消 SDK 里一次「消除检测结果」的载体。它不执行消除、不播动画，只把「哪些格子要消」「哪些特殊格子要处理」打包好，交给下游消费。
    /// </summary>
    public class SolvedData<TGridSlot> where TGridSlot : IGridSlot
    {
        public SolvedData(IReadOnlyCollection<ItemSequence<TGridSlot>> solvedSequences,
            IReadOnlyCollection<TGridSlot> specialItemGridSlots)
        {
            SolvedSequences = solvedSequences;
            SpecialItemGridSlots = specialItemGridSlots;
        }

        public IReadOnlyCollection<TGridSlot> SpecialItemGridSlots { get; }
        public IReadOnlyCollection<ItemSequence<TGridSlot>> SolvedSequences { get; }

        public IEnumerable<TGridSlot> GetSolvedGridSlots(bool onlyMovable = false)
        {
            foreach (var sequence in SolvedSequences)
            {
                foreach (var solvedGridSlot in sequence.SolvedGridSlots)
                {
                    if (onlyMovable && solvedGridSlot.IsMovable == false)
                    {
                        continue;
                    }

                    yield return solvedGridSlot;
                }
            }
        }

        public IEnumerable<TGridSlot> GetSpecialItemGridSlots(bool excludeOccupied = false)
        {
            foreach (var specialItemGridSlot in SpecialItemGridSlots)
            {
                if (excludeOccupied && specialItemGridSlot.HasItem)
                {
                    continue;
                }

                yield return specialItemGridSlot;
            }
        }
    }
}