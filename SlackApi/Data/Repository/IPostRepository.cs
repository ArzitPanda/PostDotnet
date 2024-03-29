﻿using SlackApi.Data.Model;

namespace SlackApi.Data.Repository
{
    public interface IPostRepository :IGenericRepository<Post>
    {
        public Task<IEnumerable<Post>> GetFeedById(long id);


    }
}
