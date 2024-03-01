using SlackApi.Data.Model;
using SocialTree.Data.Dto.ResponseDto;

namespace SocialTree.Services.ConverterService
{
    public interface IConverter
    {
        public Task<PostDto> postToPostDto(Post p);
        public Task<PostDto> postToPostDto(Post p,long id);


    }
}
