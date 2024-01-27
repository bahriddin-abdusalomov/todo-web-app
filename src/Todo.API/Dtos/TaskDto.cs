using Todo.API.Enums;

namespace Todo.API.Dtos;

public class TaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
    public string State { get; set; }
    public string Notes { get; set; }
}

