using Core.Structs;

namespace Core.Interfaces
{
    /// <summary>
    /// 这个类用于定义棋盘的行为
    /// </summary>
    public interface IGrid
    {
        int RowCount { get; }
        int ColumnCount { get; }
        
        bool IsPositionOnGrid(GridPosition gridPosition);
    }
}