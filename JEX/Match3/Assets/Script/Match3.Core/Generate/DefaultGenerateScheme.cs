using System.Collections.Generic;
using Match3.Core.Level;

namespace Match3.Core.Generate
{
    public class DefaultGenerateScheme : GenerateScheme
    {
        /// <summary>
        /// 默认/随机色生成方案：在基础色数组上按 Modifier 权重抽签。
        /// 与配置驱动的 GenerateScheme 不同：没有 Sequence/Option，直接出 long。
        /// </summary>
        
        private readonly LevelBase m_Level;
        private byte[] m_ElementTypes;

        public DefaultGenerateScheme(LevelBase level)
        {
            m_Level = level;
        }

        /// <summary>
        /// 传入基础色 typeId 列表，例如 {1,2,3,4,5} 对应红蓝绿黄紫。
        /// </summary>
        public void Init(byte[] elementTypes)
        {
            m_ElementTypes = elementTypes;
        }
        
        public override long RequestOnceGenerate(IGenerateDynamicModifier modifier)
        {
            var totalWeight = 0;
            for (var i = 0; i < m_ElementTypes.Length; i++)
            {
                var type = (int)m_ElementTypes[i];
                type = m_Level.ShuffleColorMap.GetValueOrDefault(type, type);
                var weight = modifier?.ModifyWeight(type) ?? 100;
                totalWeight += weight;
            }
            // 复用变量名：递减筛选
            totalWeight = m_Level.Random.RandRange(1, totalWeight + 1);
            for (var i = 0; i < m_ElementTypes.Length; i++)
            {
                var type = (int)m_ElementTypes[i];
                type = m_Level.ShuffleColorMap.GetValueOrDefault(type, type);
                var weight = modifier?.ModifyWeight(type) ?? 100;
                totalWeight -= weight;
                if (totalWeight <= 0)
                {
                    return type << 18 | 1 << 4;
                }
            }
            
            // 位写入
            var defaultType = m_Level.ShuffleColorMap.GetValueOrDefault(
                m_ElementTypes[0], m_ElementTypes[0]);
            return defaultType << 18 | 1 << 4;
        }
        
        /// <summary>
        /// 默认方案共享单例语义，Clone 返回自身（与 Jex 一致）。
        /// </summary>
        public override GenerateScheme Clone()
        {
            return this;
        }
    }
}