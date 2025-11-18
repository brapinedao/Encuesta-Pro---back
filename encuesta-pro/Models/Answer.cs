namespace encuesta_pro.Models;

public class Answer
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<AnswerDetail> Details { get; set; } = new();
}