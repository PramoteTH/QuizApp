using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Question>> GetAllOrderedAsync() =>
        _context.Questions.OrderBy(q => q.orderNumber).ToListAsync();

    public Task<Question?> GetByIdAsync(int id) =>
        _context.Questions.FindAsync(id).AsTask();

    public async Task<int> GetMaxOrderNumberAsync() =>
        await _context.Questions.AnyAsync()
            ? await _context.Questions.MaxAsync(q => q.orderNumber)
            : 0;

    public Task AddAsync(Question question)
    {
        _context.Questions.Add(question);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Question question)
    {
        _context.Questions.Remove(question);
        return Task.CompletedTask;
    }

    public Task ResequenceAsync(List<Question> questions)
    {
        for (int i = 0; i < questions.Count; i++)
            questions[i].orderNumber = i + 1;
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
