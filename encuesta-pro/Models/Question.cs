namespace encuesta_pro.Models;

public class Question
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }

    public string Text { get; set; }
    public string FieldType { get; set; } // text, textarea, radio, checkbox
    public bool Required { get; set; }
    public int Order { get; set; }

    public List<QuestionOption> Options { get; set; } = new();
}