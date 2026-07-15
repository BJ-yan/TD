using System;
using System.IO;
namespace Match3.Core.Generate
{
    /// <summary>
    /// 运行时对应GenerateOption，
    /// 存储typeId、value、weight
    /// </summary>
    public class GOption
    {
        public int typeId;
        public long value;
        public int weight;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(typeId);
            writer.Write(value);
            writer.Write(weight);
        }
        public void Deserialize(BinaryReader reader)
        {
            typeId = reader.ReadInt32();
            value = reader.ReadInt64();
            weight = reader.ReadInt32();
        }
    }
    
    /// <summary>
    /// 运行时对应GenerateSequence，
    /// 存储一组GOPtion
    /// </summary>
    [Serializable]
    public class GSequence
    {
        public GOption[] options;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(options.Length);
            foreach (var item in options)
                item.Serialize(writer);
        }
        public void Deserialize(BinaryReader reader)
        {
            var length = reader.ReadInt32();
            options = new GOption[length];
            for (var i = 0; i < length; i++)
            {
                options[i] = new GOption();
                options[i].Deserialize(reader);
            }
        }
    }
    
    /// <summary>
    /// 运行时对应GenerateScheme，
    /// 存储一组 id、次数、Min/Max、条件、GSequence[] 等
    /// </summary>
    [Serializable]
    public class GScheme
    {
        public int id;
        public bool isActive;
        public bool isShare;
        public int totalCount;
        public int generateCount;
        public int roundGap;
        public int minCount;
        public int maxCount;
        public int[] conditionIds;
        public GSequence[] sequences;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(id);
            writer.Write(isActive);
            writer.Write(isShare);
            writer.Write(totalCount);
            writer.Write(generateCount);
            writer.Write(roundGap);
            writer.Write(minCount);
            writer.Write(maxCount);
            writer.Write(conditionIds.Length);
            foreach (var item in conditionIds)
                writer.Write(item);
            writer.Write(sequences.Length);
            foreach (var item in sequences)
                item.Serialize(writer);
        }
        public void Deserialize(BinaryReader reader)
        {
            id = reader.ReadInt32();
            isActive = reader.ReadBoolean();
            isShare = reader.ReadBoolean();
            totalCount = reader.ReadInt32();
            generateCount = reader.ReadInt32();
            roundGap = reader.ReadInt32();
            minCount = reader.ReadInt32();
            maxCount = reader.ReadInt32();
            var conditionIdLength = reader.ReadInt32();
            conditionIds = new int[conditionIdLength];
            for (var i = 0; i < conditionIdLength; i++)
                conditionIds[i] = reader.ReadInt32();
            var sequenceLength = reader.ReadInt32();
            sequences = new GSequence[sequenceLength];
            for (var i = 0; i < sequenceLength; i++)
            {
                sequences[i] = new GSequence();
                sequences[i].Deserialize(reader);
            }
        }
    }
}