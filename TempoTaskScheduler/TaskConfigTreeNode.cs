using System.Collections.Generic;

namespace TempoTaskScheduler
{
    public class TaskConfigTreeNode
    {
        public TaskConfig TaskConfig { get; }
        private readonly List<TaskConfigTreeNode> _children = new List<TaskConfigTreeNode>();
        public IEnumerable<TaskConfigTreeNode> Children => _children;

        public TaskConfigTreeNode(TaskConfig taskConfig)
        {
            TaskConfig = taskConfig;
        }

        public TaskConfigTreeNode AddChildNode(TaskConfigTreeNode child)
        {
            _children.Add(child);
            return this;
        }

        


    }
}
