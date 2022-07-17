using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecommendationSystem.API.Utils
{
    public static class RecurrentTask
    {
        public static void Run(Action action, TimeSpan timeSpan, CancellationToken cancellationToken)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));

            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    action();
                    await Task.Delay(timeSpan, cancellationToken);
                }
            }, cancellationToken);
        }
    }
}
