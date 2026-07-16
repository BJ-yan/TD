using System.Collections.Generic;
using Match3.Core.Board;

namespace Match3.Core.Elements
{
    /// <summary>
    /// 棋盘元素抽象基类
    /// 处理交换后的逻辑、特殊效果结算等
    /// </summary>
    public abstract class ElementBase
    {
        private static int _sNextId = 1;
        private readonly HashSet<string> _unstableFactors = new();

        public int Id { get; } = _sNextId++;
        public int TypeId => (int)Type;
        public Cell StayedCell { get; private set; }
        public ElementState CurrentState { get; private set; } = ElementState.Idle;

        public abstract ElementType Type { get; }
        public virtual ElementKind KindId => ElementKind.Basic;
        public virtual ElementLayer Layer => ElementLayer.Medium;

        public int Life { get; protected set; } = 1;
        public virtual bool IsAlive => Life > 0;
        public bool IsDestroyed { get; private set; }

        public bool AllowDrop { get; protected set; } = true;
        public bool AllowDrag { get; protected set; } = true;
        public bool AllowMerge { get; protected set; } = true;

        public bool IsStable => _unstableFactors.Count == 0;

        public GridPos Point => StayedCell?.Pos ?? default;

        public void BindCell(Cell cell)
        {
            StayedCell = cell;
        }

        public void UnbindCell()
        {
            StayedCell = null;
        }

        public void SetState(ElementState state)
        {
            CurrentState = state;
        }
        
        /// <summary>
        /// 供 CreateParam.life
        /// </summary>
        public void SetLife(int life)
        {
            Life = life;
        }
        
        public virtual void CalibratePosition(GridPos pos)
        {
        }


        public void AddUnstableFactor(string reason)
        {
            _unstableFactors.Add(reason);
        }

        public void RemoveUnstableFactor(string reason)
        {
            _unstableFactors.Remove(reason);
        }

        /// <summary>
        /// 受击并尝试引爆，对标 Jex Element.TriggerExplode 入口（P-004 简化实现）。
        /// </summary>
        public virtual bool TriggerExplode(DamageSource source, ExplodeInfo info = default)
        {
            if (IsDestroyed || !IsAlive)
                return false;

            if (KindId == ElementKind.Barrier)
            {
                Life--;
                if (Life <= 0)
                    DestroySelf();
                return true;
            }

            DestroySelf();
            return true;
        }

        protected virtual void DestroySelf()
        {
            if (IsDestroyed)
                return;

            IsDestroyed = true;
            Life = 0;
            ClearFromCell();
        }

        private void ClearFromCell()
        {
            if (StayedCell == null)
                return;

            StayedCell.ClearElement(Layer);
            UnbindCell();
        }

        public override string ToString() => $"{GetType().Name}#{Id}({Type})";
    }
}
