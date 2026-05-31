using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace VideoGameApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VideoGameController : ControllerBase
	{
		static private List<VideoGame> videoGames = new List<VideoGame>
{
	new VideoGame
	{
		Id = 1,
		Title = "Spider-Man 2",
		Platform = "PS5",
		Developer = "Insomniac Games",
		Publisher = "Sony Interactive Entertainment"
	},
	new VideoGame
	{
		Id = 2,
		Title = "The Legend of Zelda: Breath of the Wild",
		Platform = "Nintendo Switch",
		Developer = "Nintendo EPD",
		Publisher = "Nintendo"
	},
	new VideoGame
	{
		Id = 3,
		Title = "Cyberpunk 207 7",
		Platform = "PC",
		Developer = "CD Projekt Red",
		Publisher = "CD Projekt"
	},
};

		[HttpGet]
		public ActionResult<List<VideoGame>> GetVideoGames() {
			return Ok(videoGames);
		}

		[HttpGet]
		[Route("{id}")]
		public ActionResult<VideoGame> GetVideoGameById(int id) {
			var videoGame = videoGames.FirstOrDefault(v => v.Id == id);
			if (videoGame == null) {
				return NotFound();
			}
			return Ok(videoGame);
		}

		[HttpPost]
		public ActionResult<VideoGame> AddVideoGame(VideoGame newGame)
		{
			if (newGame is null)
			{
				return BadRequest();
			}
			newGame.Id = videoGames.Max(v => v.Id) + 1;
			videoGames.Add(newGame);
			return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
		}

		[HttpPut]
		[Route("{id}")]
		public IActionResult UpdateVideoGame(int id, VideoGame updatedGame) {

			var videoGame = videoGames.FirstOrDefault(v => v.Id == id);
			if (videoGame == null || updatedGame == null)
			{
				return NotFound();
			}
			videoGame.Title = updatedGame.Title;
			videoGame.Publisher = updatedGame.Publisher;
			videoGame.Developer = updatedGame.Developer;
			videoGame.Platform = updatedGame.Platform;

			
			return NoContent();
		}


	}
}
