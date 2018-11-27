using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TempoTaskScheduler
{
    public class TaskConfigTree
    {
        public TaskConfigTreeNode Root { get; }

        public TaskConfigTree(TaskConfigTreeNode root)
        {
            Root = root;
        }


        public List<RuntimeTask> CreateRuntimeTasks()
        {
            var accumulator = new List<RuntimeTask>();
            CreateRuntimeTasks(Root, accumulator, null);
            return accumulator;
        }


        private void CreateRuntimeTasks(TaskConfigTreeNode node, List<RuntimeTask> accumulator,  List<RuntimeTask> parentTasks)
        {
            //var tasksDictionary = new Dictionary<string, List<RuntimeTask>>(); //key is TaskConfigName

            var tasks = CreateRuntimeTaskForNode(node, parentTasks);
            accumulator.AddRange(tasks);

            foreach(var dependency in node.Children)
            {
                CreateRuntimeTasks(dependency, accumulator, tasks);
            }
        }


        private List<RuntimeTask> CreateRuntimeTaskForNode(TaskConfigTreeNode node, List<RuntimeTask> parentTasks)
        {
            var result = new List<RuntimeTask>();

            foreach (var taskConfigConnection in node.TaskConfig.Connections)
            {
                var task = new Task(() =>
                {
                    Console.WriteLine($"{DateTime.Now}: Executing task {node.TaskConfig.Name} and connection {taskConfigConnection}.");
                    Thread.Sleep(1000);
                    
                });
                var runtimeTask = new RuntimeTask(task, taskConfigConnection, node.TaskConfig.Name, node.TaskConfig.MaxDegreeOfParallelism);
                if(parentTasks!=null)
                {
                    if (node.TaskConfig.OnlyIfDependentTaskSucceeded)
                    {
                        runtimeTask.Dependencies.AddRange(parentTasks.Select(rt=> rt.Task));
                    }
                    else
                    {
                        var parentRuntimeTaskWithSameConnection = parentTasks.SingleOrDefault(rt => rt.Connection == taskConfigConnection);
                        if (parentRuntimeTaskWithSameConnection != null)
                        {
                            runtimeTask.Dependencies.Add(parentRuntimeTaskWithSameConnection.Task);
                        }
                        else
                        {
                            //TBD
                        }
                    }
                }

                result.Add(runtimeTask);
            }

            return result;
        }




        

    }
}
