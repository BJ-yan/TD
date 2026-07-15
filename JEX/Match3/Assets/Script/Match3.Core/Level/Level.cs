using System.Collections.Generic;
using Match3.Core.Generate;

namespace Match3.Core.Level
{
    /// <summary>P-005 最小门面；后续再补 Board / Elements / DyFt 等。</summary>
    public class LevelBase
    {
        public LevelRandom Random { get; }
        public Dictionary<int, int> ShuffleColorMap { get; } = new();
        /// <summary>typeId==0 时走随机色方案（通常挂 DefaultGenerateScheme）。</summary>
        public GenerateScheme RandomColorGenerateScheme { get; set; }
        public LevelBase(int seed = 0)
        {
            Random = new LevelRandom(seed);
        }
    }
}