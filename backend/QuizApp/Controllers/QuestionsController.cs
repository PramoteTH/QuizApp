using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Controllers;

[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _service;

    public QuestionsController(IQuestionService service)
    {
        _service = service;
    }

    [HttpGet("api/questions")]
    public async Task<IActionResult> GetAll()
    {
        List<Question> questions = await _service.GetAllAsync();
        return Ok(questions);
    }

    [HttpPost("api/questions")]
    public async Task<IActionResult> Create([FromBody] Question question)
    {
        Question created = await _service.CreateAsync(question);
        return CreatedAtAction(nameof(GetAll), new { id = created.id }, created);
    }

    [HttpDelete("api/questions/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
