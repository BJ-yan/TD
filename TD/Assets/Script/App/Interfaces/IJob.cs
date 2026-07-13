using System.Threading;
using Cysharp.Threading.Tasks;

namespace App.Interfaces
{
    public interface IJob
    {
        int ExecutionOrder { get; }

        UniTask ExecuteAsync(CancellationToken cancellationToken = default);
    }
}