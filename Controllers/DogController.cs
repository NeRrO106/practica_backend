using dog.Models;
using dog.Services;
using Microsoft.AspNetCore.Mvc;

namespace dog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly DogService _dogService;

        public DogController(DogService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet]
        public ActionResult<List<Dog>> GetAll()
        {
            return _dogService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Dog> GetById(int id)
        {
            var dog = _dogService.GetById(id);
            if (dog == null)
            {
                return NotFound();
            }
            return dog;
        }

        [HttpGet("random")]
        public ActionResult<Dog> GetRandom()
        {
            var dog = _dogService.GetRandom();
            if (dog == null)
            {
                return NotFound("No dogs available.");
            }
            return dog;
        }

        [HttpGet("search")]
        public ActionResult<List<Dog>> FindByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
            {
                return BadRequest("Name parameter is required and must be at least 2 characters.");
            }

            var dogs = _dogService.FindByName(name);
            return dogs;
        }

        [HttpPost]
        public ActionResult<Dog> Add([FromBody] Dog dog)
        {
            if (dog == null)
            {
                return BadRequest("Dog data is required.");
            }
            if (dog.Age < 0 || dog.Age > 16 || dog.Weight < 0 || dog.Weight > 100)
            {
                return BadRequest("Invalid age or weight.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dogService.Add(dog);
            return CreatedAtAction(nameof(GetById), new { id = dog.Id }, dog);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Dog updatedDog)
        {
            if (updatedDog == null || id != updatedDog.Id)
            {
                return BadRequest("Invalid dog data or ID mismatch.");
            }

            if (updatedDog.Age < 0 || updatedDog.Weight < 0)
            {
                return BadRequest("Invalid.");
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_dogService.Update(id, updatedDog))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_dogService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
