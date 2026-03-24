using QuizApp.Models;
using QuizApp.Repositories;

namespace QuizApp.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _repo;

    public QuestionService(IQuestionRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Question>> GetAllAsync() =>
        _repo.GetAllOrderedAsync();

    public async Task<Question> CreateAsync(Question question)
    {
        var maxOrder = await _repo.GetMaxOrderNumberAsync();
        question.orderNumber = maxOrder + 1;

        await _repo.AddAsync(question);
        await _repo.SaveChangesAsync();
        return question;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var question = await _repo.GetByIdAsync(id);
        if (question == null) return false;

        await _repo.RemoveAsync(question);
        await _repo.SaveChangesAsync();

        var remaining = await _repo.GetAllOrderedAsync();
        await _repo.ResequenceAsync(remaining);
        await _repo.SaveChangesAsync();

        return true;
    }
}
