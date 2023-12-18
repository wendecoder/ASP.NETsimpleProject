using System.Collections.Generic;
using MyWebApp.Models;

namespace MyWebApp.Services
{
    public class TaskService
{
    private static List<TaskItem> tasks = new List<TaskItem>();

    public List<TaskItem> GetTasks()
    {
        return tasks;
    }

    public TaskItem AddTask(string text)
    {
        var newTask = new TaskItem { Id = Guid.NewGuid().ToString(), Text = text };
        tasks.Add(newTask);
        return newTask;
    }
    public TaskItem GetTaskById(string id)
{
#pragma warning disable CS8603 // Possible null reference return.
            return tasks.FirstOrDefault(task => task.Id == id.ToString());
#pragma warning restore CS8603 // Possible null reference return.
        }

public void DeleteTask(string id)
{
    var taskToRemove = GetTaskById(id);
    if (taskToRemove != null)
    {
        tasks.Remove(taskToRemove);
    }
}
}

}
