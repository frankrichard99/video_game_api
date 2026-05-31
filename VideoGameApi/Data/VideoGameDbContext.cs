using Microsoft.EntityFrameworkCore;

namespace VideoGameApi.Data
{
    public class VideoGameDbContext(DbConextOptions<VideoGameDbContext> options) : DbContext(options)
    {

    }
}
