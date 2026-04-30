using FluentValidation;
using Zikrillo_Blog.Shared.DTOs.Category;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryForCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
    }
}