using Match3.Core.Elements;

namespace Match3.Core.Board
{
    /// <summary>
    /// 棋盘单格，对标 Jex 五层 Cell（Hide / Bottom / Medium / Cover / Top）。
    /// Medium 层在 Jex 中命名为 Resident。
    /// </summary>
    public class Cell
    {
        private Element _hide;
        private Element _bottom;
        private Element _resident;
        private Element _cover;
        private Element _top;
        private Element _surface;
        private bool _surfaceDirty = true;
        private int _surfaceLayer = -1;

        public int Id { get; }
        public GridPos Pos { get; }
        public CellType CellType { get; set; } = CellType.Normal;

        public Cell(int id, GridPos pos)
        {
            Id = id;
            Pos = pos;
        }

        public void SetOwner(Board board)
        {
            Board = board;
        }

        public Board Board { get; private set; }

        public Element Hide
        {
            get => _hide;
            set => SetLayer(ref _hide, value, ElementLayer.Hide);
        }

        public Element Bottom
        {
            get => _bottom;
            set => SetLayer(ref _bottom, value, ElementLayer.Bottom);
        }

        /// <summary>主交互层，对标 Jex Resident。</summary>
        public Element Resident
        {
            get => _resident;
            set => SetLayer(ref _resident, value, ElementLayer.Medium);
        }

        public Element Cover
        {
            get => _cover;
            set => SetLayer(ref _cover, value, ElementLayer.Cover);
        }

        public Element Top
        {
            get => _top;
            set => SetLayer(ref _top, value, ElementLayer.Top);
        }

        public Element Surface
        {
            get
            {
                if (!_surfaceDirty)
                    return _surface;

                if (_top != null)
                {
                    _surface = _top;
                    _surfaceLayer = (int)ElementLayer.Top;
                }
                else if (_cover != null)
                {
                    _surface = _cover;
                    _surfaceLayer = (int)ElementLayer.Cover;
                }
                else if (_resident != null)
                {
                    _surface = _resident;
                    _surfaceLayer = (int)ElementLayer.Medium;
                }
                else if (_bottom != null)
                {
                    _surface = _bottom;
                    _surfaceLayer = (int)ElementLayer.Bottom;
                }
                else
                {
                    _surface = _hide;
                    _surfaceLayer = (int)ElementLayer.Hide;
                }

                _surfaceDirty = false;
                return _surface;
            }
        }

        public bool IsPlayable => CellType == CellType.Normal || CellType == CellType.Generate;

        public bool IsEmpty => Resident == null && Cover == null && Top == null;

        public Element QueryElement(ElementLayer layer) => QueryElement((int)layer);

        public Element QueryElement(int layerIndex)
        {
            return layerIndex switch
            {
                (int)ElementLayer.Hide => _hide,
                (int)ElementLayer.Bottom => _bottom,
                (int)ElementLayer.Medium => _resident,
                (int)ElementLayer.Cover => _cover,
                (int)ElementLayer.Top => _top,
                _ => null
            };
        }

        public void SetElement(ElementLayer layer, Element element)
        {
            switch (layer)
            {
                case ElementLayer.Hide:
                    Hide = element;
                    break;
                case ElementLayer.Bottom:
                    Bottom = element;
                    break;
                case ElementLayer.Medium:
                    Resident = element;
                    break;
                case ElementLayer.Cover:
                    Cover = element;
                    break;
                case ElementLayer.Top:
                    Top = element;
                    break;
            }

            element?.BindCell(this);
        }

        public void ClearElement(ElementLayer layer)
        {
            SetElement(layer, null);
        }

        /// <summary>
        /// 统一伤害入口：从 Surface 层向下穿透，对标 Jex Cell.Hit。
        /// </summary>
        public bool Hit(DamageSource source, ExplodeInfo info = default)
        {
            if (!IsPlayable || Surface == null)
                return false;

            source.Point = Pos;
            var tickLayerIndex = _surfaceLayer;
            var successHit = false;

            while (tickLayerIndex >= 0)
            {
                var element = QueryElement(tickLayerIndex);
                tickLayerIndex--;

                if (element == null || !element.IsAlive)
                    continue;

                var hit = element.TriggerExplode(source, info);
                successHit = hit || successHit;

                // 障碍层阻挡伤害继续向下（对标 Jex Barrier 截断）
                if (element.KindId == ElementKind.Barrier && element.IsAlive)
                    break;
            }

            return successHit;
        }

        private void SetLayer(ref Element field, Element value, ElementLayer layer)
        {
            if (field == value)
                return;

            field?.UnbindCell();
            field = value;
            value?.BindCell(this);
            _surfaceDirty |= _surfaceLayer <= (int)layer;
        }
    }
}
