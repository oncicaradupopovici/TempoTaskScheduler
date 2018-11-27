using System.Collections.Generic;

namespace TempoTaskScheduler
{
    public class TaskConfig
    {
        public string Name { get; }
        public int MaxDegreeOfParallelism { get; }
        public IEnumerable<string> Connections { get; }
        public string DependsOnTask { get; }
        public bool OnlyIfDependentTaskSucceeded { get; }

        public TaskConfig(string name, int maxDegreeOfParallelism, IEnumerable<string> connections, string dependsOnTask, bool onlyIfDependentTaskSucceeded)
        {
            Name = name;
            MaxDegreeOfParallelism = maxDegreeOfParallelism;
            Connections = connections;
            DependsOnTask = dependsOnTask;
            OnlyIfDependentTaskSucceeded = onlyIfDependentTaskSucceeded;
        }

    }
}
