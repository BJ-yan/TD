using System.Collections.Generic;
using System.IO;
using Match3.Core.Env;
using Match3.Core.Level;

namespace Match3.Core.Generate
{
    /// <summary>
    /// 生成序列：持有多个 Option，按权重抽签得到打包 long。
    /// Scheme 只负责选「当前是哪条 Sequence」；真正抽签在这里。
    /// </summary>
    public class GenerateSequence : IGenerateConfig
    {
        public List<GenerateOption> Options { get; set; }
        public int ClassTypeId { get; } = (int)EGenerateConfigType.GenerateSequence;
        
        private LevelBase m_Level;

        public void SetLevel(LevelBase level)
        {
            m_Level = level;
        }

        /// <summary>
        /// 加权抽签：Σweight → RandRange(1,total+1) → 递减命中 Option。
        /// typeId==0 → RandomColorGenerateScheme；否则 ShuffleColorMap + Modify(18,14)。
        /// </summary>
        public long GenerateInner(IGenerateDynamicModifier modifier)
        {
            var totalWeight = 0;
            foreach (var option in Options)
                totalWeight += option.generateWeight;
            
            var randomWeight = m_Level.Random.RandRange(1, totalWeight + 1);
            foreach (var option in Options)
            {
                randomWeight -= option.generateWeight;
                /// 当前未命中
                if (randomWeight > 0)
                    continue;

                /// 命中拿取当前元素类型
                var typeId = option.elementTypeId;

                // 随机元素拿默认
                if (typeId == 0)
                    return m_Level.RandomColorGenerateScheme.RequestOnceGenerate(modifier);
                
                typeId = m_Level.ShuffleColorMap.GetValueOrDefault(typeId, typeId);
                return LevelUtil.Modify(option.value, 18, 14, typeId);
            }

            return -1;
        }

        public long RequestOnceGenerate(IGenerateDynamicModifier modifier)
        {
            var result = GenerateInner(modifier);
            if (result < 0) return -1;
            
            return result;
        }
        
#region Storage
        public void Deserialize(GSequence sequence)
        {
            Options = new List<GenerateOption>();
            foreach (var goption in sequence.options)
            {
                var option = new GenerateOption(); // Jex: Pool.GetResuableGenerateOption()
                option.Deserialize(goption);
                Options.Add(option);
            }
        }
                
        public void Serialize(BinaryWriter writer)
        {
            var optionsCount = Options.Count;
            writer.Write(optionsCount);
            for (var i = 0; i < optionsCount; i++)
                Options[i].Serialize(writer);
        }
        public void Deserialize(BinaryReader reader)
        {
            Options = new List<GenerateOption>();
            var optionsCount = reader.ReadInt32();
            for (var i = 0; i < optionsCount; i++)
            {
                var option = new GenerateOption();
                option.Deserialize(reader);
                Options.Add(option);
            }
        }
        
        public void Clear()
        {
            if (Options == null) return;
            foreach (var option in Options)
                option.Clear(); // Jex: Pool.Release…
            Options.Clear();
        }
#endregion
    }
}