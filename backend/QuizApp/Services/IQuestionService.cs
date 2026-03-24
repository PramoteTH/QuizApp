using QuizApp.Models;

namespace QuizApp.Services;

public interface IQuestionService
{
    Task<List<Question>> GetAllAsync();
    Task<Question> CreateAsync(Question question);
    Task<bool> DeleteAsync(int id);
}
