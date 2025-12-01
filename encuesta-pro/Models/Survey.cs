namespace encuesta_pro.Models;

public class Survey
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Question> Questions { get; set; } = new();
}