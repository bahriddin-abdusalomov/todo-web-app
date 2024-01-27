using Todo.API.Dtos;
using Todo.API.Entities;

namespace Todo.API.Interfaces;

public interface ITaskService
{
    Task<IList<TaskModel>> GetTasks();
    Task<TaskModel> GetTaskById(int id);
    Task<bool> CreateTask(TaskDto createdTask);
    Task<bool> UpdateTask(int id, TaskDto updatedTask);
    Task<bool> DeleteTask(int id);
}
