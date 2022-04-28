var builder = WebApplication.CreateBuilder(args);
//Database
var dbSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(dbSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
//Repositories
builder.Services.AddTransient<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
//Services
builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
//Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
//JWT authentication token
var authsettings = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<AuthenticationSettings>(authsettings);
builder.Services.AddAuthorization(options =>{});
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options=>
{
    options.TokenValidationParameters = new(){
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
        ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretForKey"]))
    };
}
);
//GraphQl
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();


var app = builder.Build();
//Swagger documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
//JWT authentication token
app.UseAuthentication();
app.UseAuthorization();


//Setup
app.MapGet("/setup", async (IRestaurantService restaurantService, IAuthenticationService authenticationService) => {
    await authenticationService.Setup();
    return Results.Created($"/restaurants", await restaurantService.Setup());
});


//Authentication/login
app.MapPost("/authenticate", async (IAuthenticationService authenticationservice, AuthenticationRequestBody auth) => Results.Ok(await authenticationservice.Authenticate(auth)));


//GraphQl
app.MapGraphQL();


//Restaurants
app.MapGet("/restaurants", async (IRestaurantService restaurantService) => Results.Ok(await restaurantService.GetRestaurants()));

app.MapGet("/restaurant/{id}", async (IRestaurantService restaurantService, string id) => Results.Ok(await restaurantService.GetRestaurant(id)));

app.MapPost("/restaurant", async (IRestaurantService restaurantService, Restaurant restaurant) => {
    var results = await restaurantService.AddRestaurant(restaurant);
    return Results.Created($"/restaurant/{restaurant.RestaurantId}", results);
});

app.MapPut("/restaurant", [Authorize] async (IRestaurantService restaurantService, Restaurant restaurant) => Results.Ok(await restaurantService.UpdateRestaurant(restaurant)));

app.MapDelete("/restaurant/{id}", [Authorize] async (IRestaurantService restaurantService, string id) => Results.Ok(await restaurantService.DeleteRestaurant(id)));


//Reviews
app.MapGet("/reviews", async (IReviewService reviewService) => Results.Ok(await reviewService.GetReviews()));

app.MapGet("/review/{id}", async (IReviewService reviewService, string id) => Results.Ok(await reviewService.GetReview(id)));

app.MapGet("/restaurantreview/{id}", async (IReviewService reviewService, string id) => Results.Ok(await reviewService.GetReviewsRestaurant(id)));

app.MapPost("/review", async (IReviewService reviewService, Review review) => {
    var results = await reviewService.AddReview(review);
    return Results.Created($"/review/{review.ReviewId}", results);
});

app.MapPut("/review", async (IReviewService reviewService, Review review) => Results.Ok(await reviewService.UpdateReview(review)));

app.MapDelete("/review/{id}", async (IReviewService reviewService, string id) => Results.Ok(await reviewService.DeleteReview(id)));

//Local/development
//app.Run("http://localhost:3000");

//Docker
app.Run();
public partial class Program { }


//app.Run("http://0.0.0:3000");
