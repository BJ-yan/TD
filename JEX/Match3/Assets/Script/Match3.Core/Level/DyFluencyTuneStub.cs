namespace Match3.Core.Level
{
    /// <summary>
    /// DyFluencyTune 最小 Stub。
    /// 用途：给 UpdateModifier 提供元素系数与权重上下限 / KML，无需真实 DyFt 模块。
    /// </summary>
    public class DyFluencyTuneStub
    {
        /// <summary>
        /// 五色各自的额外倍率，下标 0..4 对应 typeId 1..5。
        /// 用途：rate[i] *= ElementFactors[i]，在邻域调整之后做全局色偏。
        /// </summary>
        public float[] ElementFactors { get; set; } = { 1f, 1f, 1f, 1f, 1f };
        /// <summary>
        /// K/M/L 邻域系数与 Clamp 上下限。
        /// 用途：IsDyFtOn 时乘到对应 rate；无论开关，最后都用 min/max 夹紧。
        /// </summary>
        public TuneWeightFactors WeightFactors { get; set; } = new TuneWeightFactors
        {
            k = 1f,
            m = 1f,
            l = 1f,
            min = 0f,
            max = 10f
        };
    }
}