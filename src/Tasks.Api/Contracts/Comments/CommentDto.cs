using Domain;

namespace Tasks.Api.Contracts;

public record CommentDto(
    Guid Id,
    string Text,
    DateTime CreatedAtUtc,
    UserProfileDto CreatedBy)
{
    public CommentDto(Comment comment)
        : this(
            comment.Id,
            comment.Content,
            comment.CreatedAtUtc,
            new UserProfileDto(comment.User)) { }
}