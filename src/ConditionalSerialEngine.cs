class ConditionalSerialEngine
{
    Dictionary<string, int> context = new Dictionary<string, int>();

    Task? currentTask;

    List<Task> tasks = new List<Task>();



    public void SetContextItem(string key, int value)
    {
        context.Add(key, value);
    }

    public Dictionary<string, int> Context
    {

        get
        {
            return context;
        }
    }


    public void ParseRule(string ruleStr)
    {
        var rules = ruleStr.Split("->");

        for (int i = 0; i < rules.Length; i++)
        {

            var items = rules[i].Split(".");

            var condition = new Condition(int.Parse(items[1]));


            tasks.Add(new Task(condition, this) { TaskId = i, TaskName = items[0] });

        }

    }

    public void NotifyTaskExecutionStatus(TaskExecutionStatus status)
    {

        try
        {
            currentTask = tasks[status.taskId + 1];

            currentTask.Execute();
        }
        catch (System.Exception)
        {

            Console.WriteLine("no any task");
        }


    }


    public void Fire()
    {
        if (currentTask == null)
        {
            currentTask = tasks[0];
        }

        currentTask?.Execute();

    }

}

enum Status
{
    SUCCESS,
    FAILED
}

class TaskExecutionStatus
{
    public Status status { get; set; }
    public int taskId { get; set; }

}

class Task
{


    ConditionalSerialEngine engine;
    Condition condition;

    public Task(Condition condition, ConditionalSerialEngine engine)
    {
        this.condition = condition;

        this.engine = engine;
    }

    public int TaskId { get; set; }
    public string? TaskName { get; set; }

    public void Execute()
    {

        if (condition.Calculate(engine.Context.GetValueOrDefault("conditionVar")))
        {

            Console.WriteLine($"task {this.TaskName} is running");

            engine.NotifyTaskExecutionStatus(new TaskExecutionStatus() { taskId = this.TaskId, status = Status.SUCCESS });
        }
        else
        {
            Console.WriteLine($"task {this.TaskName}, been skipped");

            engine.NotifyTaskExecutionStatus(new TaskExecutionStatus() { taskId = this.TaskId, status = Status.FAILED });
        }




    }

}

class Condition
{

    int param2;

    public Condition(int taskEnvParam)
    {
        param2 = taskEnvParam;
    }

    public bool Calculate(int param1)
    {
        var result = param1 + this.param2;

        if (result > 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}