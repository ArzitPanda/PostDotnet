using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Services.PostService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlackApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(long id)
        {
            var post = await _postService.GetPostById(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("person/{personId}")]
        public async Task<IActionResult> GetPostsOfPerson(long personId)
        {
            var posts = await _postService.GetPostsOfPerson(personId);
            return Ok(posts);
        }

        [HttpGet("person/{personId}/visibility/{visibility}")]
        public async Task<IActionResult> GetPostsByVisibilityOfPerson(long personId, string visibility)
        {
            var posts = await _postService.GetPostsByVisibilityOfPerson(personId, visibility);
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostDto postDto)
        {
            try
            {
                var post = await _postService.AddPost(postDto);
                return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the post: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(long id, UpdatePostDto postDto)
        {
            try
            {
                postDto.Id = id;
                if (!await _postService.UpdatePost(postDto))
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the post: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(long id)
        {
            try
            {
                if (!await _postService.DeletePost(id))
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the post: {ex.Message}");
            }
        }
    }
}
