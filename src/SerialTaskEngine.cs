
namespace demo
{

    class SerialTaskEngine
    {

        List<task> tasks = new List<task>();

        task? currentTask = null;


        public void parseRule(string rule)
        {

            var taskStrings = rule.Split("->");

            for (int i = 0; i < taskStrings.Length; i++)
            {
                tasks.Add(new task(this) { taskId = i, taskName = taskStrings[i] });
            }

        }

        public void notify(taskState state)
        {
            Console.WriteLine($"current state {state.status}");
            if (state.status == status.error)
            {
                Console.WriteLine($"this task {@state.taskId}, be {@state.status}");
            }
            else
            {
                try
                {
                    currentTask = tasks[state.taskId + 1];

                    currentTask.execute();
                }
                catch (System.Exception)
                {

                    Console.WriteLine("no any task");
                }




            }
        }

        public void fire()
        {
            if (currentTask == null)
            {
                currentTask = tasks[0];

                currentTask.execute();
            }
        }
    }

    enum status
    {
        done,
        error
    }

    class taskState
    {
        public status status { get; set; }
        public int taskId { get; set; }
    }

    class task
    {

        private SerialTaskEngine taskEngine;

        public task(SerialTaskEngine engine)
        {
            taskEngine = engine;
        }

        public int taskId { get; set; }
        public string? taskName { get; set; }

        public void execute()
        {

            try
            {
                Console.WriteLine($"task {this.taskName}, is running");
                //throw new Exception();
                taskEngine.notify(new taskState() { taskId = this.taskId, status = status.done });
            }
            catch (System.Exception)
            {
                taskEngine.notify(new taskState() { taskId = this.taskId, status = status.error });

            }


        }

    }

}