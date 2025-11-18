namespace encuesta_pro.Models;

public class AnswerDetail
{
    public int Id { get; set; }
    public int AnswerId { get; set; }
    public Answer Answer { get; set; }

    public int QuestionId { get; set; }
    public int? OptionId { get; set; }
    public string TextValue { get; set; }
}