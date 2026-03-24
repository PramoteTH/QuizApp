using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Tests.Controllers;

[TestClass]
public class QuestionsControllerTests
{
    private static WebApplicationFactory<Program> _factory;
    private Mock<IQuestionService> _serviceMock;
    private HttpClient _client;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<IQuestionService>();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _serviceMock.Object);
                });
            });

        _client = _factory.CreateClient();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [TestMethod]
    public async Task GetAll_Returns200_WithQuestionList()
    {
        List<Question> questions = new List<Question>
        {
            new Question { id = 1, questionText = "Q1", orderNumber = 1 },
            new Question { id = 2, questionText = "Q2", orderNumber = 2 }
        };
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(questions);

        HttpResponseMessage response = await _client.GetAsync("/api/questions");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        List<Question>? result = await response.Content.ReadFromJsonAsync<List<Question>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public async Task Create_Returns201_WhenValid()
    {
        Question created = new Question { id = 1, questionText = "New Q", orderNumber = 1 };
        _serviceMock.Setup(s => s.CreateAsync(It.IsAny<Question>())).ReturnsAsync(created);

        Question dto = new Question
        {
            questionText = "New Q",
            answer1 = "A", answer2 = "B", answer3 = "C", answer4 = "D"
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/questions", dto);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [TestMethod]
    public async Task Delete_Returns204_WhenFound()
    {
        _serviceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

        HttpResponseMessage response = await _client.DeleteAsync("/api/questions/1");

        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [TestMethod]
    public async Task Delete_Returns404_WhenNotFound()
    {
        _serviceMock.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

        HttpResponseMessage response = await _client.DeleteAsync("/api/questions/99");

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
