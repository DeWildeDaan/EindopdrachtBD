namespace Project_Tests;
public class Helper
{

    public static WebApplicationFactory<Program> CreateApi(){
    var application = new WebApplicationFactory<Program>()
    .WithWebHostBuilder(builder =>
    {       
        builder.ConfigureServices(services =>
        {
            //FakeRestaurantRepository
            var descriptorRestaurant = services.SingleOrDefault(d => d.ServiceType == typeof(IRestaurantRepository));
            services.Remove(descriptorRestaurant);

            var fakeRestaurantRepository = new ServiceDescriptor(typeof(IRestaurantRepository), typeof(FakeRestaurantRepository), ServiceLifetime.Transient);
            services.Add(fakeRestaurantRepository);

            //FakeReviewRepository
            var descriptorReview = services.SingleOrDefault(d => d.ServiceType == typeof(IReviewRepository));
            services.Remove(descriptorRestaurant);

            var fakeReviewRepository = new ServiceDescriptor(typeof(IReviewRepository), typeof(FakeReviewRepository), ServiceLifetime.Transient);
            services.Add(fakeReviewRepository);
        });

    });

    return application;

}
}