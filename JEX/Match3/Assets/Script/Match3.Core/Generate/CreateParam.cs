using System;
using System.Collections.Generic;
using Match3.Core.Board;

namespace Match3.Core.Generate
{
    /// <summary>
    /// 用来标记元素是从哪条路径生成出来的，主要是用来区分表现、统计
    /// </summary>
    public enum BirthSource
    {
        None,
        /// <summary>配置生成 + 生成口生成</summary>
        Create,
        /// <summary>匹配生成</summary>
        SwapMatch,
        /// <summary>匹配生成</summary>
        FallMatch,
        /// <summary>替换：用新元素替换旧元素</summary>
        Modify,
        /// <summary>系统派发（关前、补步等）</summary>
        Dispatch,
        /// <summary>系统派发（连胜）</summary>
        DispatchWin,
        /// <summary>结算</summary>
        Settlement,
    }
    
    /// <summary>
    /// 创建元素参数。位域布局对齐 Jex：
    /// form @0..3，life @4..10，typeId @18..31。
    /// Generate 产物常见形态：typeId &lt;&lt; 18 | life &lt;&lt; 4
    /// </summary>
    public struct CreateParam
    {
        private long m_Value;
        public int typeId;
        public int form;
        public int life;
        public Cell cell;
        public List<Cell> cells;
        public ElementLayer layer;
        public BirthSource birthType;
        public CreateParam(
            int targetTypeId,
            int lifeValue,
            List<Cell> targetCells,
            ElementLayer targetLayer = ElementLayer.Medium,
            int formValue = 0)
        {
            typeId = targetTypeId;
            form = formValue;
            life = lifeValue;
            cell = null;
            cells = targetCells;
            layer = targetLayer;
            m_Value = 0;
            birthType = BirthSource.None;
        }
        public CreateParam(
            int targetTypeId,
            int lifeValue,
            Cell targetCell,
            ElementLayer targetLayer = ElementLayer.Medium,
            int formValue = 0)
        {
            typeId = targetTypeId;
            form = formValue;
            life = lifeValue;
            cell = targetCell;
            cells = null;
            layer = targetLayer;
            m_Value = 0;
            birthType = BirthSource.None;
        }
        public CreateParam(
            int targetTypeId,
            int lifeValue,
            List<Cell> targetCells,
            ElementLayer targetLayer,
            long value,
            int formValue = 0)
        {
            typeId = targetTypeId;
            form = formValue;
            life = lifeValue;
            cell = null;
            cells = targetCells;
            layer = targetLayer;
            m_Value = value;
            birthType = BirthSource.None;
        }
        public CreateParam(
            int targetTypeId,
            int lifeValue,
            Cell targetCell,
            ElementLayer targetLayer,
            long value,
            int formValue = 0)
        {
            typeId = targetTypeId;
            form = formValue;
            life = lifeValue;
            cell = targetCell;
            cells = null;
            layer = targetLayer;
            m_Value = value;
            birthType = BirthSource.None;
        }
        /// <summary>ElementStorage 主路径：从打包 long 解出 typeId/form/life。</summary>
        public CreateParam(long value, Cell targetCell, ElementLayer targetLayer = ElementLayer.Medium)
        {
            typeId = ElementBits.GetTypeId(value);
            form = ElementBits.GetForm(value);
            life = ElementBits.GetLife(value);
            cell = targetCell;
            cells = null;
            layer = targetLayer;
            m_Value = value;
            birthType = BirthSource.None;
        }
        public void SetBirthSource(BirthSource aniType)
        {
            birthType = aniType;
        }
        /// <summary>读取自定义附加位段，区间 [start, end)。</summary>
        public int GetValue(byte start, byte end)
        {
            var rangeBitCount = end - start;
            if (start < ElementBits.DefaultBitUsed
                || rangeBitCount <= 0
                || start + rangeBitCount > ElementBits.MaxBitRange)
                throw new ArgumentOutOfRangeException();
            return (int)(m_Value >> start & (1 << rangeBitCount) - 1);
        }
        public bool GetValue(byte start) => GetValue(start, (byte)(start + 1)) == 1;
    }
    
    // <summary>
    /// Jex Elements 上的位域工具；Demo 暂放此处，后续可并入 Elements 控制类。
    /// </summary>
    public static class ElementBits
    {
        public const int DefaultBitUsed = 42;
        public const int MaxBitRange = 64;
        private const int Bit4 = (1 << 4) - 1;
        private const int Bit7 = (1 << 7) - 1;
        private const int Bit14 = (1 << 14) - 1;
        public static int GetForm(long value) => (int)(value & Bit4);
        public static int GetLife(long value) => (int)(value >> 4 & Bit7);
        public static int GetTypeId(long value) => (int)(value >> 18 & Bit14);
    }
}