namespace Match3.Core.Elements
{
    /// <summary>
    /// 伤害类型，对标 Jex DamageType（P-004 先保留核心几种，后续扩展）。
    /// </summary>
    public enum DamageType
    {
        FallMerge3 = 0,
        SwapMerge3,
        NearMatch,
        Click,
        Special,
        SpecialSwap,
    }
}
