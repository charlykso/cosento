using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturer? _ilecturer;
        public LecturerController(ILecturer? ilecturer)
        {
            _ilecturer = ilecturer;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> getLecturers()
        {
            try
            {
                var lecturer = await _ilecturer!.GetLecturers();
                if (lecturer == null)
                {
                    return NoContent();
                }
                return Ok(lecturer);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/get")]
        public async Task<ActionResult> getLecturer([FromRoute] string id)
        {
            try
            {
                var lecturer = await _ilecturer!.GetLecturer(id);
                if (lecturer == null)
                {
                    return NoContent();
                }
                return Ok(lecturer);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> createLecturer([FromForm] Lecturer_DTO newLecturer)
        {
            const string A = "Email already exists";
            const string B = "Lecturer created";
            const string C = "Something went wrong";
            try
            {
                if (ModelState.IsValid)
                {
                    var lecturer = await _ilecturer!.CreateLecturer(newLecturer);
                    switch (lecturer.ToString())
                    {
                        case (A or C):
                            return BadRequest(lecturer.ToString());
                        case B:
                            return Ok(lecturer.ToString());
                        default:
                            return BadRequest(lecturer.ToString());
                    }
                }
                return BadRequest("Please fill all required feild");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<ActionResult> updateLecturer([FromForm] Lecturer_DTO editLecturer, [FromRoute] string id)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var lecturer = await _ilecturer!.UpdateLecturer(id, editLecturer);
                    if (lecturer.ToString() == "Lecturer updated")
                    {

                        return Ok(lecturer.ToString());
                    }
                    return BadRequest(lecturer.ToString());
                }
                return BadRequest("Please fill all required feild");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> deleteLecturer([FromRoute] string id)
        {
            try
            {
                var lecturer = await _ilecturer!.DeleteLecturer(id);
                if (lecturer.ToString() == "Deleted successfuly")
                {
                    return Ok(lecturer.ToString());
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
