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
            // 被消除的序列
            SolvedSequences = solvedSequences;
            // 因为消除而受到影响的特殊格子
            SpecialItemGridSlots = specialItemGridSlots;
        }

        public IReadOnlyCollection<TGridSlot> SpecialItemGridSlots { get; }
        public IReadOnlyCollection<ItemSequence<TGridSlot>> SolvedSequences { get; }

        /// <summary>
        /// 遍历所有消除序列中包含的格子。
        /// </summary>
        /// <param name="onlyMovable">为 true 时仅返回可移动的格子（<see cref="IGridSlot.IsMovable"/> 为 true）。</param>
        /// <returns>消除序列中的格子集合；十字消除时同一格子可能在多条序列中出现，因此结果可能包含重复项。</returns>
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

        /// <summary>
        /// 遍历因消除而受影响的特殊格子。
        /// </summary>
        /// <param name="excludeOccupied">为 true 时跳过已有物品的格子（<see cref="IGridSlot.HasItem"/> 为 true）。</param>
        /// <returns>由 <see cref="SpecialItemGridSlots"/> 收集的特殊格子集合，不包含消除序列中的普通糖果格。</returns>
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