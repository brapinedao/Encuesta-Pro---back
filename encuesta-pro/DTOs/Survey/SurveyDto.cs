using encuesta_pro.Models;

namespace encuesta_pro.DTOs.Survey;

public class SurveyDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<QuestionDto> Questions { get; set; }
}

public class CreateSurveyDto
{
    public int CompanyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<CreateQuestionDto> Questions { get; set; }
}

public class UpdateSurveyDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<UpdateQuestionDto> Questions { get; set; }
}

public class QuestionDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string FieldType { get; set; }
    public List<QuestionOptionDto> Options { get; set; }
}

public class CreateQuestionDto
{
    public string Text { get; set; }
    public string FieldType { get; set; }
    public List<CreateQuestionOptionDto> Options { get; set; }
}

public class UpdateQuestionDto
{
    public int? Id { get; set; }
    public string Text { get; set; }
    public string FieldType { get; set; }
    public List<UpdateQuestionOptionDto> Options { get; set; }
}

public class QuestionOptionDto
{
    public int Id { get; set; }
    public string Text { get; set; }
}

public class CreateQuestionOptionDto
{
    public string Text { get; set; }
}

public class UpdateQuestionOptionDto
{
    public int? Id { get; set; }
    public string Text { get; set; }
}
