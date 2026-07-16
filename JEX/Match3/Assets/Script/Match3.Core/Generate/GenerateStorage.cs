using System;
using System.Collections.Generic;
using Match3.Core.Board;
using Match3.Core.Level;
using Match3.Core.Generate;

namespace Match3.Core.Generate
{
    ///<summary>
    /// 出生口元素仓储
    /// 用途：挂在生成格上，按Scheme列表 + 加权抽签，失败使用Default-RandomColor
    ///</summary>
    public class GenerateStorage
    {
        private readonly Cell m_GenerateCell;
        private readonly LevelBase m_Level;
        private readonly BasicElementDynamicModifier  m_Modifier;
        private readonly List<GenerateScheme> m_GenerateSchemeList = new();
        
        public GridPos Position { get; }

        public GenerateStorage(LevelBase level, Cell generateCell)
        {
            m_Level = level ?? throw new ArgumentNullException(nameof(level));
            m_GenerateCell = generateCell ?? throw new ArgumentNullException(nameof(generateCell));
            Position = new GridPos(generateCell.Pos.X, generateCell.Pos.Y - 1);
            m_Modifier = new BasicElementDynamicModifier();
        }
        
        public void AddGenerateScheme(int schemeId)
        {
            var generateScheme = m_Level.RequestGenerateSchemeForCell(schemeId);
            if (generateScheme == null) return;
            if (!generateScheme.IsActive) return;
            if (!generateScheme.IsShare)
                AddGenerateScheme(generateScheme.Clone());
            else
                AddGenerateScheme(generateScheme);
        }
        
        public void AddGenerateScheme(GenerateScheme generateScheme)
        {
            if (generateScheme == null) return;
            m_GenerateSchemeList.Add(generateScheme);
            m_GenerateSchemeList.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }
    }
}