namespace QuizApp.Models;

public class Question
{
    public int id { get; set; }
    public string questionText { get; set; } = string.Empty;
    public string answer1 { get; set; } = string.Empty;
    public string answer2 { get; set; } = string.Empty;
    public string answer3 { get; set; } = string.Empty;
    public string answer4 { get; set; } = string.Empty;
    public int orderNumber { get; set; }
}
