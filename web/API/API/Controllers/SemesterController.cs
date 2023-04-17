using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SemesterController : ControllerBase
    {
        private readonly ISemester _isemester;
        public SemesterController(ISemester isemester)
        {
            _isemester = isemester;
        }

        [HttpGet("{id}/get")]
        public async Task<ActionResult> get([FromRoute] string id)
        {
            try
            {
                var semester = await _isemester.GetSemester(id);
                if (semester == null)
                {
                    return NotFound();
                }
                return Ok(semester);
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
                var semesters = await _isemester.GetSemesters();
                if (semesters == null)
                {
                    return NoContent();
                }
                return Ok(semesters);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> create([FromForm] Semester_DTO newSemester)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var semester = await _isemester.CreateSemester(newSemester);
                    if (semester.ToString() == "New semester created")
                    {
                        return Ok(semester);
                    }
                    return BadRequest(semester.ToString());
                }
                return BadRequest("Please fill in the form correctly");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<ActionResult> update([FromRoute] string id, [FromForm] Semester_DTO editSemester)
        {
            try
            {
                var semester = await _isemester.UpdateSemester(id, editSemester);
                if (semester == null)
                {
                    return NotFound();
                }
                return Ok(semester);
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
                var semester = await _isemester.DeleteSemester(id);
                if (semester.ToString() == "Semester deleted successfuly")
                {
                    return Ok(semester);
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
