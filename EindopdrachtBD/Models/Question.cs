namespace Project.Model;

public class Question
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? QuestionId { get; set; }
    public string? Text { get; set; }
}