using Core.Structs;

namespace Core.Interfaces
{
    /// <summary>
    /// 这个类用于定义单个格子的行为
    /// </summary>
    public interface IGridSlot
    {
        
        int ItemId { get; }

        bool HasItem { get; }
        bool IsMovable { get; }
        bool CanContainItem { get; }

        IGridSlotState State { get; }
        GridPosition GridPosition { get; }
    }
}