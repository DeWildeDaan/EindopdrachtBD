var builder = WebApplication.CreateBuilder(args);

//Database
var dbSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(dbSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
//Repositories
builder.Services.AddTransient<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
//Services
builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
//Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//Add swagger documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapGet("/", () => "Hello World!");


//Restaurants
app.MapGet("/setup", async (IRestaurantService restaurantService) => Results.Created($"/restaurants", await restaurantService.Setup()));

app.MapGet("/restaurants", async (IRestaurantService restaurantService) => Results.Ok(await restaurantService.GetRestaurants()));

app.MapGet("/restaurant/{id}", async (IRestaurantService restaurantService, string id) => Results.Ok(await restaurantService.GetRestaurant(id)));

app.MapPost("/restaurant", async (IRestaurantService restaurantService, Restaurant restaurant) => {
    var results = await restaurantService.AddRestaurant(restaurant);
    return Results.Created($"/restaurant/{restaurant.RestaurantId}", results);
});

app.MapPut("/restaurant", async (IRestaurantService restaurantService, Restaurant restaurant) => Results.Ok(await restaurantService.UpdateRestaurant(restaurant)));

app.MapDelete("/restaurant/{id}", async (IRestaurantService restaurantService, string id) => Results.Ok(await restaurantService.DeleteRestaurant(id)));


//Reviews
app.MapGet("/reviews", async (IReviewService reviewService) => Results.Ok(await reviewService.GetReviews()));

app.MapGet("/review/{id}", async (IReviewService reviewService, string id) => Results.Ok(await reviewService.GetReview(id)));

app.MapPost("/review", async (IReviewService reviewService, Review review) => {
    var results = await reviewService.AddReview(review);
    return Results.Created($"/review/{review.ReviewId}", results);
});

app.MapPut("/review", async (IReviewService reviewService, Review review) => Results.Ok(await reviewService.UpdateReview(review)));

app.MapDelete("/review/{id}", async (IReviewService reviewService, string id) => Results.Ok(await reviewService.DeleteReview(id)));


app.Run("http://localhost:3000");
//app.Run();
//public partial class Program { }
//app.Run("http://0.0.0:3000");
