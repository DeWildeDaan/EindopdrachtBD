var builder = WebApplication.CreateBuilder(args);

//Database
var dbSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(dbSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
//Repositories
builder.Services.AddTransient<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
//Services


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
