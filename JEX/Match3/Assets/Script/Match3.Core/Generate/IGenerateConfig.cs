namespace Match3.Core.Generate
{
    // 生成配置
    public interface IGenerateConfig
    {
        int ClassTypeId { get; }
        void Clear();
    }
}
