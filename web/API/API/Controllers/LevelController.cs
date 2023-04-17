using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LevelController : ControllerBase
    {
        private readonly ILevel _ilevel;
        public LevelController(ILevel ilevel)
        {
            _ilevel = ilevel;
        }

        [HttpGet("{id}/get")]
        public async Task<ActionResult> get([FromRoute] string id)
        {
            try
            {
                var level = await _ilevel.GetLevel(id);
                if (level == null)
                {
                    return NotFound();
                }
                return Ok(level);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> getAll()
        {
            try
            {
                var levels = await _ilevel.GetLevels();
                if (levels == null)
                {
                    return NoContent();
                }
                return Ok(levels);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> create([FromForm] Level_DTO newLevel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var level = await _ilevel.CreateLevel(newLevel);
                    if (level.ToString() == "Level created")
                    {
                        return Ok(level);
                    }
                    return BadRequest(level);
                }
                return BadRequest("Please fill in the form correctly");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<ActionResult> update([FromRoute] string id, [FromForm] Level_DTO editLevel)
        {
            try
            {
                var level = await _ilevel.UpdateLevel(id, editLevel);
                if (level.ToString() == "Updated successfuly")
                {
                    return Ok(level);
                }
                return BadRequest(level.ToString());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> delete([FromRoute] string id)
        {
            try
            {
                var level = await _ilevel.DeleteLevel(id);
                if (level.ToString() == "Level deleted successfuly")
                {
                    return Ok(level);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
