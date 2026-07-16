namespace Match3.Core.Level
{
    /// <summary>
    /// Round 最小 Stub。
    /// 用途：只提供 Number，满足 TryCreaterRound；完整 Round.Stable 等留给 W2。
    /// </summary>
    public class RoundStub
    {
        /// <summary>
        /// 当前回合号。
        /// 用途：与 Scheme.Round 比较，决定 RoundGap 周期是否触发 GenerateCount++。
        /// </summary>
        public int Number { get; set; }
    }
}