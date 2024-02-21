using MovieApiHomeTask.Dtos;
using MovieApiHomeTask.Interfaces;

namespace MovieApiHomeTask.Clients;

public class CommentClient : ICommentClient
{
    HttpClient _client;

    public CommentClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<CommentDto> GetById(int userId)
    {
        var response = await _client.GetAsync($"https://jsonplaceholder.typicode.com/comments/{userId}");

        var data = await response.Content.ReadAsAsync<CommentDto>();

        return data;
    }

    public async Task<List<CommentDto>> GetRandomComments(int postId)
    {
        var response = await _client.GetAsync($"https://jsonplaceholder.typicode.com/comments?postId={postId}");

        var data = await response.Content.ReadAsAsync<List<CommentDto>>();

        return data;
    }
}
