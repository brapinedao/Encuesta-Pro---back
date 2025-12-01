using encuesta_pro.Data;
using encuesta_pro.DTOs.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace encuesta_pro.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StatisticsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("survey/{surveyId}")]
    public async Task<ActionResult<SurveyStatisticsDto>> GetSurveyStatistics(int surveyId)
    {
        var survey = await _context.Surveys
            .Include(s => s.Questions)
            .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(s => s.Id == surveyId);

        if (survey == null)
        {
            return NotFound();
        }

        var answers = await _context.Answers
            .Include(a => a.Details)
            .Where(a => a.SurveyId == surveyId)
            .ToListAsync();

        var stats = new SurveyStatisticsDto
        {
            SurveyId = survey.Id,
            SurveyTitle = survey.Title,
            TotalResponses = answers.Count,
            Questions = new List<QuestionStatisticsDto>()
        };

        foreach (var question in survey.Questions)
        {
            var questionStats = new QuestionStatisticsDto
            {
                QuestionId = question.Id,
                QuestionText = question.Text,
                FieldType = question.FieldType,
                Data = new List<DataPointDto>()
            };

            var details = answers.SelectMany(a => a.Details)
                                 .Where(d => d.QuestionId == question.Id)
                                 .ToList();

            if (question.FieldType == "radio" || question.FieldType == "checkbox" || question.FieldType == "select")
            {
                foreach (var option in question.Options)
                {
                    var count = details.Count(d => d.OptionId == option.Id);
                    questionStats.Data.Add(new DataPointDto
                    {
                        Label = option.Text,
                        Value = count
                    });
                }
            }
            else
            {
                var count = details.Count(d => !string.IsNullOrWhiteSpace(d.TextValue));
                questionStats.Data.Add(new DataPointDto
                {
                    Label = "Respuestas",
                    Value = count
                });
            }

            stats.Questions.Add(questionStats);
        }

        return stats;
    }
}
