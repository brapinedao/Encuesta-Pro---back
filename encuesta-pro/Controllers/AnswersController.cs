using encuesta_pro.Data;
using encuesta_pro.DTOs.Answer;
using encuesta_pro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace encuesta_pro.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AnswersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnswerDto>>> GetAnswers([FromQuery] int? surveyId)
    {
        var query = _context.Answers
            .Include(a => a.Details)
            .AsQueryable();

        if (surveyId.HasValue)
        {
            query = query.Where(a => a.SurveyId == surveyId.Value);
        }

        var answers = await query.ToListAsync();

        return answers.Select(a => new AnswerDto
        {
            Id = a.Id,
            SurveyId = a.SurveyId,
            CreatedAt = a.CreatedAt,
            Details = a.Details.Select(d => new AnswerDetailDto
            {
                Id = d.Id,
                QuestionId = d.QuestionId,
                OptionId = d.OptionId,
                TextValue = d.TextValue
            }).ToList()
        }).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnswerDto>> GetAnswer(int id)
    {
        var answer = await _context.Answers
            .Include(a => a.Details)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (answer == null)
        {
            return NotFound();
        }

        return new AnswerDto
        {
            Id = answer.Id,
            SurveyId = answer.SurveyId,
            CreatedAt = answer.CreatedAt,
            Details = answer.Details.Select(d => new AnswerDetailDto
            {
                Id = d.Id,
                QuestionId = d.QuestionId,
                OptionId = d.OptionId,
                TextValue = d.TextValue
            }).ToList()
        };
    }

    [HttpPost]
    public async Task<ActionResult<AnswerDto>> CreateAnswer(CreateAnswerDto dto)
    {
        var answer = new Answer
        {
            SurveyId = dto.SurveyId,
            CreatedAt = DateTime.UtcNow,
            Details = dto.Details.Select(d => new AnswerDetail
            {
                QuestionId = d.QuestionId,
                OptionId = d.OptionId,
                TextValue = d.TextValue
            }).ToList()
        };

        _context.Answers.Add(answer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAnswer), new { id = answer.Id }, new AnswerDto
        {
            Id = answer.Id,
            SurveyId = answer.SurveyId,
            CreatedAt = answer.CreatedAt,
            Details = answer.Details.Select(d => new AnswerDetailDto
            {
                Id = d.Id,
                QuestionId = d.QuestionId,
                OptionId = d.OptionId,
                TextValue = d.TextValue
            }).ToList()
        });
    }
}
