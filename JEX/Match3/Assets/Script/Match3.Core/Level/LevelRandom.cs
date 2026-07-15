using System;

namespace Match3.Core.Level
{
    public class LevelRandom
    {
        private readonly Random _rng;
        public LevelRandom(int seed) => _rng = new Random(seed);
        public int RandRange(int minInclusive, int maxExclusive) =>
            _rng.Next(minInclusive, maxExclusive);
    }
}