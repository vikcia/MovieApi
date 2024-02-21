using MovieApiHomeTask.Dtos;

namespace MovieApiHomeTask.Interfaces;

public interface ICommentClient
{
    Task<CommentDto> GetById(int userId);
    Task<List<CommentDto>> GetRandomComments(int postId);
}