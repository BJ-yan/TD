using System;
using System.Collections.Generic;
using Match3.Core.Elements;

namespace Match3.Core.Board
{
    /// <summary>
    /// 棋盘空间模型
    /// 功能：维护棋盘边界、棋盘内格子，交换格子
    /// </summary>
    public class Board
    {
        private readonly Cell[,] _cells;
        private int _nextCellId = 1;

        public int Rows { get; }
        public int Cols { get; }

        public Board(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentOutOfRangeException(nameof(rows), "Rows and cols must be positive.");

            Rows = rows;
            Cols = cols;
            _cells = new Cell[rows, cols];

            for (var y = 0; y < rows; y++)
            for (var x = 0; x < cols; x++)
            {
                var cell = new Cell(_nextCellId++, new GridPos(x, y));
                cell.SetOwner(this);
                _cells[y, x] = cell;
            }
        }

        public bool InBounds(GridPos pos) =>
            pos.X >= 0 && pos.X < Cols && pos.Y >= 0 && pos.Y < Rows;

        public Cell GetCell(GridPos pos) =>
            InBounds(pos) ? _cells[pos.Y, pos.X] : null;

        public Cell GetCell(int x, int y) => GetCell(new GridPos(x, y));

        public void ForEachCell(Action<Cell> action)
        {
            for (var y = 0; y < Rows; y++)
            for (var x = 0; x < Cols; x++)
                action(_cells[y, x]);
        }

        public bool AreAdjacent(GridPos a, GridPos b)
        {
            var dx = Math.Abs(a.X - b.X);
            var dy = Math.Abs(a.Y - b.Y);
            return (dx + dy) == 1;
        }

        /// <summary>交换两格 Medium 层（Resident）元素。</summary>
        public void Swap(GridPos a, GridPos b)
        {
            if (!AreAdjacent(a, b))
                throw new InvalidOperationException($"Cells are not adjacent: {a}, {b}");

            var cellA = GetCell(a);
            var cellB = GetCell(b);
            if (cellA == null || cellB == null)
                throw new InvalidOperationException("Swap out of bounds.");

            var temp = cellA.Resident;
            cellA.Resident = cellB.Resident;
            cellB.Resident = temp;
        }

        public void SetCellType(GridPos pos, CellType cellType)
        {
            var cell = GetCell(pos);
            if (cell != null)
                cell.CellType = cellType;
        }

        public IEnumerable<Cell> GetNeighbors4(GridPos pos)
        {
            foreach (var neighbor in pos.GetNeighbors4())
            {
                var cell = GetCell(neighbor);
                if (cell != null)
                    yield return cell;
            }
        }
    }
}
