using encuesta_pro.Data;
using encuesta_pro.DTOs.Survey;
using encuesta_pro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace encuesta_pro.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SurveysController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SurveysController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SurveyDto>>> GetSurveys([FromQuery] int? companyId)
    {
        var query = _context.Surveys
            .Include(s => s.Questions)
            .ThenInclude(q => q.Options)
            .AsQueryable();

        if (companyId.HasValue)
        {
            query = query.Where(s => s.CompanyId == companyId.Value);
        }

        var surveys = await query.ToListAsync();

        return surveys.Select(s => new SurveyDto
        {
            Id = s.Id,
            CompanyId = s.CompanyId,
            Title = s.Title,
            Description = s.Description,
            Questions = s.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Text = q.Text,
                FieldType = q.FieldType,
                Options = q.Options.Select(o => new QuestionOptionDto
                {
                    Id = o.Id,
                    Text = o.Text
                }).ToList()
            }).ToList()
        }).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyDto>> GetSurvey(int id)
    {
        var survey = await _context.Surveys
            .Include(s => s.Questions)
            .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (survey == null)
        {
            return NotFound();
        }

        return new SurveyDto
        {
            Id = survey.Id,
            CompanyId = survey.CompanyId,
            Title = survey.Title,
            Description = survey.Description,
            Questions = survey.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Text = q.Text,
                FieldType = q.FieldType,
                Options = q.Options.Select(o => new QuestionOptionDto
                {
                    Id = o.Id,
                    Text = o.Text
                }).ToList()
            }).ToList()
        };
    }

    [HttpPost]
    public async Task<ActionResult<SurveyDto>> CreateSurvey(CreateSurveyDto dto)
    {
        var survey = new Survey
        {
            CompanyId = dto.CompanyId,
            Title = dto.Title,
            Description = dto.Description,
            Questions = dto.Questions.Select(q => new Question
            {
                Text = q.Text,
                FieldType = q.FieldType,
                Options = q.Options.Select(o => new QuestionOption
                {
                    Text = o.Text
                }).ToList()
            }).ToList()
        };

        _context.Surveys.Add(survey);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSurvey), new { id = survey.Id }, new SurveyDto
        {
            Id = survey.Id,
            CompanyId = survey.CompanyId,
            Title = survey.Title,
            Description = survey.Description,
            Questions = survey.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Text = q.Text,
                FieldType = q.FieldType,
                Options = q.Options.Select(o => new QuestionOptionDto
                {
                    Id = o.Id,
                    Text = o.Text
                }).ToList()
            }).ToList()
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSurvey(int id, UpdateSurveyDto dto)
    {
        var survey = await _context.Surveys.FindAsync(id);

        if (survey == null)
        {
            return NotFound();
        }

        survey.Title = dto.Title;
        survey.Description = dto.Description;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSurvey(int id)
    {
        var survey = await _context.Surveys.FindAsync(id);
        if (survey == null)
        {
            return NotFound();
        }

        _context.Surveys.Remove(survey);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
