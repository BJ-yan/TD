using System;
using Core.Interfaces;
using App.Interfaces;

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
        // 使用到的游戏功能
        private readonly GameBoard<TGridSlot> _gameBoard;
        private readonly IGameBoardSolver<TGridSlot> _gameBoardSolver;
        private readonly ILevelGoalsProvider<TGridSlot> _levelGoalsProvider;
        private readonly IGameBoardDataProvider<TGridSlot> _gameBoardDataProvider;
        private readonly ISolvedSequencesConsumer<TGridSlot>[] _solvedSequencesConsumers;

        // 游戏内属性
        private bool _isStarted;
        private int _achievedGoals;
        private LevelGoal<TGridSlot> _levelGoals;
        protected IGameBoard<TGridSlot> GameBoard => _gameBoard;
        public event EventHandler Finished;
        public event EventHandler<LevelGoal<TGridSlot>> LevelGoalAchieved;

		protected BaseGame(GameConfig<TGridSlot> config)
		{
			_gameBoard = new GameBoard<TGridSlot>();

 			_gameBoardSolver = config.GameBoardSolver;
            _levelGoalsProvider = config.LevelGoalsProvider;
            _gameBoardDataProvider = config.GameBoardDataProvider;
            _solvedSequencesConsumers = config.SolvedSequencesConsumers;
		}

        public void InitGameLevel(int level)
        {
            if (_isStarted)
            {
                throw new InvalidOperationException("Can not be initialized while the current game is active.");
            }
            
            _gameBoard.SetGridSlots(_gameBoardDataProvider.GetGameBoardSlots(level));
            _levelGoals = _levelGoalsProvider.GetLevelGoals(level, _gameBoard);
        }
        
        public void Dispose()
        {
            // _gameBoard?.Dispose();
        }
        
    }
}