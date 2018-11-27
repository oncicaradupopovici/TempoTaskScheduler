using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TempoTaskScheduler
{
    public class TaskExecutor
    {

        private readonly ConcurrentDictionary<string, SemaphoreSlim> _semaphores = new ConcurrentDictionary<string, SemaphoreSlim>();

        public Task Execute(List<RuntimeTask> taskList)
        {
            var tasks = new List<Task>();
            foreach (var runtimeTask in taskList)
            {
                tasks.Add(ExecuteTask(runtimeTask));
            }

            return Task.WhenAll(tasks);
        }


        private async Task ExecuteTask(RuntimeTask task)
        {
            await Task.WhenAll(task.Dependencies);
            var sem =_semaphores.GetOrAdd(task.Name, new SemaphoreSlim(task.MaxDegreeOfParallelism));
            await sem.WaitAsync();
            task.Task.Start();
            await task.Task;
            sem.Release();
        }


    }
}
