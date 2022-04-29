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
//Validation
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RestaurantValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReviewValidator>());
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

app.MapGet("/restaurant/{id}", async (IRestaurantService restaurantService, string id) => {
    var results = await restaurantService.GetRestaurant(id);
    if(results is null){
        return Results.NoContent();
    } else {
        return Results.Ok(results);
    }
});

app.MapPost("/restaurant", async (IValidator<Restaurant> validator, IRestaurantService restaurantService, Restaurant restaurant) => {
    var validatorResult = validator.Validate(restaurant);
    if(validatorResult.IsValid){
        var results = await restaurantService.AddRestaurant(restaurant);
        return Results.Created($"/restaurant/{restaurant.RestaurantId}", results);
    }else {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
});

app.MapPut("/restaurant", [Authorize] async (IValidator<Restaurant> validator,IRestaurantService restaurantService, Restaurant restaurant) => {
    var validatorResult = validator.Validate(restaurant);
    if(validatorResult.IsValid){
        var results = await restaurantService.UpdateRestaurant(restaurant);
        return Results.Created($"/restaurant/{restaurant.RestaurantId}", results);
    }else {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
});

app.MapDelete("/restaurant/{id}", [Authorize] async (IRestaurantService restaurantService, string id) => Results.Ok(await restaurantService.DeleteRestaurant(id)));


//Reviews
app.MapGet("/reviews", async (IReviewService reviewService) => Results.Ok(await reviewService.GetReviews()));

app.MapGet("/review/{id}", async (IReviewService reviewService, string id) => {
    var results = await reviewService.GetReview(id);
    if(results is null){
        return Results.NoContent();
    } else {
        return Results.Ok(results);
    }
});

app.MapGet("/restaurantreview/{id}", async (IReviewService reviewService, string id) => {
    var results = await reviewService.GetReviewsRestaurant(id);
    if(results.Count() == 0){
        return Results.NoContent();
    } else {
        return Results.Ok(results);
    }
});

app.MapPost("/review", async (IValidator<Review> validator, IReviewService reviewService, Review review) => {
    var validatorResult = validator.Validate(review);
    if(validatorResult.IsValid){
        var results = await reviewService.AddReview(review);
        return Results.Created($"/review/{review.ReviewId}", results);
    }else {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
});

app.MapPut("/review", async (IValidator<Review> validator, IReviewService reviewService, Review review) => {
    var validatorResult = validator.Validate(review);
    if(validatorResult.IsValid){
        var results = await reviewService.UpdateReview(review);
        return Results.Created($"/review/{review.ReviewId}", results);
    }else {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
});

app.MapDelete("/review/{id}", async (IReviewService reviewService, string id) => Results.Ok(await reviewService.DeleteReview(id)));

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerFeature>()
        ?.Error;
    if (exception is not null)
    {
        var response = new { error = exception.Message };
        context.Response.StatusCode = 400;

        await context.Response.WriteAsJsonAsync(response);
    }
}));


//Local development
//app.Run("http://localhost:3000");
//Docker
//app.Run("http://0.0.0.0:3000");
//Tests
app.Run(); 
public partial class Program { }