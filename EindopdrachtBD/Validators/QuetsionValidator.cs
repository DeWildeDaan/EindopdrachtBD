namespace Project.Validator;

public class QuestionValidator : AbstractValidator<Question>
{
    public QuestionValidator()
    {
        RuleFor(p => p.QuestionId).Empty().WithMessage("QuestionId must be empty");
        RuleFor(p => p.Text).MinimumLength(1).WithMessage("QuestionId can't be empty").MaximumLength(150).WithMessage("QuestionId can't be longer than 150 characters");
    }
}