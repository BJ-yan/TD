using System;
using System.Runtime.CompilerServices;

namespace Core.Structs
{
    /// <summary>
    /// 这个类用于实现坐标系统
    /// </summary>
    public readonly struct GridPosition : IEquatable <GridPosition>
    {
        public int RowIndex { get; }

        public int ColumnIndex { get; }

        public GridPosition(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }

        // 方向静态定义
        public static GridPosition Up { get; } = new GridPosition(-1, 0);

        public static GridPosition Down { get; } = new GridPosition(1, 0);

        public static GridPosition Left { get; } = new GridPosition(0, -1);

        public static GridPosition Right { get; } = new GridPosition(0, 1);

        public static GridPosition Zero { get; } = new GridPosition(0, 0);

        // 重载运算符
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GridPosition operator +(GridPosition a, GridPosition b)
        {
            return new GridPosition(a.RowIndex + b.RowIndex, a.ColumnIndex + b.ColumnIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GridPosition operator -(GridPosition a, GridPosition b)
        {
            return new GridPosition(a.RowIndex - b.RowIndex, a.ColumnIndex - b.ColumnIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(GridPosition a, GridPosition b)
        {
            return a.RowIndex == b.RowIndex && a.ColumnIndex == b.ColumnIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(GridPosition a, GridPosition b)
        {
            return a.RowIndex != b.RowIndex || a.ColumnIndex != b.ColumnIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(GridPosition other)
        {
            return RowIndex == other.RowIndex && ColumnIndex == other.ColumnIndex;
        }

        public override bool Equals(object obj)
        {
            return obj is GridPosition other && Equals(other);
        }
    }
}