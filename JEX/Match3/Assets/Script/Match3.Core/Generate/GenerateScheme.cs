using System.Collections.Generic;
using System.IO;
using Match3.Core.Env;
using Match3.Core.Level;

namespace Match3.Core.Generate
{
    public class GenerateScheme : IGenerateConfig
    {
        public List<GenerateOption> Options { get; set; }
        public int ClassTypeId { get; } = (int) EGenerateConfigType.GenerateScheme;

        private LevelBase m_Level;
        
        public void SetLevel(LevelBase level)
        {
            m_Level = level;
        }
        
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

        /// <summary>
        /// 假设Options是：
        /// A typeId = 1（红）  weight = 10
        /// B typeId = 2（蓝）  weight = 30
        /// C typeId = 0 （随机）weight = 60
        ///
        /// total weight = 100
        ///
        /// |---- 红 10 ----|-------- 蓝 30 --------|-------------- C 60 --------------|
        /// 应用进来实现权重抽选option
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        long GenerateInner(IGenerateDynamicModifier modifier)
        {
            // 每次重算权重（后续难度调控可能改 generateWeight）
            var totalWeight = 0;

            foreach (var option in Options)
                totalWeight += option.generateWeight;
            
            var randomWeight = m_Level.Random.RandRange(1, totalWeight + 1);
            foreach (var option in Options)
            {
                randomWeight -= option.generateWeight;
                if (randomWeight > 0)
                    continue;

                var typeId = option.elementTypeId;
                if (typeId == 0)
                {
                    // 随机色方案（带 Modifier）
                    return m_Level.RandomColorGenerateScheme.RequestOnceGenerate(modifier);
                }

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
        
        public void Clear()
        {
            if (Options == null) return;
            foreach (var option in Options)
                option.Clear();
            Options.Clear();
        }
    }
}