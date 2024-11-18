using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

// Thread pool executor with a single worker thread
public static class DefaultExecutor
{
    private static readonly TaskScheduler TaskScheduler = new LimitedConcurrencyLevelTaskScheduler(1);

    public static TaskScheduler GetScheduler() => TaskScheduler;

    // Custom TaskScheduler for single-threaded execution
    private class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        private readonly LinkedList<Task> _tasks = new LinkedList<Task>(); // Queue of tasks
        private readonly int _maxDegreeOfParallelism;
        private int _runningTasks;

        public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
        {
            _maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            lock (_tasks)
            {
                return _tasks.ToArray();
            }
        }

        protected override void QueueTask(Task task)
        {
            lock (_tasks)
            {
                _tasks.AddLast(task);
                if (_runningTasks < _maxDegreeOfParallelism)
                {
                    _runningTasks++;
                    ThreadPool.UnsafeQueueUserWorkItem(_ =>
                    {
                        TryExecuteTask(GetNextTask());
                    }, null);
                }
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (taskWasPreviouslyQueued)
            {
                return false;
            }
            return TryExecuteTask(task);
        }

        private Task GetNextTask()
        {
            lock (_tasks)
            {
                if (_tasks.Count == 0) return null;

                var task = _tasks.First.Value;
                _tasks.RemoveFirst();
                return task;
            }
        }
    }
}

// Custom attribute for ensuring the method is called after submit
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RequiresSubmitAttribute : Attribute
{
    public static void Validate(object instance)
    {
        var futureField = instance.GetType().GetField("_futureTask", BindingFlags.NonPublic | BindingFlags.Instance);
        var futureTask = futureField?.GetValue(instance) as Task;

        if (futureTask == null)
        {
            throw new InvalidOperationException("Job not submitted yet! You must call Submit() first.");
        }
    }
}

// Method dispatcher for handling methods based on argument type
public class MethodDispatcher
{
    private readonly ConcurrentDictionary<Type, Delegate> _handlers = new ConcurrentDictionary<Type, Delegate>();

    public void Register<T>(Action<object, T> handler)
    {
        _handlers[typeof(T)] = handler;
    }

    public void Dispatch(object instance, object arg)
    {
        var argType = arg.GetType();
        if (_handlers.TryGetValue(argType, out var handler))
        {
            ((Action<object, object>)handler)(instance, arg);
        }
        else
        {
            throw new InvalidOperationException($"No handler registered for type {argType.Name}");
        }
    }
}

// Utility for checking custom instructions in experiments
public static class InstructionChecker
{
    public static bool CheckCustomInstruction(
        IEnumerable<Experiment> experiments, 
        IEnumerable<string> optypes = null)
    {
        if (optypes != null)
        {
            // Optypes store class names as strings
            return optypes.Any(optype => optype.Contains("SaveData"));
        }

        // Otherwise iterate over instruction names
        return experiments.Any(exp =>
            exp.Instructions.Any(inst => inst.Name.Contains("save_")));
    }
}

public class Experiment
{
    // List of instructions in the experiment
    public List<Instruction> Instructions { get; set; }

    // Constructor to initialize the instructions list
    public Experiment()
    {
        Instructions = new List<Instruction>();
    }
}

