namespace Project.GraphQL;
public record UpdateRestaurantInput(string name, string email, string country, string province, string city, string streetandnumber, int totalreviews, double averagescore);