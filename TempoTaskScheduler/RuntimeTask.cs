using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TempoTaskScheduler
{
    public class RuntimeTask
    {
        public string Name { get; }
        public string Connection { get; }
        public int MaxDegreeOfParallelism { get; }
        public Task Task { get; }
        public List<Task> Dependencies { get; } = new List<Task>();

        public RuntimeTask(Task task, string connection, string name, int maxDegreeOfParallelism)
        {
            Task = task;
            Connection = connection;
            Name = name;
            MaxDegreeOfParallelism = maxDegreeOfParallelism;
        }
    }
}
