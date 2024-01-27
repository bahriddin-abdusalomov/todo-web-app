namespace Todo.Blazor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

using Todo.Blazor.Models;

public interface ITaskService
{
    Task<IList<TaskModel>> GetTasks();
    Task<TaskModel> GetTaskById(int id);
    Task CreateTask(TaskModel task);
    Task UpdateTask(TaskModel task);
    Task DeleteTask(int id);
}