using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuizApp.Models;
using QuizApp.Repositories;
using QuizApp.Services;

namespace QuizApp.Tests.Services;

[TestClass]
public class QuestionServiceTests
{
    private Mock<IQuestionRepository> _repositoryMock;
    private QuestionService _service;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IQuestionRepository>();
        _service = new QuestionService(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnsQuestionsFromRepository()
    {
        List<Question> questions = new List<Question>
        {
            new Question { id = 1, questionText = "Q1", orderNumber = 1 },
            new Question { id = 2, questionText = "Q2", orderNumber = 2 }
        };
        _repositoryMock.Setup(r => r.GetAllOrderedAsync()).ReturnsAsync(questions);

        List<Question> result = await _service.GetAllAsync();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(questions, result);
    }

    [TestMethod]
    public async Task CreateAsync_AssignsNextOrderNumber()
    {
        _repositoryMock.Setup(r => r.GetMaxOrderNumberAsync()).ReturnsAsync(3);
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Question>())).Returns(Task.CompletedTask);

        Question dto = new Question
        {
            questionText = "New Question",
            answer1 = "A", answer2 = "B", answer3 = "C", answer4 = "D"
        };

        Question result = await _service.CreateAsync(dto);

        Assert.AreEqual(4, result.orderNumber);
        Assert.AreEqual("New Question", result.questionText);
    }

    [TestMethod]
    public async Task CreateAsync_WhenNoQuestionsExist_AssignsOrderNumber1()
    {
        _repositoryMock.Setup(r => r.GetMaxOrderNumberAsync()).ReturnsAsync(0);
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Question>())).Returns(Task.CompletedTask);

        Question dto = new Question { questionText = "First Question" };

        Question result = await _service.CreateAsync(dto);

        Assert.AreEqual(1, result.orderNumber);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnsFalse_WhenQuestionNotFound()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Question?)null);

        bool result = await _service.DeleteAsync(99);

        Assert.IsFalse(result);
        _repositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Question>()), Times.Never);
    }
}
