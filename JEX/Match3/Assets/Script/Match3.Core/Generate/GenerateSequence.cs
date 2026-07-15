using System;
using System.Collections.Generic;
using Match3.Core.Generate;

namespace Match3.Core.Generate
{
    public class GenerateSequence
    {
        public class Level
        {
            public LevelRandom Random { get; }
            public Dictionary<int, int> ShuffleColorMap { get; } = new();
            
            /// <summary>typeId==0 时走随机色方案（通常挂 DefaultGenerateScheme）。</summary>
            public GenerateScheme RandomColorGenerateScheme { get; set; }

            public Level(int seed = 0)
            {
                Random = new LevelRandom(seed);
            }
        }
        
        /// <summary>
        /// 对齐 Jex Level.Random.RandRange；max 为开区间（同 System.Random.Next）。
        /// </summary>
        public class LevelRandom
        {
            private readonly Random _rng;
            public LevelRandom(int seed) => _rng = new Random(seed);
            public int RandRange(int minInclusive, int maxExclusive) =>
                _rng.Next(minInclusive, maxExclusive);
        }
    }
}