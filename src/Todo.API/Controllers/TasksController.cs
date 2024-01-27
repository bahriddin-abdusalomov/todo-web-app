using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Todo.API.Dtos;
using Todo.API.Entities;
using Todo.API.Interfaces;

namespace Todo.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]

public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskService taskService, ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync(TaskDto createdTask)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var success = await _taskService.CreateTask(createdTask);

                if (success)
                    return Ok("Task created successfully");
                else
                    return BadRequest("Unable to create task");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogError($"Error in CreateTask: {errors}");
                return BadRequest(errors);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in CreateTask: {ex}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTasksAsync()
    {
        try
        {
            var tasks = await _taskService.GetTasks();
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in GetTasks: {ex}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet]
    public async Task<TaskModel> GetTaskById(int id)
    {
        try
        {
            
            var task = await _taskService.GetTaskById(id);

            if (task == null)
            {
                _logger.LogWarning($"Task with id {id} not found");
                return null;
            }

            return task;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in GetTaskById: {ex}");
            throw; 
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDto updatedTask)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var success = await _taskService.UpdateTask(id, updatedTask);

                if (success)
                {

                    return Ok("Task updated successfully");
                }
                else
                    return NotFound("Task not found");
            }
            else
            {
                return BadRequest("Invalid model state");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in UpdateTask: {ex}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            bool result = await _taskService.DeleteTask(id);

            if (result)
            {
                _logger.LogInformation($"Task with id {id} has been successfully deleted.");
                return Ok("Task successfully deleted.");
            }
            else
            {
                _logger.LogWarning($"Task with id {id} not found.");
                return NotFound(); 
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting the task. {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
