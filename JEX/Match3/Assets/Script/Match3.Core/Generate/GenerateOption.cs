using System.IO;

namespace Match3.Core.Generate
{
    public enum EGenerateConfigType
    {
        GenerateScheme = 0, // 方案：多序列 + 切换条件
        GenerateSequence,     // 序列：多选项 + 权重
        GenerateOptions       // 选项：单次生成目标
    }

    /// <summary>
    /// 它表示生成序列里一条候选配置：
    /// 要生成什么、带什么属性、有什么权重
    /// </summary>
    public class GenerateOption : IGenerateConfig
    {
        /// <summary>要生成的元素 typeId</summary>
        public int elementTypeId;

        /// <summary>元素信息，按位写入
        /// [0,4)	form
        /// [4,11)	life
        /// [11,18)	partId
        /// [18,32)	typeId
        /// [32,42)	instanceId
        /// [42,64)	扩展自定义属性
        /// </summary>
        public long value;
        
        /// <summary>生成权重</summary>
        public int generateWeight;

        public int ClassTypeId { get; } = (int)EGenerateConfigType.GenerateOptions;
        
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(elementTypeId);
            writer.Write(value);
            writer.Write(generateWeight);
        }
        
        public void Deserialize(BinaryReader reader)
        {
            elementTypeId = reader.ReadInt32();
            value = reader.ReadInt64();
            generateWeight = reader.ReadInt32();
        }

        public void Deserialize(GOption option)
        {
            elementTypeId = option.typeId;
            value = option.value;
            generateWeight = option.weight;
        }
        
        public void Clear()
        {
            elementTypeId = 0;
            value = 0;
            generateWeight = 0;
        }
    }
}