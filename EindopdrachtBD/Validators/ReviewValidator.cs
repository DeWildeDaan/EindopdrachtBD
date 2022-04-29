namespace Project.Validator;

public class ReviewValidator : AbstractValidator<Review>
{
    public ReviewValidator()
    {
        RuleFor(p => p.RestaurantId).NotEmpty().WithMessage("RestaurantId can't be empty");
        RuleFor(p => p.Text).NotEmpty().WithMessage("Text can't be empty");
        RuleFor(p => p.Score).GreaterThanOrEqualTo(0).WithMessage("Score must be greather than or eaqual to 0").LessThanOrEqualTo(5).WithMessage("Score must be less than or eaqual to 5");
        RuleFor(p => p.DateAndTime).Empty().WithMessage("DateAndTime must be empty");
    }
}