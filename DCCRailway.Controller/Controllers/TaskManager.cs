using System.Reflection;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Exceptions;
using DCCRailway.Controller.Tasks;
using DCCRailway.Controller.Tasks.Events;

namespace DCCRailway.Controller.Controllers;

public class TaskManager(ICommandStation commandStation, Assembly assembly) {
    private readonly Dictionary<string, IControllerTask> _runningTasks   = [];
    private          Dictionary<Type, TaskAttribute>     _availableTasks = [];

    public IControllerTask? this[string name] => _runningTasks[name] ?? null;
    public List<IControllerTask> RunningTasks => _runningTasks.Values.ToList();

    public List<TaskAttribute> Tasks {
        get {
            if (_availableTasks.Any() is false) RegisterTasks();
            return _availableTasks.Values.ToList();
        }
    }

    public event EventHandler<TaskEventArgs> TaskEvent;

    public IControllerTask? Create(string taskName) {
        if (Tasks is not { Count: > 0 }) throw new TaskException(taskName, "CommandStation has no supported Tasks");
        try {
            foreach (var task in _availableTasks) {
                if (task.Value.Name != null && task.Value.Name.Equals(taskName, StringComparison.InvariantCultureIgnoreCase)) {
                    var taskInstance = (IControllerTask?)Activator.CreateInstance(task.Key);
                    if (taskInstance != null) return taskInstance;
                }
            }
            return null;
        } catch (Exception ex) {
            throw new TaskException(taskName, "Error instantiating the Task.", ex);
        }
        throw new TaskException(taskName, "Task type specified is not supported by this command station.");
    }

    public IControllerTask? Attach(IControllerTask task) => Attach(task.Name, task, task.Frequency);

    public IControllerTask? Attach(string instanceName, string taskName, TimeSpan? frequency = null) {
        var task = Create(taskName);
        if (task != null) return Attach(instanceName, task, frequency);
        return null;
    }

    public IControllerTask? Attach(string instanceName, IControllerTask task, TimeSpan? frequency = null) {
        task.Name           = instanceName;
        task.CommandStation = commandStation;
        if (frequency != null) task.Frequency = (TimeSpan)frequency;
        var attr                              = AttributeExtractor.GetAttribute<TaskAttribute>(task.GetType());
        if (attr != null) _runningTasks.TryAdd(instanceName, task);
        return task;
    }

    private void RegisterTasks() {
        if (assembly is null) throw new ApplicationException("No Assembly has been set for the Controller Tasks Manager");
        var foundTypes = assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IControllerTask)));

        foreach (var task in foundTypes) {
            var attr = AttributeExtractor.GetAttribute<TaskAttribute>(task);
            if (attr != null && !string.IsNullOrEmpty(attr.Name)) {
                var taskInterface = task.ImplementedInterfaces.First(x => x.FullName != null && x.FullName.StartsWith("DCCRailway.Controller.Tasks.", StringComparison.InvariantCultureIgnoreCase));
                if (!_availableTasks.ContainsKey(taskInterface)) _availableTasks.TryAdd(task, attr);
            }
        }
    }

    protected void RegisterTask<T>() where T : IControllerTask {
        var attr = AttributeExtractor.GetAttribute<TaskAttribute>(typeof(T));
        if (attr is null || string.IsNullOrEmpty(attr.Name))
            throw new ApplicationException("Task instance cannot be NULL and must be a concrete object.");
        if (!_availableTasks.ContainsKey(typeof(T))) _availableTasks.TryAdd(typeof(T), attr);
    }

    protected void ClearTasks() {
        _availableTasks = [];
    }

    public void StartAllTasks() {
        foreach (var task in _runningTasks) {
            task.Value.Start();
        }
    }

    public void StopAllTasks() {
        foreach (var task in _runningTasks) {
            task.Value.Stop();
        }
    }

    #region Raise Events
    private void OnTaskAdd(object sender, IControllerTask task) {
        var e = new TaskEventArgs();
        TaskEvent(sender, e);
    }

    private void OnTaskStart(object sender, IControllerTask task) {
        var e = new TaskEventArgs();
        TaskEvent(sender, e);
    }

    private void OnTaskStop(object sender, IControllerTask task) {
        var e = new TaskEventArgs();
        TaskEvent(sender, e);
    }
    #endregion
}