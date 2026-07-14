namespace Match3.Core.Generate
{
    // 生成配置
    public interface IGenerateConfig
    {
        int ClassTypeId { get; }
        void Clear();
    }

    /// <summary>
    /// 随机概率的动态调整器，用来给动态难度控制准备的
    /// </summary>
    public interface IGenerateDynamicModifier
    {
        int ModifyWeight(int elementType, int originalWeight = 100);
    }
}
