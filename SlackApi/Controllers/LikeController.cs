using Microsoft.AspNetCore.Mvc;
using SocialTree.Services.LikeService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikeController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpPost]
    public async Task<IActionResult> AddLikeAsync(long userId, long postId)
    {
        try
        {
            var like = await _likeService.AddLikeAsync(userId, postId);
            return Ok(like);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetLikesAsync(long postId)
    {
        try
        {
            var likes = await _likeService.GetLikesAsync(postId);
            return Ok(likes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpDelete("{userId}/{postId}")]
    public async Task<IActionResult> DeleteLikeAsync(long userId, long postId)
    {
        try
        {
            await _likeService.DeleteLikeAsync(userId, postId);
            return Ok("Like deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
