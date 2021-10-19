using FluentValidation;
using WordCount.Core.Resources;

namespace WordCountSolution.Validations
{
    public class SaveTextResourceValidator : AbstractValidator<SaveTextResource>
    {
        public SaveTextResourceValidator()
        {
            RuleFor(a => a.TextString)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}