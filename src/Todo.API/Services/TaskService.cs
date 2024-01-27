using Todo.API.Entities;
using Todo.API.Data;
using Todo.API.Dtos;
using Todo.API.Interfaces;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Todo.API.Services;
public class TaskService : ITaskService
{
    private readonly TodoDbContext _dbContext;

    public TaskService(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateTask(TaskDto createdTask)
    {
        if (createdTask == null)
            throw new ArgumentNullException(nameof(createdTask));

        var task = new TaskModel()
        {
            Title = createdTask.Title,
            Description = createdTask.Description,
            DueDate = createdTask.DueDate,
            Priority = createdTask.Priority,
            State = createdTask.State,
            Notes = createdTask.Notes
        };

        try
        {
            _dbContext.Tasks.Add(task);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
        catch (DbException ex)
        {
            throw new ApplicationException("Error occurred while creating the task.", ex);
        }
    }

    public async Task<IList<TaskModel>> GetTasks()
    {
        try
        {
            var tasks = await _dbContext.Tasks.ToListAsync();
            return tasks;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetTasks: {ex.Message}");
            throw; 
        }
    }

    public async Task<TaskModel> GetTaskById(int id)
    {
        try
        {
            var task = await _dbContext.Tasks
                .SingleOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return null;
            }

            return task;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> UpdateTask(int id, TaskDto updatedTask)
    {
        var existingTask = await _dbContext.Tasks.FindAsync(id);

        if (existingTask == null)
            return false; 

        existingTask.Title = updatedTask.Title ?? existingTask.Title;
        existingTask.Description = updatedTask.Description ?? existingTask.Description;
        existingTask.DueDate = updatedTask.DueDate != default ? updatedTask.DueDate : existingTask.DueDate;
        existingTask.Priority = updatedTask.Priority != default ? updatedTask.Priority : existingTask.Priority;
        existingTask.State = updatedTask.State != default ? updatedTask.State : existingTask.State;
        existingTask.Notes = updatedTask.Notes ?? existingTask.Notes;

        try
        {
            _dbContext.Tasks.Update(existingTask);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
        catch (DbException ex)
        {
            throw new ApplicationException("Error occurred while updating the task.", ex);
        }
    }

    public async Task<bool> DeleteTask(int id)
    {
        var task = await _dbContext.Tasks.FindAsync(id);

        if (task == null)
            return false; 

        try
        {
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbException ex)
        {
            throw new ApplicationException("Error occurred while deleting the task.", ex);
        }
    }
}

