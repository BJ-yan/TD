namespace Match3.Core.Elements
{
    /// <summary>
    /// 元素状态机，对标 Jex ElementState。
    /// </summary>
    public enum ElementState
    {
        None = 0,
        Idle,
        Swap,
        Explode,
    }
}
