using System.Collections.Generic;
using Match3.Core.Generate;

namespace Match3.Core.Level
{
    /// <summary>
    /// 最小门面
    ///</summary>
    public class LevelBase
    {
        /// <summary>
        /// 关卡随机数。
        /// 用途：GenerateSequence / DefaultGenerateScheme 加权抽签必须走这里，保证可复现（固定 seed 可测）。
        /// </summary>
        public LevelRandom Random { get; }
        /// <summary>
        /// 颜色洗牌映射：配置 typeId → 实际 typeId。
        /// 用途：Sequence / DefaultScheme 抽色前做一次替换（活动换色、打乱等）；无映射则原样。
        /// </summary>
        public Dictionary<int, int> ShuffleColorMap { get; } = new();
        /// <summary>
        /// 棋盘引用。
        /// 用途：GetElementCount（Min/Max 门控）、GetReckonElement（邻域采样读 Resident）。
        /// </summary>
        public Board.Board Board { get; }
        public Elements.ElementManager ElementManager { get; }
        /// <summary>
        /// 是否开启动态流畅度（DyFt）。
        /// 用途：false 时跳过 DyFt 模块调用，且 UpdateModifier 里不乘 K/M/L 邻域系数（与 Jex 分支一致）。
        /// P-005 默认 false 即可。
        /// </summary>
        public bool IsDyFtOn { get; set; }
        public RoundStub Round { get; } = new();
        public DyFluencyTuneStub DyFluencyTune { get; } = new();
        public GenerateScheme RandomColorGenerateScheme { get; set; }
        public DefaultGenerateScheme DefaultGenerateScheme { get; set; }
        private readonly Dictionary<int, GenerateScheme> _schemesById = new();
        
        /// <summary>
        /// 构造。
        /// 用途：创建可复现的 LevelRandom；Board/Elements 可稍后注入。
        /// </summary>
        public LevelBase(int seed = 0)
        {
            Random = new LevelRandom(seed);
        }

        public void RegisterGenerateScheme(GenerateScheme scheme)
        {
            if (scheme == null) return;
            _schemesById[scheme.Id] = scheme;
        }

        public GenerateScheme RequestGenerateSchemeForCell(int schemeId)
        {
            return _schemesById.TryGetValue(schemeId, out var scheme) ? scheme : null;
        }
    }
}