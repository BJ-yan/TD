using System;
using System.Collections.Generic;
using System.IO;
using Match3.Core.Level;

namespace Match3.Core.Generate
{
    /// <summary>
    /// 生成方案：可含多个 Sequence，并带总数/轮转/MinMax 等门控字段。
    /// 抽签本身在 Sequence.GenerateInner；这里只转发当前 SequenceIndex。
    /// </summary>
    public class GenerateScheme : IGenerateConfig
    {
       public List<GenerateSequence> Sequence  { get; set; }
       
       public int Id { get; set; }
       public bool IsClone { get; set; }
       public bool IsActive { get; set; }

       public int TotalCount { get; set; }
       public int GenerateCount { get; set; }
       public int Round { get; set; }
       public int SequenceIndex { get; set; }
       public int RoundGap { get; set; }
       public int MinCount { get; set; }
       public int MaxCount { get; set; }
       public bool IsShare { get; set; }
       public int[] ConditionIds { get; set; }
       public int Priority { get; set; }
       
       public int ClassTypeId { get; } = (int)EGenerateConfigType.GenerateScheme;
       
       private LevelBase m_Level;

       public void SetLevel(LevelBase level)
       {
           m_Level = level;
           if (Sequence == null) return;
           for (var i = 0; i < Sequence.Count; i++)
               Sequence[i].SetLevel(level);
       }
       
       public virtual long RequestOnceGenerate(IGenerateDynamicModifier modifier)
       {
           if (Sequence == null || Sequence.Count == 0) return -1;

           var currentSequence = Sequence[SequenceIndex];

           if (currentSequence == null) return -1;
           return currentSequence.RequestOnceGenerate(modifier);
       }
       
#region Storage
       
       public void Serialize(BinaryWriter writer, bool containSequence = false)
       {
           writer.Write(Id);
           writer.Write(IsClone);
           writer.Write(IsActive);
           writer.Write(IsShare);
           writer.Write(TotalCount);
           writer.Write(GenerateCount);
           writer.Write(SequenceIndex);
           writer.Write(Round);
           writer.Write(RoundGap);
           writer.Write(MinCount);
           writer.Write(MaxCount);
           writer.Write(Priority);
           
           var length = ConditionIds?.Length ?? 0;
           writer.Write(length);

           for (var i = 0; i < length; i++)
               writer.Write(ConditionIds[i]);

           if (!containSequence) return;

           var sequenceCount = Sequence.Count;
           writer.Write(sequenceCount);

           for (var i = 0; i < sequenceCount; i++)
               Sequence[i].Serialize(writer);
       }
       
       public void Deserialize(GScheme scheme)
       {
           Id = scheme.id;
           IsClone = false;
           IsActive = scheme.isActive;
           IsShare = scheme.isShare;
           TotalCount = scheme.totalCount;
           GenerateCount = scheme.generateCount;
           SequenceIndex = 0;
           Round = 0;
           RoundGap = scheme.roundGap;
           MinCount = scheme.minCount;
           MaxCount = scheme.maxCount;
           var conditionIdLength = scheme.conditionIds?.Length ?? 0;
           ConditionIds = new int[conditionIdLength];
           for (var i = 0; i < conditionIdLength; i++)
               ConditionIds[i] = scheme.conditionIds[i];
           Sequence = new List<GenerateSequence>();
           foreach (var gsequence in scheme.sequences)
           {
               var sequence = new GenerateSequence();
               sequence.SetLevel(m_Level);
               sequence.Deserialize(gsequence);
               Sequence.Add(sequence);
           }
       }
       
       public virtual GenerateScheme Clone()
       {
           // Jex: Pool.GetResuableGenerateScheme()；Demo 用 new
           var clone = new GenerateScheme();
           clone.SetLevel(m_Level);
           clone.Id = Id;
           clone.IsClone = true;
           clone.Sequence = Sequence; // 共享 Sequence 列表（与 Jex 一致）
           clone.IsActive = IsActive;
           clone.IsShare = IsShare;
           clone.TotalCount = TotalCount;
           clone.GenerateCount = GenerateCount;
           clone.RoundGap = RoundGap;
           clone.MinCount = MinCount;
           clone.MaxCount = MaxCount;
           clone.Priority = Priority;
           clone.ConditionIds = new int[ConditionIds.Length];
           for (var i = 0; i < ConditionIds.Length; i++)
               clone.ConditionIds[i] = ConditionIds[i];
           return clone;
       }
       
       public virtual void Clear()
       {
           if (!IsClone && Sequence != null)
           {
               foreach (var sequence in Sequence)
                   sequence.Clear();
               Sequence.Clear();
           }
           Sequence = null;
           Id = 0;
           IsClone = false;
           IsActive = false;
           TotalCount = 0;
           GenerateCount = 0;
           Round = 0;
           SequenceIndex = 0;
           RoundGap = 0;
           MinCount = 0;
           MaxCount = 0;
           IsShare = false;
           ConditionIds = Array.Empty<int>();
           m_Level = null;
       }
#endregion
    }
}