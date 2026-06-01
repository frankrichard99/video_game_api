using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VideoGameController(VideoGameDbContext context) : ControllerBase
	{
		private readonly VideoGameDbContext _context = context;

        [HttpGet]
		public async Task<ActionResult<List<VideoGame>>> GetVideoGames() {
			return Ok(await _context.VideoGames.ToListAsync());
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
		{
            var videoGame = await _context.VideoGames.FindAsync(id);
			if (videoGame == null)
			{
				return NotFound();
			}
			return Ok(videoGame);
		}

		[HttpPost]
		public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame newGame)
		{
			if (newGame is null)
			{
				return BadRequest();
			}

			_context.VideoGames.Add(newGame);
			await _context.SaveChangesAsync(); 

			return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> UpdateVideoGame(int id, VideoGame updatedGame)
		{

            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame == null)
            {
                return NotFound();
            }

            videoGame.Title = updatedGame.Title;
			videoGame.Publisher = updatedGame.Publisher;
			videoGame.Developer = updatedGame.Developer;
			videoGame.Platform = updatedGame.Platform;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteVideoGame(int id)
		{
            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame == null)
            {
                return NotFound();
            }
            _context.Remove(videoGame);
			await _context.SaveChangesAsync();
			return NoContent();
		}

	}
}
