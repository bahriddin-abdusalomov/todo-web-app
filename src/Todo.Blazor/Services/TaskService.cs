using Microsoft.AspNetCore.Components;

using Todo.Blazor.Interfaces;
using Todo.Blazor.Models;

namespace Todo.Blazor.Services;

public class TaskService : ITaskService
{
    private readonly HttpClient httpClient;

    public TaskService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IList<TaskModel>?> GetTasks()
    {
        var tasks = await httpClient.GetFromJsonAsync<IList<TaskModel>>("api/Tasks/GetTasks");
        return tasks;
    }
        
    public async Task<TaskModel?> GetTaskById(int id)
    {
        var task = await httpClient.GetFromJsonAsync<TaskModel>($"api/Tasks/GetTaskById?id={id}");
        return task;
    }

    public async Task CreateTask(TaskModel task)
    {
        await httpClient.PostAsJsonAsync("api/Tasks/CreateTask", task);
    }

    public async Task UpdateTask(TaskModel task)
    {
        await httpClient.PutAsJsonAsync($"api/Tasks/UpdateTask/?id={task.Id}", task);
    }

    public async Task DeleteTask(int id)
    {
        await httpClient.DeleteAsync($"api/Tasks/DeleteTask?id={id}");
    }
}
