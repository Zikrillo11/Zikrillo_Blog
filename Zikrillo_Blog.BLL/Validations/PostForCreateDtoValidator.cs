using FluentValidation;
using Zikrillo_Blog.Shared.DTOs.Post;

public class PostForCreateDtoValidator : AbstractValidator<PostForCreateDto>
{
    public PostForCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title bo‘sh bo‘lmasin")
            .MinimumLength(3).WithMessage("Kamida 3 ta harf")
            .MaximumLength(100).WithMessage("100 tadan oshmasin");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content bo‘sh bo‘lmasin")
            .MinimumLength(10);

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category tanlanishi kerak");
    }
}