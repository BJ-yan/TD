using System;
using Match3.Core.Board;
using Match3.Core.Level;
using Match3.Core.Generate;

namespace Match3.Core.Elements
{
    /// <summary>
    /// 元素管理器（P-005 最小版）。
    /// 用途：负责按 CreateParam 创建元素并挂到棋盘。
    /// </summary>
    public class ElementManager
    {
        private readonly LevelBase _level;

        public ElementManager(LevelBase level)
        {
            _level = level;
        }

        /// <summary>
        /// 按照筛选出来的生成参数在指定位置生成元素
        /// </summary>
        /// <param name="position"></param>
        /// <param name="createParam"></param>
        /// <returns></returns>
        public ElementBase CreateOn(GridPos position, CreateParam createParam)
        {
            // 生成数据合法检查
            if (createParam.typeId <= 0 || createParam.typeId >= (int)ElementType.Purple)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(createParam.typeId),
                    createParam.typeId,
                    "CreateOn 需要具体基础色 typeId（1..Purple）。"
                );
            }
            
            var elementType = (ElementType)createParam.typeId;
            /// todo: 后续引入对象池
            var element = new ColorElement(elementType);
            
            if (createParam.life > 0)
                element.SetLife(createParam.life);

            if (createParam.cell != null)
                createParam.cell.SetElement(createParam.layer, element);
            
            element.CalibratePosition(position);

            return element;
        }
    }
}