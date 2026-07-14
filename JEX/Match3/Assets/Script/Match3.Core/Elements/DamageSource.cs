using Match3.Core.Board;

namespace Match3.Core.Elements
{
    /// <summary>
    /// 伤害来源，对标 Jex DamageSource（P-004 精简版，保留 Hit 链所需字段）。
    /// </summary>
    public struct DamageSource
    {
        public DamageType Type;
        public GridPos Point;
        public int SourceElementId;

        public DamageSource(DamageType type, GridPos point = default, int sourceElementId = 0)
        {
            Type = type;
            Point = point;
            SourceElementId = sourceElementId;
        }

        public long Encode()
        {
            return ((long)Type << 32) | (uint)SourceElementId;
        }
    }
}
