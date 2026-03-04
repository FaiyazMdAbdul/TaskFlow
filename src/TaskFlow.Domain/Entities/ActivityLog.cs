namespace TaskFlow.Domain.Entities;

public class ActivityLog
{
    public Guid Id { get; set; }
    public Guid TaskItemId { get; set; }
    public Guid UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public TaskItem TaskItem { get; set; } = null!;
    public User User { get; set; } = null!;
}