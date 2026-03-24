using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public QuestionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var questions = await _context.Questions
            .OrderBy(q => q.orderNumber)
            .ToListAsync();
        return Ok(questions);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Question question)
    {
        var maxOrder = await _context.Questions.AnyAsync()
            ? await _context.Questions.MaxAsync(q => q.orderNumber)
            : 0;
        question.orderNumber = maxOrder + 1;

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = question.id }, question);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null)
            return NotFound();

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();

        // Re-sequence remaining questions
        var remaining = await _context.Questions
            .OrderBy(q => q.orderNumber)
            .ToListAsync();

        for (int i = 0; i < remaining.Count; i++)
            remaining[i].orderNumber = i + 1;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
