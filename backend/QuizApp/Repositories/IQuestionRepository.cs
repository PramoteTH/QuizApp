using QuizApp.Models;

namespace QuizApp.Repositories;

public interface IQuestionRepository
{
    Task<List<Question>> GetAllOrderedAsync();
    Task<Question?> GetByIdAsync(int id);
    Task<int> GetMaxOrderNumberAsync();
    Task AddAsync(Question question);
    Task RemoveAsync(Question question);
    Task ResequenceAsync(List<Question> questions);
    Task SaveChangesAsync();
}
