using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TempoTaskScheduler
{
    public class Test
    {

        public async Task Run()
        {
            var t1 = new TaskConfigTreeNode(new TaskConfig("T1", 2, new List<string>{"C1", "C2", "C3"}, null, false ));
            var t2 = new TaskConfigTreeNode(new TaskConfig("T2", 2, new List<string>{"C1", "C2", "C3"}, "T1", false ));
            var t3 = new TaskConfigTreeNode(new TaskConfig("T3", 2, new List<string>{"C1", "C2", "C4"}, "T1", true ));
            var t4 = new TaskConfigTreeNode(new TaskConfig("T4", 2, new List<string>{"C2", "C4", "C5"}, "T2", false ));

            t1
                .AddChildNode(t2)
                .AddChildNode(t3);

            t2.AddChildNode(t4);

            var tree = new TaskConfigTree(t1);
            var taskList = tree.CreateRuntimeTasks();
            var taskExecutor = new TaskExecutor();
            await taskExecutor.Execute(taskList);
        }
    }
}
