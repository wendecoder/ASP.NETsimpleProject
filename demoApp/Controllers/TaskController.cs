using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Services;

namespace MyWebApp.Controllers
{
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _taskService.GetTasks();
            return Ok(tasks);
        }

        [HttpPost]
public IActionResult AddTask([FromBody] string task)
{
    Console.WriteLine("AddTask method reached!");
    if (task == null || string.IsNullOrWhiteSpace(task))
    {
        return BadRequest("Task text cannot be empty");
    }

    // Call AddTask without trying to assign the result to a variable
    var responseItem = _taskService.AddTask(task);

    // Respond with OK status (200) and a message
    return Ok(new { text = responseItem.Text,
    id = responseItem.Id });
}
[HttpDelete("{id}")]
public IActionResult DeleteTask(string id)
{
    var taskToDelete = _taskService.GetTaskById(id);

    if (taskToDelete == null)
    {
        return NotFound("Task not found");
    }

    _taskService.DeleteTask(id);

    return Ok(new { Message = "Task deleted successfully" });
}
    }
    
}
