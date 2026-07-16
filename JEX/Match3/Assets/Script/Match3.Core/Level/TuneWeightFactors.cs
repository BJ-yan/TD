namespace Match3.Core.Level
{
    /// <summary>
    /// 流畅度权重因子（对标 Jex TuneWeightFactors 的最小字段集）。
    /// </summary>
    public struct TuneWeightFactors
    {
        /// <summary>近邻（上/左/右）同色时的乘算系数。</summary>
        public float k;
        /// <summary>斜向近邻同色时的乘算系数。</summary>
        public float m;
        /// <summary>稍远邻域同色时的乘算系数。</summary>
        public float l;
        /// <summary>rate 下限（Clamp）。</summary>
        public float min;
        /// <summary>rate 上限（Clamp）。</summary>
        public float max;
    }
}