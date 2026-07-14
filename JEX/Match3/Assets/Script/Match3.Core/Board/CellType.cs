namespace Match3.Core.Board
{
    /// <summary>
    /// 位置格子类型.
    /// </summary>
    public enum CellType
    {
        /// <summary>不可通行，不参与逻辑。</summary>
        Block = 0,
        /// <summary>空白占位格。</summary>
        Blank,
        /// <summary>普通可玩格。</summary>
        Normal,
        /// <summary>出生口/生成格。</summary>
        Generate,
    }
}
