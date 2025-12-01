namespace encuesta_pro.DTOs.Answer;

public class AnswerDto
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<AnswerDetailDto> Details { get; set; }
}

public class CreateAnswerDto
{
    public int SurveyId { get; set; }
    public List<CreateAnswerDetailDto> Details { get; set; }
}

public class AnswerDetailDto
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public int? OptionId { get; set; }
    public string TextValue { get; set; }
}

public class CreateAnswerDetailDto
{
    public int QuestionId { get; set; }
    public int? OptionId { get; set; }
    public string TextValue { get; set; }
}
