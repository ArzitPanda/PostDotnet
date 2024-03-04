using Microsoft.AspNetCore.Mvc;
using SlackApi.Services.FeedService;

namespace SlackApi.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class FeedController :ControllerBase
    {
        private readonly IFeedService _feedService;
        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;


        }


        [HttpGet]
        public async Task<IActionResult> Get(long id,string type)
        {

            var posts =await  _feedService.GetFeedByIdAndType(id, type);

            return Ok(posts);


        }

        [HttpGet("suggestion")]
        public async Task<IActionResult> Get(long id)
        {
            var posts = await _feedService.GetFeedSuggestionById(id);
            return Ok(posts);
        }




    }
}
