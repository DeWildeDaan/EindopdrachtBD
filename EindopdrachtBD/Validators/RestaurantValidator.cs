namespace Project.Validator;

public class RestaurantValidator : AbstractValidator<Restaurant>
{
    public RestaurantValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name can't be empty");
        RuleFor(p => p.Email).EmailAddress().WithMessage("Insert a valid e-mail");
        RuleFor(p => p.Country).NotEmpty().WithMessage("Country can't be empty");
        RuleFor(p => p.Province).NotEmpty().WithMessage("Province can't be empty");
        RuleFor(p => p.City).NotEmpty().WithMessage("City can't be empty");
        RuleFor(p => p.StreetAndNumber).NotEmpty().WithMessage("StreetAndNumber can't be empty");
        RuleFor(p => p.TotalReviews).Empty().WithMessage("TotalReviews must be empty");
        RuleFor(p => p.AverageScore).Empty().WithMessage("AverageScore must be empty");
    }
}