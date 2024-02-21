namespace MovieApiHomeTask.Dtos;

public class FilterRatingDto
{
    public int MovieId { get; set; }
    public List<RatingDto> Rating { get; set; }
}
