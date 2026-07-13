using System;
using Core.Interfaces;

namespace App.Internal
{
    /// <summary>
    /// BaseGame 是 Match3.App 层的抽象基类，采用模板方法模式（Template Method Pattern）。
    /// 它的核心职责是：
    /// 定义三消游戏的通用流程骨架（初始化关卡、启动/停止游戏、目标检测、事件通知）
    /// 通过依赖注入管理多个组件（棋盘数据、求解器、目标提供者、消除消费者）
    /// 提供受保护方法供子类（Match3Game）调用，实现代码复用
    /// </summary>
    public abstract class BaseGame<TGridSlot> : IDisposable where TGridSlot :IGridSlot
    {
        // private readonly GameBoard<TGridSlot> _gameBoard;

        public void Dispose()
        {
            // _gameBoard?.Dispose();
        }
        
    }
}