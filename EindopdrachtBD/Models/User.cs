namespace Project.Model;

public class User{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? NickName { get; set; }
    public string? Location { get; set; }
}