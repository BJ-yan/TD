using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Match3.Core.Board
{
    /// <summary>
    /// 棋盘格子坐标。约定：(0,0) 在左上角，X 向右增，Y 向下增。
    /// </summary>
    public readonly struct GridPos : IEquatable<GridPos>
    {
        public int X { get; }
        public int Y { get; }

        public GridPos(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public GridPos Offset(int dx, int dy) => new(X + dx, Y + dy);
        
        /// <summary>上下左右四邻域（不检查边界）。</summary>
        public IEnumerable<GridPos> GetNeighbors4()
        {
            yield return new GridPos(X, Y - 1); // 上
            yield return new GridPos(X, Y + 1); // 下
            yield return new GridPos(X - 1, Y); // 左
            yield return new GridPos(X + 1, Y); // 右
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(GridPos other) => X == other.X && Y == other.Y;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is GridPos other && Equals(other);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => $"({X},{Y})";
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(GridPos a, GridPos b) => a.Equals(b);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(GridPos a, GridPos b) => !a.Equals(b);
    }
}