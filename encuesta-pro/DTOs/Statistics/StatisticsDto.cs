namespace encuesta_pro.DTOs.Statistics;

public class SurveyStatisticsDto
{
    public int SurveyId { get; set; }
    public string SurveyTitle { get; set; }
    public int TotalResponses { get; set; }
    public List<QuestionStatisticsDto> Questions { get; set; }
}

public class QuestionStatisticsDto
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string FieldType { get; set; }
    public List<DataPointDto> Data { get; set; }
}

public class DataPointDto
{
    public string Label { get; set; }
    public int Value { get; set; }
}
