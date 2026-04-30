using FluentValidation;
using Zikrillo_Blog.Shared.DTOs.Comment;
public class CommentCreateDtoValidator : AbstractValidator<CommentForCreateDto>
{
    public CommentCreateDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MinimumLength(2);
    }
}