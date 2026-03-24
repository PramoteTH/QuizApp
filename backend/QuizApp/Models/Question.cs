namespace QuizApp.Models;

public class Question
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string Answer1 { get; set; } = string.Empty;
    public string Answer2 { get; set; } = string.Empty;
    public string Answer3 { get; set; } = string.Empty;
    public string Answer4 { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
}
