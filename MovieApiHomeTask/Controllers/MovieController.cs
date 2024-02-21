using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApiHomeTask.Dtos;
using MovieApiHomeTask.Entities;
using MovieApiHomeTask.Interfaces;

namespace MovieApiHomeTask.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost("movies")]
    public async Task<IActionResult> Create(MovieDto movieDto)
    {
        return Ok(await _movieService.Create(movieDto));//201
    }

    [HttpPost("ratings")]
    public async Task<IActionResult> AddRating(RatingDto ratingDto)
    {
        return Ok(await _movieService.AddRating(ratingDto));//201
    }

    [HttpGet("movie/{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        return Ok(await _movieService.GetMovieById(id));
    }

    [HttpGet("comments/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _movieService.GetById(id));
    }

    [HttpGet("Allcomments")]
    public async Task<IActionResult> GetRandomComments(int postId)
    {
        return Ok(await _movieService.GetRandomComments(postId));
    }

    [HttpGet("FilterByRating")]
    public async Task<IActionResult> FilterByRating(int ratingFrom, int ratingTo)
    {
        return Ok(await _movieService.FilterByRating(ratingFrom, ratingTo));
    }

    // POST: MovieController/Edit/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}

    //// GET: MovieController/Delete/5
    //public ActionResult Delete(int id)
    //{
    //    return View();
    //}

    //// POST: MovieController/Delete/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Delete(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}
}
